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
            calculator.add_interval(new Interval( (0, 1, 0), (8, 40) ));
            calculator.add_interval(new Interval( (0, 1, 0), (8, 32) ));
            calculator.add_interval(new Interval( (0, 1, 0), (8, 42) ));
            calculator.add_interval(new Interval( (0, 1, 0), (8, 48) ));
            calculator.add_interval(new Interval( (0, 1, 0), (9, 03) ));

            Debug.WriteLine(calculator.avg_pace);
        }
    }
}
