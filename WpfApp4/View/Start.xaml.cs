using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp4.Control;

namespace WpfApp4.View
{
    /// <summary>
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        StartControl startControl;

        public Start()
        {
            startControl = new StartControl(this);
            startControl.Max3();
            InitializeComponent();

        }
        private void secondBooton_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new StopwatchTest();
        }
        private void firstBooton_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new EventTask();
        }
        private void Fourth_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new MutexView();
        }
        private void newApp_Click(object sender, RoutedEventArgs e)
        {
            new Start().Show();
        }
        private void semafor_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new SemaforView();
        }
    }
}
