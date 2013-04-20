using System.ComponentModel.Composition;
using Algorithm;

namespace IOManager
{
    public class IoManager : IIoManager
    {
        #region Private Member Variables

        private readonly IAlgorithm _algorithm;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public IoManager(IAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        #endregion
    }
}
