﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using PiggySync.Common;
using PiggySync.Domain.Concrete;
using PiggySync.Model;
using PiggySync.Model.Concrete;

namespace PiggySync.Core
{
    public class FileManager
    {
        private static SyncInfoPacket rootFolder;
        private static FileManager fileManager;

        static FileManager()
        {
            if (fileManager == null)
            {
                fileManager = new FileManager();
                CreateRootFolder();
            }
        }

        private FileManager()
        {
            rootFolder = new SyncInfoPacket();
        }

        public static SyncInfoPacket RootFolder
        {
            get { return rootFolder; }
            set { rootFolder = value; }
        }

        public static SyncInfoPacket RootFolderCopy
        {
            get
            {
                lock (rootFolder)
                {
                    return rootFolder.Copy();
                }
            }
        }

        public static void CreateRootFolder()
        {
            rootFolder = new SyncInfoPacket();
            CreateRootFolder(rootFolder);
            DatabaseManager.Instance.SaveFiles(rootFolder);
        }

        private static void CreateRootFolder(SyncInfoPacket root) //TODO delete packets 
        {
            string path = XmlSettingsRepository.Instance.Settings.SyncRootPath;
            var dbFiles = DatabaseManager.Instance.GetAllFiles();
            CreateRootFolder(root, path, dbFiles);
            AddOtherDeletedFiles(dbFiles, root);
        }

        private static void CreateRootFolder(SyncInfoPacket root, string path, HashSet<FileInf> dbFiles)
            //TODO delete packets 
        {
            if (root is FolderInfoPacket)
            {
                path += @"\" + (root as FolderInfoPacket).FolderName;
            }
            GetFiles(root, path, dbFiles);
            GetDirectories(root, path, dbFiles);

            rootFolder.ElelmentsCount = rootFolder.GetFileCount();
        }


        private static void GetDirectories(SyncInfoPacket root, string path, HashSet<FileInf> deletedFiles)
        {
            string[] folders = TypeResolver.DirectoryHelper.GetDirectories(path);
            FolderInfoPacket folder;
            foreach (var x in folders)
            {
                try
                {
                    folder = new FolderInfoPacket(x.Substring(path.Length + 1));
                    //simple path 
                    CreateRootFolder(folder, path, deletedFiles);
                    root.Folders.Add(folder);
                    Debug.WriteLine("Added dir: {0}", x);
                }
                catch (IOException ex)
                {
                    Debug.WriteLine("Cannot add dir: {0}", ex);
                }
            }
        }

        private static void GetFiles(SyncInfoPacket root, string path, HashSet<FileInf> dbFiles)
        {
            string[] localFileNames = TypeResolver.DirectoryHelper.GetFiles(path);
            var addedFiles = new List<FileInf>();
            foreach (var x in localFileNames)
            {
                try
                {
                    var fileInf = new FileInf(x)
                    {
                        Path = path,
                    };
                    if (XmlSettingsRepository.Instance.Settings.BannedFiles.Contains(fileInf.FileName))
                    {
                        continue;
                    }
                    root.Files.Add(new FileInfoPacket(fileInf));
                    addedFiles.Add(fileInf);
                    Debug.WriteLine("Added file: {0}", x.Substring(path.Length + 1));
                }
                catch (IOException e)
                {
                    Debug.WriteLine("Cannot add file: {0}", e);
                }
            }
            var folderFiles = new List<FileInf>(dbFiles.Where(x => x.Path == path));
            foreach (var element in folderFiles)
            {
                FileInf file = null;
                if ((file = addedFiles.FirstOrDefault(x => x.FileName == element.FileName && !element.IsDeleted)) ==
                    null)
                {
                    Debug.WriteLine("Added new deleted file: " + element.FileName);
                    element.IsDeleted = true;
                    DatabaseManager.Instance.SaveDeletedFile(element);
                    root.DeletedFiles.Add(new FileDeletePacket(element));
                }
                else
                {
                    file.IsDeleted = false;
                    file.Id = element.Id;
                }
                dbFiles.Remove(element);
            }
        }

        public static void RefreshRootFolder()
        {
            CreateRootFolder();
        }

        private static void AddOtherDeletedFiles(HashSet<FileInf> dbFiles, SyncInfoPacket root)
        {
            foreach (var file in dbFiles)
            {
                file.IsDeleted = true;
                var folder = file.Path.Replace(XmlSettingsRepository.Instance.Settings.SyncRootPath, String.Empty);
                folder = folder.Replace('\\', '/');
                var foldersTree = folder.Split('/');
                string curr = XmlSettingsRepository.Instance.Settings.SyncRootPath;
                foreach (var element in foldersTree)
                {
                    curr = Path.Combine(element, curr);
                    if (!TypeResolver.DirectoryHelper.Exists(curr))
                    {
                        var deletedFolder =
                            new FolderInfoPacket(
                                curr.Replace(XmlSettingsRepository.Instance.Settings.SyncRootPath, String.Empty), 144);
                        root.Folders.Add(deletedFolder); //TODO
                        deletedFolder.DeletedFiles.Add(new FileDeletePacket(file));
                        break;
                    }
                }
                rootFolder.DeletedFiles.Add(new FileDeletePacket(file));
                //TODO Create deleted folder packet
            }
        }

        public static void RefreshPath(string path)
        {
            if (!TypeResolver.DirectoryHelper.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }
            path = path.Replace(XmlSettingsRepository.Instance.Settings.SyncRootPath, String.Empty);
            path = path.Replace('\\', '/');
            SyncInfoPacket folder = rootFolder;
            if (!String.IsNullOrWhiteSpace(path))
            {
                foreach (var segment in path.Split('/'))
                {
                    folder = folder.Folders.First(element => element.FolderName == segment);
                }
            }
            var dbFiles = DatabaseManager.Instance.GetAllFiles();
            GetFiles(folder, path, dbFiles);
            DatabaseManager.Instance.SaveFiles(folder);
        }
    }
}