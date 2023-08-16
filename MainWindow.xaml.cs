using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace PaceCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Calculator calculator;
        public MainWindow()
        {
            InitializeComponent();

            calculator = new Calculator();
        }

        /*
         * Event Handlers
         */

        private void IntervalAddButton_Click(object sender, RoutedEventArgs e)
        {
            AddRow();
        }

        private void XButton_Click(object sender, RoutedEventArgs e)
        {
            Button buttonSender = (Button)sender;
            int row = Grid.GetRow(buttonSender);
            RemoveRow(row);
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            calculator.Clear();
            if (!ExtractIntervals())
            {
                UnDisplayAverage();
                return;
            }
            calculator.CalcAvgPace();
            DisplayAverage();
        }

        /*
         * Helper Methods
         */

        private void AddRow()
        {
            RowDefinition newRow = new RowDefinition
            {
                Height = new GridLength(40.0)
            };
            IntervalGrid.RowDefinitions.Add(newRow);
            //IntervalGrid.RegisterName("RowDef", newRow);

            AddTextBoxToRow("dist", "DistBox", 0);
            AddTextBoxToRow("hour", "HourBox", 1);
            AddTextBoxToRow("min", "MinBox", 2);
            AddTextBoxToRow("sec", "SecBox", 3);
            AddXToRow();
        }

        private void AddTextBoxToRow(string text, string type, int column)
        {
            // add control to row
            TextBox box = new TextBox
            {
                Margin = new Thickness(5.0),
                FontSize = 20.0,
                BorderThickness = new Thickness(0),
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEB, 0xEB, 0xEB))
            };
            IntervalGrid.Children.Add(box);
            int row = IntervalGrid.RowDefinitions.Count - 1;
            Grid.SetRow(box, row);
            Grid.SetColumn(box, column);

            // add an identifiable name to the control
            string name = type + "Row" + row.ToString();
            IntervalGrid.RegisterName(name, box);
        }

        private void AddXToRow()
        {
            // add button to row
            int row = IntervalGrid.RowDefinitions.Count - 1;
            Button xButton = new Button
            {
                Margin = new Thickness(5.0),
                Content = "X",
                FontSize = 20.0,
                Padding = new Thickness(0.0, -2.0, 0.0, 0.0),
                BorderThickness = new Thickness(0),
                Background = new SolidColorBrush(Color.FromArgb(0xFF,0xFF,0xAD,0xAD))
            };
            xButton.Click += XButton_Click;
            IntervalGrid.Children.Add(xButton);
            Grid.SetRow(xButton, row);
            Grid.SetColumn(xButton, 4);

            // add an identifiable name to the button
            IntervalGrid.RegisterName("XButtonRow" + row.ToString(), xButton);
        }

        private void ShiftUpRow(int row)
        {
            int newRow = row - 1;

            string[] oldNames =
            {
                "DistBoxRow" + row,
                "HourBoxRow" + row,
                "MinBoxRow" + row,
                "SecBoxRow" + row,
                "XButtonRow" + row
            };

            string[] newNames =
            {
                "DistBoxRow" + newRow,
                "HourBoxRow" + newRow,
                "MinBoxRow" + newRow,
                "SecBoxRow" + newRow,
                "XButtonRow" + newRow
            };
            
            for(int i = 0; i <= 4; i++)
            {
                Control elem = (Control)IntervalGrid.FindName(oldNames[i]);
                Grid.SetRow(elem, newRow);
                IntervalGrid.UnregisterName(oldNames[i]); // Previous Row-Position Name
                IntervalGrid.RegisterName(newNames[i], elem);
            }
        }

        private void RemoveRow(int row)
        {
            string rowString = row.ToString();

            string[] namesToRemove =
            {
                "DistBoxRow" + rowString,
                "HourBoxRow" + rowString,
                "MinBoxRow" + rowString,
                "SecBoxRow" + rowString,
                "XButtonRow" + rowString
            };

            List<Control> toRemove = new List<Control>();
            for(int i = 0; i <= 4; i++)
            {
                toRemove.Add((Control)IntervalGrid.FindName(namesToRemove[i]));
                IntervalGrid.UnregisterName(namesToRemove[i]);
            }

            foreach(Control elem in toRemove)
            {
                IntervalGrid.Children.Remove(elem);
            }

            // shift all lower elements up a row
            for(int i = row + 1; i <= IntervalGrid.RowDefinitions.Count - 1; i++)
            {
                ShiftUpRow(i);
            }
            IntervalGrid.RowDefinitions.RemoveAt(IntervalGrid.RowDefinitions.Count - 1);
        }

        private void InvalidateTextBox(TextBox box)
        {
            box.Background = Brushes.Red;
        }

        private void ValidateTextBox(TextBox box)
        {
            box.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xEB, 0xEB, 0xEB));
        }

        private bool ValidateIntervals()
        {
            bool allValid = true;

            if(IntervalGrid.RowDefinitions.Count == 0) { return false; }

            for (int i = 0; i < IntervalGrid.RowDefinitions.Count; i++)
            {
                string rowString = i.ToString();

                string[] dataFields =
                {
                    "DistBoxRow" + rowString,
                    "HourBoxRow" + rowString,
                    "MinBoxRow" + rowString,
                    "SecBoxRow" + rowString
                };

                TextBox dist = (TextBox)IntervalGrid.FindName(dataFields[0]);
                TextBox hour = (TextBox)IntervalGrid.FindName(dataFields[1]);
                TextBox min = (TextBox)IntervalGrid.FindName(dataFields[2]);
                TextBox sec = (TextBox)IntervalGrid.FindName(dataFields[3]);

                string dist_text = dist.Text;
                string hour_text = hour.Text;
                string min_text = min.Text;
                string sec_text = sec.Text;

                try
                {
                    float dist_float = float.Parse(dist_text);
                    if(dist_float <= 0.0)
                    {
                        InvalidateTextBox(dist);
                        allValid = false;
                    }
                    else
                    {
                        ValidateTextBox(dist);
                    }
                }
                catch(Exception e)
                {
                    InvalidateTextBox(dist);
                    allValid = false;
                }

                try
                {
                    int hour_int = int.Parse(hour_text);
                    if(hour_int < 0)
                    {
                        InvalidateTextBox(hour);
                        allValid = false;
                    }
                    else
                    {
                        ValidateTextBox(hour);
                    }
                }
                catch
                {
                    if(hour_text == "")
                    {
                        hour.Text = "0";
                        ValidateTextBox(hour);
                    }
                    else
                    {
                        InvalidateTextBox(hour);
                        allValid = false;
                    }
                }

                try
                {
                    int min_int = int.Parse(min_text);
                    if (min_int < 0)
                    {
                        InvalidateTextBox(min);
                        allValid = false;
                    }
                    else
                    {
                        ValidateTextBox(min);
                    }
                }
                catch
                {
                    if (min_text == "")
                    {
                        min.Text = "0";
                        ValidateTextBox(min);
                    }
                    else
                    {
                        InvalidateTextBox(min);
                        allValid = false;
                    }
                }

                try
                {
                    float sec_float = float.Parse(sec_text);
                    if (sec_float < 0.0)
                    {
                        InvalidateTextBox(sec);
                        allValid = false;
                    }
                    else
                    {
                        ValidateTextBox(sec);
                    }
                }
                catch (Exception e)
                {
                    if (sec_text == "")
                    {
                        sec.Text = "0.0";
                        ValidateTextBox(sec);
                    }
                    else
                    {
                        InvalidateTextBox(sec);
                        allValid = false;
                    }
                }

            }

            return allValid;
        }

        public bool ExtractIntervals()
        {
            if (!ValidateIntervals())
                return false;

            for(int i = 0; i<IntervalGrid.RowDefinitions.Count; i++)
            {
                string rowString = i.ToString();

                string[] dataFields =
                {
                    "DistBoxRow" + rowString,
                    "HourBoxRow" + rowString,
                    "MinBoxRow" + rowString,
                    "SecBoxRow" + rowString
                };

                TextBox _dist = (TextBox)IntervalGrid.FindName(dataFields[0]);
                TextBox _hour = (TextBox)IntervalGrid.FindName(dataFields[1]);
                TextBox _min = (TextBox)IntervalGrid.FindName(dataFields[2]);
                TextBox _sec = (TextBox)IntervalGrid.FindName(dataFields[3]);

                float dist = float.Parse(_dist.Text);
                int hour = int.Parse(_hour.Text);
                int min = int.Parse(_min.Text);
                float sec = float.Parse(_sec.Text);

                calculator.AddInterval(new Interval( dist, (hour, min, sec) ));
            }

            return true;
        }

        public void DisplayAverage()
        {
            RowDefinition row = (RowDefinition)IntervalGrid.FindName("AverageDisplayRow");
            row.Height = new GridLength(55);

            (int, int) avgPace = calculator.avgPace;
            string minutes = avgPace.Item1.ToString();
            string seconds = avgPace.Item2.ToString();
            if(avgPace.Item2 < 10)
            {
                seconds = "0" + seconds;
            }
            string displayText = $"Average Pace: {minutes}'{seconds}\"";

            TextBlock avgDisplay = new TextBlock
            {
                Text = displayText,
                FontWeight = FontWeights.DemiBold,
                TextAlignment = TextAlignment.Center,
                FontSize = 20.0,
                Padding = new Thickness(10),
                Background = Brushes.LightGray,
                Height = 50.0,
                Margin = new Thickness(0, 5 ,0 ,0)
            };

            MainWindowLayoutGrid.Children.Add(avgDisplay);
            try
            {
                MainWindowLayoutGrid.RegisterName("AverageDisplayTextBlock", avgDisplay);
            }
            catch (Exception e) { }
            Grid.SetRow(avgDisplay, 4);
        }

        public void UnDisplayAverage()
        {
            RowDefinition row = (RowDefinition)MainWindowLayoutGrid.FindName("AverageDisplayRow");
            row.Height = new GridLength(0);
            try
            {
                MainWindowLayoutGrid.UnregisterName("AverageDisplayTextBlock");
            }
            catch (Exception e) { }
        }

    }

}
