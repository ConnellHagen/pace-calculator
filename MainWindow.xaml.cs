using System;
using System.Collections.Generic;
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
            RowDefinition newRow = new RowDefinition
            {
                Height = new GridLength(40.0)
            };
            IntervalGrid.RowDefinitions.Add(newRow);

            AddTextBoxToRow("dist", 0);
            AddTextBoxToRow("hour", 1);
            AddTextBoxToRow("min", 2);
            AddTextBoxToRow("sec", 3);
            AddXToRow();
        }

        private void AddTextBoxToRow(string text, int column)
        {
            TextBox box = new TextBox
            {
                Margin = new Thickness(5.0),
                Text = text,
                FontSize = 20.0
            };
            IntervalGrid.Children.Add(box);
            Grid.SetRow(box, IntervalGrid.RowDefinitions.Count - 1);
            Grid.SetColumn(box, column);
        }

        private void AddXToRow()
        {
            Button x = new Button
            {
                Margin = new Thickness(5.0),
                Content = "X",
                FontSize = 20.0,
                Padding = new Thickness(0.0, -2.0, 0.0, 0.0)
            };
            IntervalGrid.Children.Add(x);
            Grid.SetRow(x, IntervalGrid.RowDefinitions.Count - 1);
            Grid.SetColumn(x, 4);
        }
    }
}
