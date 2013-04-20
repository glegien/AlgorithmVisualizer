using System.ComponentModel.Composition;

namespace Algorithm
{
    /// <summary>
    /// Use this interface to interact with algorithm
    /// </summary>
    [InheritedExport]
    public interface IAlgorithm
    {
        /// <summary>
        /// Example method for setting up projects purposes
        /// Feel free to remove / change when necessary
        /// </summary>
        void Run();
    }
}
