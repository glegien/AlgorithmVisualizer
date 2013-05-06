using System.ComponentModel.Composition;

namespace IOManager
{
    [InheritedExport]
    public interface IIoManager
    {
        string ArchivePath { get; set; }

        void StartAlgorithm(string inputData);

        void StopAlgorithm();

        void PauseAlgorithm();

        void ContinueAlgorithm();
    }
}
