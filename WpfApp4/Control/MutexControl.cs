using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp4.Control
{
    internal class MutexControl : INotifyPropertyChanged
    {
        private Random _random;
        private Mutex _mutex;
        private BinaryWriter? _writer;
        private BinaryReader? _reader;
        private int _getBuffer;
        private int _getSimple;
        private int _getSevenSimple;
        private int[] _buffer;
        private ObservableCollection<int> _randomList;
        private ObservableCollection<int> _simpleList;
        private ObservableCollection<int> _sevenList;

        #region Binding
        public int CountIteration { get; set; }
        public int GetRandom
        {
            get { return _getBuffer; }
            set { _getBuffer = value; NotifyPropertyChanged("GetRandom"); }

        }
        public int GetSimple
        {
            get { return _getSimple; }
            set { _getSimple = value; NotifyPropertyChanged("GetSimple"); }

        }
        public int GetSevenSimple
        {
            get { return _getSevenSimple; }
            set { _getSevenSimple = value; NotifyPropertyChanged("GetSevenSimple"); }

        }
        public ObservableCollection<int> GetRandomList
        {
            get { return _randomList; }
            set { _randomList = value; NotifyPropertyChanged("GetRandomList"); }
        }
        public ObservableCollection<int> GetSimpleList
        {
            get { return _simpleList; }
            set { _simpleList = value; NotifyPropertyChanged("GetSimpleList"); }
        }
        public ObservableCollection<int> GetSevenList
        {
            get { return _sevenList; }
            set { _sevenList = value; NotifyPropertyChanged("GetSevenList"); }
        }
        #endregion

        public void SaveRandomInFile()
        {

            _mutex.WaitOne();

            using (_writer = new BinaryWriter(File.Open("Random.bin", FileMode.OpenOrCreate)))
            {
                for (int i = 0; i < CountIteration; i++)
                {

                    _writer.Write(_buffer[i]);
                    GetRandom = _buffer[i];

                    Application.Current.Dispatcher.Invoke(() => GetRandomList.Add(_buffer[i]));

                }
            }
            _mutex.ReleaseMutex();



        }
        public void SimpleNumSaveinFile()
        {
            _mutex.WaitOne();
            using (_reader = new BinaryReader(File.Open("Random.bin", FileMode.Open)))
            {
                for (int i = 0; i < CountIteration; i++)
                {
                    _buffer[i] = _reader.ReadInt32();
                }

            }
            using (_writer = new BinaryWriter(File.Open("Simple.bin", FileMode.OpenOrCreate)))
            {
                for (int i = 0; i < CountIteration; i++)
                {
                    if (IsSimple(_buffer[i]))
                    {
                        _writer.Write(_buffer[i]);
                        GetSimple = _buffer[i];
                        Application.Current.Dispatcher.Invoke(() => GetSimpleList.Add(_buffer[i]));
                    }
                }
            }
            _mutex.ReleaseMutex();
        }
        public void SevenSaveInfile()
        {
            _mutex.WaitOne();
            using (_reader = new BinaryReader(File.Open("Random.bin", FileMode.Open)))
            {

                using (_writer = new BinaryWriter(File.Open("SimpleSeven.bin", FileMode.OpenOrCreate)))
                {
                    for (int i = 0; i < CountIteration; i++)
                    {
                        _buffer[i] = _reader.ReadInt32();

                        if (_buffer[i] % 10 == 7)
                        {
                            _writer.Write(_buffer[i]);
                            GetSevenSimple = _buffer[i];
                            Application.Current.Dispatcher.Invoke(() => GetSevenList.Add(_buffer[i]));
                        }

                    }

                }

            }
            _mutex.ReleaseMutex();
        }
        private bool IsSimple(int incommingNum)
        {

            bool isSimple = false;
            for (int i = 2; i <= incommingNum; i++)
            {
                isSimple = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        isSimple = false;
                        break;
                    }

                }
            }
            return isSimple;
        }

        public MutexControl()
        {
            _mutex = new Mutex();
            _random = new Random();
            CountIteration = 5000000;
            _randomList = new ObservableCollection<int>();
            _simpleList = new ObservableCollection<int>();
            _sevenList = new ObservableCollection<int>();

            _buffer = new int[CountIteration];
            for (int i = 0; i < CountIteration; i++)
            {
                _buffer[i] = _random.Next(0, 100);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
