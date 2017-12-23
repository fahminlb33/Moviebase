﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using NLog;

namespace Moviebase.Core.Workers
{
    public class MovieRenameWorker : IMovieRenameWorker, IDisposable
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        #region Properties
        public List<MovieEntryFacade> MovieEntries { get; set; }
        public string FileRenamePattern { get; set; }
        public string FolderRenamePattern { get; set; }
        public bool SwapThe { get; set; }
        #endregion

        public IEnumerable<Task> CreateTasks()
        {
            foreach (var entry in MovieEntries)
            {
                yield return new Task(() =>
                {
                    try
                    {
                        _log.Info("Processing: " + entry.Title);
                        using (var fileInfo = new PowerPath(entry.FullPath))
                        {
                            RenameFile(fileInfo, entry);
                            RenameDirectory(fileInfo, entry);
                        }

                        _log.Info("Processed: " + entry.Title);
                    }
                    catch (Exception e)
                    {
                        _log.Error(e, "Error processing: " + entry.Title);
                    }
                });
            }
        }

        private void RenameFile(PowerPath fileInfo, MovieEntryFacade entry)
        {
            var originalFilePath = fileInfo.GetFullPath();
            var renamedFile = fileInfo.RenameFileByPattern(FileRenamePattern, entry);

            if (SwapThe) renamedFile.SwapFileName(Commons.TheName);
            var name = renamedFile.GetFileName();
                    
            var destFilePath = Path.Combine(fileInfo.GetDirectoryPath(), name);

            if (originalFilePath == destFilePath) return;
            File.Move(originalFilePath, destFilePath);
            entry.FullPath = destFilePath;
        }

        private void RenameDirectory(PowerPath fileInfo, MovieEntryFacade entry)
        {
            var originalFilePath = fileInfo.GetDirectoryPath();
            var renamedPath = fileInfo.RenameLastDirectoryByPattern(FolderRenamePattern, entry);

            if (SwapThe) renamedPath.SwapLastDirectoryName(Commons.TheName);
            var directoryPath = renamedPath.GetDirectoryPath();

            if (originalFilePath == directoryPath) return;
            Directory.Move(originalFilePath, directoryPath);

            var name = Path.GetFileName(entry.FullPath);
            Debug.Assert(name != null);
            entry.FullPath = Path.Combine(directoryPath, name);
        }

        #region IDisposable Support
        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                if (MovieEntries != null) MovieEntries.Clear();
            }

            MovieEntries = null;

            _disposedValue = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
