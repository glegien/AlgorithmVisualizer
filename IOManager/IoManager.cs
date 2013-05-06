using System.ComponentModel.Composition;
using Algorithm;
using System.IO;
using System;

namespace IOManager
{
    public class IoManager : IIoManager
    {
        #region Private Member Variables

        private readonly IAlgorithm _algorithm;
        private readonly string _directoryNameFormat = "yyyy-M-d_hh_mm_ss";
        private readonly string _inputFileName = "input.dat";
        private readonly string _outputFileName = "output.dat";
        private readonly FileSystemWatcher _watcher;
        private string _currentArchivePath;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public IoManager(IAlgorithm algorithm)
        {
            _algorithm = algorithm;
            ArchivePath = Directory.GetCurrentDirectory();

            _watcher = new FileSystemWatcher();
            _watcher.Path = Directory.GetCurrentDirectory();
            _watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.FileName 
                | NotifyFilters.LastWrite | NotifyFilters.DirectoryName;
            _watcher.Filter = "*.*"; 
            _watcher.Created += new FileSystemEventHandler(OnOutputFileCreated);
        }

        #endregion

        #region IIoManager Implementation

        public string ArchivePath { get; set; }

        public void StartAlgorithm(string inputData)
        {
            SaveInputAsFile(inputData);
            EnableWatcherForOutputFile();
            _algorithm.Start(inputData);
        }

        public void StopAlgorithm()
        {
            _algorithm.Stop();
            DisableWatcherForOutputFile();
        }

        public void PauseAlgorithm()
        {
            _algorithm.Pause();
            DisableWatcherForOutputFile();
        }

        public void ContinueAlgorithm()
        {
            EnableWatcherForOutputFile();
            _algorithm.Continue();
        }

        #endregion

        #region Archiviztaion methods

        private void SaveInputAsFile(string inputData)
        {
            var directoryName = DateTime.Now.ToString(_directoryNameFormat);
            var path = Path.Combine(new[] { ArchivePath, directoryName });
            _currentArchivePath = path;
            var dirInfo = Directory.CreateDirectory(path);

            using (var streamWriter = File.CreateText(Path.Combine(dirInfo.FullName, _inputFileName)))
            {
                streamWriter.Write(inputData);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        private void EnableWatcherForOutputFile()
        {
            _watcher.EnableRaisingEvents = true;
        }

        private void DisableWatcherForOutputFile()
        {
            _watcher.EnableRaisingEvents = false;
        }

        private void OnOutputFileCreated(object source, FileSystemEventArgs e)
        {
            while(FileInUse(e.FullPath))
                System.Threading.Thread.Sleep(100);
            File.Move(e.FullPath, Path.Combine(_currentArchivePath,_outputFileName));
        }

        private bool FileInUse(string path)
        {
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Open))
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
        }

        #endregion
    }
}
