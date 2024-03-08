using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace WpfApp4.Control
{
    internal class SemaforControl : INotifyPropertyChanged
    {
        private Random _random;
        private SemaphoreSlim _semaphore;
        private ObservableCollection<string> _messageThread;
        Thread? _thread;
        public int Count { get; set; }
        public ObservableCollection<string> GetMessage
        {
            get { return _messageThread; }
            set { _messageThread = value; NotifyPropertyChanged("GetMessage"); }


        }
        public void RandomGen()
        {
            Application.Current.Dispatcher.Invoke(() => GetMessage.Add($"{_thread?.Name} Enter in generation"));
            _semaphore.Wait();
            for (int i = 0; i < Count; i++)
            {
                Application.Current.Dispatcher.Invoke(() => GetMessage.Add($"{_random.Next(0, 5000)}"));
            }
            _semaphore.Release();
            Application.Current.Dispatcher.Invoke(() => GetMessage.Add($"{_thread?.Name} Out generation"));
        }
        public void StartSemafore()
        {
            for (int i = 0; i < 10; i++)
            {
                _thread = new Thread(RandomGen) { Name = $"Super thread {i}" };
                _thread.Start();
            }
        }
        public SemaforControl()
        {
            Count = 5000000;
            _random = new Random();
            _messageThread = new ObservableCollection<string>();
            _semaphore = new SemaphoreSlim(3, 3);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
