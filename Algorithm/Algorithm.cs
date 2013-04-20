using System.Threading;

namespace Algorithm
{
    public class Algorithm : IAlgorithm
    {
        #region Private Constants

        private const int SleepTime = 100;

        #endregion

        #region Private Member Variables

        private readonly bool _shouldContinue;

        #endregion

        #region Constructors

        public Algorithm()
        {
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

        #endregion
    }
}
