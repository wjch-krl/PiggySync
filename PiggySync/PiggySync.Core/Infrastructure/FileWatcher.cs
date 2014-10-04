using DuckSyncWin.Domain.Concrete;
using DuckSyncWin.WinUI.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DuckSyncWin.WinUI.Infrastructure
{
    public static class FileWatcher
    {
        private static DuckConnect dckConnect;
        private static XmlSettingsRepository repo;
        private static FileSystemWatcher watcher;

        static void watcher_Created(object sender, FileSystemEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Name + " has created. Sending notyfy...");//TODO save modyfied file packet
            string newElementPath = e.FullPath.Replace(XmlSettingsRepository.Instance.Settings.CurrentDirectory+"\\", "");
            System.Diagnostics.Debug.WriteLine(newElementPath + " Adding");
            //  if (e.ChangeType == WatcherChangeTypes.Created)
            try
            {
           //     dckConnect.NotyfyAllHost();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        static void addFileToRootFolder()
        {

        }

        static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Name + " has changed. Sending notyfy...");//TODO save modyfied file packet
            try
            {
           //     dckConnect.NotyfyAllHost();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

        }


        public static void Initialize(DuckConnect main)
        {
            dckConnect = main;
            repo = XmlSettingsRepository.Instance;

            if (watcher == null)
            {
                watcher = new FileSystemWatcher(repo.Settings.CurrentDirectory);
                watcher.IncludeSubdirectories = true;

                watcher.Changed += watcher_Changed;
                watcher.Created += watcher_Created;

                watcher.EnableRaisingEvents = true;
            }
        }

        public static void RefreshMonitoredDirectory(string dir)
        {
            watcher.EnableRaisingEvents = false;
            watcher.Path = dir;
            watcher.EnableRaisingEvents = true;
        }
    }
}
