using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace Algorithm
{
    public class Algorithm : IAlgorithm
    {
        #region Private Constants

        private const int SleepTime = 100;

        #endregion

        #region Private Member Variables

        private IList<ICalculationResultListener> _calculationResultListenerCollection;
        private volatile bool _paused;
        private volatile int _numberOfIterations;

        #endregion

        #region Constructors

        public Algorithm()
        {
            _calculationResultListenerCollection = new List<ICalculationResultListener>();  
        }

        #endregion

        #region IAlgorithm Implementation

        public void Start(string inputData)
        {         
            CalculationResults = new List<ICalculationResult>();
            _paused = false;
            _numberOfIterations = 30;

            Compute();
        }

        public void Continue()
        {
            if (_paused)
            {
                _paused = false;
                Compute();
            }
            else throw new Exception("Cannot continue stoped or finished algorithm!");
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Stop()
        {
            _numberOfIterations = 0;
        }

        public void RegisterCalculationResultListener(ICalculationResultListener calculationResultListener)
        {
            if (calculationResultListener == null)
                throw new ArgumentNullException("calculationResultListener");

            _calculationResultListenerCollection.Add(calculationResultListener);
        }

        public IEnumerable<ICalculationResult> CalculationResults { get; private set; }

        #endregion

        #region Algorithm computations

        /// <summary>
        /// Time-consuming operations
        /// </summary>
        private void Compute()
        {
            new Thread(() =>
            {
                lock (this)
                {
                    while (!_paused && _numberOfIterations > 0)
                    {
                        Thread.Sleep(SleepTime);
                        Console.WriteLine("N: " + _numberOfIterations);
                        (CalculationResults as List<ICalculationResult>).Add(new SimpleCalculationResult());
                        _numberOfIterations--;
                    }
                    if (_numberOfIterations == 0)
                    {
                        using (var stream = File.Create(Path.Combine(Directory.GetCurrentDirectory(), "AAA.bata")))
                        {
                            stream.WriteByte(8);
                            stream.Flush();
                            stream.Close();
                        }
                    }
                }
            }).Start();
        }

        #endregion
    }
}
