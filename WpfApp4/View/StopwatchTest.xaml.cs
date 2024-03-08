using System;
using System.Collections.Generic;
using System.Linq;
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
using WpfApp4.Control;

namespace WpfApp4.View
{
    /// <summary>
    /// Interaction logic for StopwatchTest.xaml
    /// </summary>
    public partial class StopwatchTest : UserControl
    {
        private StopWatchControl watchControl;
        Regex regex;
        public StopwatchTest()
        {
            InitializeComponent();
            watchControl = new StopWatchControl();
            this.DataContext = watchControl;

            regex = new Regex("[^0-9]+");
        }
        private void startProgramm_Click(object sender, RoutedEventArgs e)
        {


            watchControl.Iterations = Convert.ToInt32(iterationsCountTextBox.Text);
            if (withCounterChBox.IsChecked != true)
            {
                watchControl.StartTest();
            }
            else { watchControl.StartTestBinding(); }

        }
        private void iterationsCountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
