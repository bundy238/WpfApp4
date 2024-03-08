using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.Control
{
    internal class StopWatchControl : INotifyPropertyChanged
    {

        private int _counterMunex;
        private int _counterLocker;
        private int _counterMonitor;
        private Stopwatch _stopWatchMutex;
        private Stopwatch _stopWatchLocker;
        private Stopwatch _stopWatchMonitor;
        private object _locker;
        private object _monitor;
        private Mutex _mutex;

        private long _milisecondsLocker;
        private long _milisecondsMutex;
        private long _milisecondsMonitor;
        public int Iterations { get; set; }

        #region Binding
        public long LockerTime
        {
            get { return _milisecondsLocker; }
            set
            {
                _milisecondsLocker = value;
                NotifyPropertyChanged("LockerTime");

            }
        }
        public long MutexTime
        {
            get { return _milisecondsMutex; }
            set
            {
                _milisecondsMutex = value;
                NotifyPropertyChanged("MutexTime");

            }
        }
        public long MonitorTime
        {
            get { return _milisecondsMonitor; }
            set { _milisecondsMonitor = value; NotifyPropertyChanged("MonitorTime"); }
        }

        public int LockerCounter
        {
            get { return _counterLocker; }
            set { _counterLocker = value; NotifyPropertyChanged("LockerCounter"); }

        }
        public int MutexCounter
        {
            get { return _counterMunex; }
            set { _counterMunex = value; NotifyPropertyChanged("MutexCounter"); }
        }
        public int MonitorCounter
        {
            get { return _counterMonitor; }
            set { _counterMonitor = value; NotifyPropertyChanged("MonitorCounter"); }
        }

        #endregion
        #region With Binding counter
        public void TestMutexBinding()
        {
            _stopWatchMutex.Start();
            for (int i = 0; i < Iterations; i++)
            {
                _mutex.WaitOne();
                MutexCounter++;
                _mutex.ReleaseMutex();
            }
            _stopWatchMutex.Stop();
            MutexTime = _stopWatchMutex.ElapsedMilliseconds;

        }
        public void TestMonitorBinding()
        {
            _stopWatchMonitor.Start();
            for (int i = 0; i < Iterations; i++)
            {
                Monitor.Enter(_monitor);
                MonitorCounter++;
                Monitor.Exit(_monitor);
            }
            _stopWatchMonitor.Stop();
            MonitorTime = _stopWatchMonitor.ElapsedMilliseconds;
        }
        public void TestLockerBinding()
        {
            _stopWatchLocker.Start();
            for (int i = 0; i < Iterations; i++)
            {

                lock (_locker)
                {
                    LockerCounter++;
                }

            }
            _stopWatchLocker.Stop();
            LockerTime = _stopWatchLocker.ElapsedMilliseconds;
        }
        public void StartTestBinding()
        {
            new Thread(TestMutexBinding).Start();
            new Thread(TestMutexBinding).Start();
            new Thread(TestMutexBinding).Start();
            new Thread(TestMutexBinding).Start();
            new Thread(TestMutexBinding).Start();

            new Thread(TestMonitorBinding).Start();
            new Thread(TestMonitorBinding).Start();
            new Thread(TestMonitorBinding).Start();
            new Thread(TestMonitorBinding).Start();
            new Thread(TestMonitorBinding).Start();

            new Thread(TestLockerBinding).Start();
            new Thread(TestLockerBinding).Start();
            new Thread(TestLockerBinding).Start();
            new Thread(TestLockerBinding).Start();
            new Thread(TestLockerBinding).Start();
        }
        #endregion

        public void TestMutex()
        {
            _stopWatchMutex.Start();
            for (int i = 0; i < Iterations; i++)
            {
                _mutex.WaitOne();
                _counterMunex++;
                _mutex.ReleaseMutex();
            }
            _stopWatchMutex.Stop();
            MutexTime = _stopWatchMutex.ElapsedMilliseconds;

        }
        public void TestMonitor()
        {
            _stopWatchMonitor.Start();
            for (int i = 0; i < Iterations; i++)
            {
                Monitor.Enter(_monitor);
                _counterMonitor++;
                Monitor.Exit(_monitor);
            }
            _stopWatchMonitor.Stop();
            MonitorTime = _stopWatchMonitor.ElapsedMilliseconds;
        }
        public void TestLocker()
        {
            _stopWatchLocker.Start();
            for (int i = 0; i < Iterations; i++)
            {

                lock (_locker)
                {
                    _counterLocker++;
                }

            }
            _stopWatchLocker.Stop();
            LockerTime = _stopWatchLocker.ElapsedMilliseconds;
        }
        public void StartTest()
        {
            new Thread(TestMutex).Start();
            new Thread(TestMutex).Start();
            new Thread(TestMutex).Start();
            new Thread(TestMutex).Start();
            new Thread(TestMutex).Start();

            new Thread(TestMonitor).Start();
            new Thread(TestMonitor).Start();
            new Thread(TestMonitor).Start();
            new Thread(TestMonitor).Start();
            new Thread(TestMonitor).Start();

            new Thread(TestLocker).Start();
            new Thread(TestLocker).Start();
            new Thread(TestLocker).Start();
            new Thread(TestLocker).Start();
            new Thread(TestLocker).Start();
        }
        public StopWatchControl()
        {
            Iterations = 1000000;
            _stopWatchMutex = new Stopwatch();
            _stopWatchLocker = new Stopwatch();
            _stopWatchMonitor = new Stopwatch();
            _locker = new object();
            _monitor = new object();
            _mutex = new Mutex();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
