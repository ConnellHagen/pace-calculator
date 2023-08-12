using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Text;
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
            calculator.AddInterval(new Interval( (0, 1, 0), (8, 40) ));
            calculator.AddInterval(new Interval( (0, 1, 0), (8, 32) ));
            calculator.AddInterval(new Interval( (0, 1, 0), (8, 42) ));
            calculator.AddInterval(new Interval( (0, 1, 0), (8, 48) ));
            calculator.AddInterval(new Interval( (0, 1, 0), (9, 03) ));

            Debug.WriteLine(calculator.avgPace);
        }

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
                Text = text,
                FontSize = 20.0
            };
            IntervalGrid.Children.Add(box);
            int row = IntervalGrid.RowDefinitions.Count - 1;
            Grid.SetRow(box, row);
            Grid.SetColumn(box, column);

            // add an identifiable name to the control
            string name = type + "Row" + row.ToString();
            IntervalGrid.RegisterName(name, box);
        }

        /*
         * Helper Methods
         */

        private void AddXToRow()
        {
            // add button to row
            int row = IntervalGrid.RowDefinitions.Count - 1;
            Button xButton = new Button
            {
                Margin = new Thickness(5.0),
                Content = "X",
                FontSize = 20.0,
                Padding = new Thickness(0.0, -2.0, 0.0, 0.0)
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
    }
}
