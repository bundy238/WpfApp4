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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp4.Control;  

namespace WpfApp4.View
{
    /// <summary>
    /// Interaction logic for EventTask.xaml
    /// </summary>
    public partial class EventTask : UserControl
    {
        EventTaskThreadControl controlEvent;
        public EventTask()
        {
            InitializeComponent();
            controlEvent = new EventTaskThreadControl();
            this.DataContext = controlEvent;
        }
        private void StartAutoResetEvent_Click(object sender, RoutedEventArgs e)
        {
            controlEvent.StartAutoResetEvent();
        }
    }
}
