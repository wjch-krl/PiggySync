using System;
using System.Diagnostics;
using System.IO;
using PiggySync.Domain.Abstract;
using PiggySync.Domain.Concrete;
using PiggySync.Model;

namespace PiggySync.DesktopFileWather
{
    public class FileWatcher : IFileWather
    {
        private static SyncManager syncM;
        private static XmlSettingsRepository repo;
        private static FileSystemWatcher watcher;

        private static void watcher_Created(object sender, FileSystemEventArgs e)
        {
            fileCreated(e.FullPath);
        }

        private static void fileCreated(string path)
        {
            Debug.WriteLine(path + " has created. Sending notyfy..."); //TODO save modyfied file packet
            FileManager.RefreshPath(path);
            string newElementPath = path.Replace(XmlSettingsRepository.Instance.Settings.SyncRootPath + "\\",
                String.Empty);
            Debug.WriteLine(newElementPath + " Adding");
            try
            {
                syncM.NotyfyAllHost();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private static void fileChanged(string path)
        {
            Debug.WriteLine(path + " has changed. Sending notyfy...");
            FileManager.RefreshPath(path);
            try
            {
                syncM.NotyfyAllHost();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            fileChanged(e.FullPath);
        }

        private static void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            FileDeleted(e.FullPath);
        }

        public static void Initialize(SyncManager main)
        {
            syncM = main;
            repo = XmlSettingsRepository.Instance;

            if (watcher == null)
            {
                watcher = new FileSystemWatcher(repo.Settings.SyncRootPath);
                watcher.IncludeSubdirectories = true;

                watcher.Changed += watcher_Changed;
                watcher.Created += watcher_Created;
                watcher.Deleted += watcher_Deleted;
                watcher.Renamed += wather_Renamed;
                watcher.EnableRaisingEvents = true;
            }
        }

        public static void RefreshMonitoredDirectory(string dir)
        {
            watcher.EnableRaisingEvents = false;
            watcher.Path = dir;
            watcher.EnableRaisingEvents = true;
        }

        private static void FileDeleted(string fullPath)
        {
            var fileInf = new FileInf(fullPath)
            {
                IsDeleted = true,
            };
            FileManager.RefreshPath(fullPath);
            DatabaseManager.Instance.SaveDeletedFile(fileInf);
            try
            {
                syncM.NotyfyAllHost();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private static void wather_Renamed(object sender, RenamedEventArgs e)
        {
            FileRenamed(e.OldFullPath, e.FullPath);
        }

        private static void FileRenamed(string oldFullPath, string fullPath)
        {
            throw new NotImplementedException();
        }
    }
}