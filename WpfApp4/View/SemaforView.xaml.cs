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
    /// Interaction logic for SemaforView.xaml
    /// </summary>
    public partial class SemaforView : UserControl
    {

        SemaforControl semaforControl;
        public SemaforView()
        {
            semaforControl = new SemaforControl();
            this.DataContext = semaforControl;
            InitializeComponent();

        }

        private void countTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex;
            regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (countTextBox.Text != "")
            {
                semaforControl.Count = Convert.ToInt32(countTextBox.Text);
            }
            semaforControl.StartSemafore();
        }
    }
}
