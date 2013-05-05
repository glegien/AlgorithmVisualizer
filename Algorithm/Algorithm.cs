using System;
using System.Collections.Generic;
using System.Threading;

namespace Algorithm
{
    public class Algorithm : IAlgorithm
    {
        #region Private Constants

        private const int SleepTime = 100;

        #endregion

        #region Private Member Variables

        private readonly IList<ICalculationResultListener> _calculationResultListenerCollection;
        private readonly bool _shouldContinue;

        #endregion

        #region Constructors

        public Algorithm()
        {
            _calculationResultListenerCollection = new List<ICalculationResultListener>();
            CalculationResults = new List<ICalculationResult>();
            _shouldContinue = true;
        }

        #endregion

        #region IAlgorithm Implementation

        public void Run()
        {
            while (_shouldContinue)
            {
                Thread.Sleep(SleepTime);
            }
        }

        public void RegisterCalculationResultListener(ICalculationResultListener calculationResultListener)
        {
            if (calculationResultListener == null)
                throw new ArgumentNullException("calculationResultListener");

            _calculationResultListenerCollection.Add(calculationResultListener);
        }

        public IEnumerable<ICalculationResult> CalculationResults { get; private set; }

        #endregion
    }
}
