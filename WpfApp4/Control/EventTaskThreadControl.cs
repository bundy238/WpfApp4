using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WpfApp4.Control
{
    class EventTaskThreadControl : INotifyPropertyChanged
    {
        private Thread saveFileRandomThread;
        private Thread SumInFileThread;
        private Thread multiplyFileRandomThread;
        private List<int> _randomNum;
        private List<int> _sum;
        private Random _random;
        private AutoResetEvent _auto;
        private string _Info;
        private int _iteration;


        public string? GetInfo
        {
            get { return _Info; }
            set { _Info = value; NotifyPropertyChanged("GetInfo"); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NumberGeneration()
        {
            _auto.WaitOne();
            GetInfo = $"Thread {saveFileRandomThread.Name} Start";

            for (int i = 0; i < _iteration; i++)
            {

                _randomNum.Add(_random.Next(0, 100));
            }
            using (BinaryWriter writer = new BinaryWriter(File.Open("Num.bin", FileMode.OpenOrCreate)))
            {
                for (int i = 0; i < _randomNum.Count(); i++)
                {
                    writer.Write(_randomNum[i]);
                }
            }
            _auto.Set();
            GetInfo = $"Thread {saveFileRandomThread.Name} End";

        }
        public void SumInfile()
        {
            GetInfo = $"Thread {SumInFileThread.Name} Start";
            _auto.WaitOne();
            using (BinaryReader read = new BinaryReader(File.Open("Num.bin", FileMode.Open)))
            {
                for (int i = 0; i < _iteration; i++)
                {
                    _randomNum.Add(read.ReadInt32());
                }
            }

            for (int i = 0; i < _randomNum.Count(); i++)
            {
                _sum.Add(_randomNum[i]);
                _sum[i] += _randomNum[i];
            }
            using (BinaryWriter writer = new BinaryWriter(File.Open("Num.bin", FileMode.OpenOrCreate)))
            {
                for (int i = 0; i < _sum.Count(); i++)
                {
                    writer.Write(_sum[i]);
                }
            }
            _auto.Set();
            GetInfo = $"Thread {SumInFileThread.Name} End";
        }
        public void Multiply()
        {
            GetInfo = $"Thread {multiplyFileRandomThread.Name} Start";
            _auto.WaitOne();
            using (BinaryReader read = new BinaryReader(File.Open("Num.bin", FileMode.Open)))
            {
                for (int i = 0; i < _iteration; i++)
                {
                    _sum.Add(read.ReadInt32());
                }
            }
            for (int i = 0; i < _randomNum.Count(); i++)
            {
                _sum.Add(_randomNum[i]);
                _sum[i] *= _randomNum[i];
            }
            using (BinaryWriter writer = new BinaryWriter(File.Open("Num.bin", FileMode.OpenOrCreate)))
            {
                for (int i = 0; i < _sum.Count(); i++)
                {
                    writer.Write(_sum[i]);
                }
            }
            GetInfo = $"Thread {multiplyFileRandomThread.Name} End";
        }
        public void StartAutoResetEvent()
        {
            saveFileRandomThread = new Thread(NumberGeneration);
            SumInFileThread = new Thread(SumInfile);
            multiplyFileRandomThread = new Thread(Multiply);

            saveFileRandomThread.Name = "Save random num in file";
            SumInFileThread.Name = "Save sum in file";
            multiplyFileRandomThread.Name = "Save multiply in file";

            saveFileRandomThread.Start();
            SumInFileThread.Start();
            multiplyFileRandomThread.Start();
            _auto.Set();
        }
        public EventTaskThreadControl()
        {
            GetInfo = "Constructor";
            _iteration = 50000000;
            _randomNum = new List<int>();
            _sum = new List<int>();
            _random = new Random();
            _auto = new AutoResetEvent(false);
        }
    }
}
