using System;

namespace PiggySync.Common
{
    public interface IDirectoryHelper
    {
        string DocumentsPath { get; }

        string MachineName { get; }
        string MyDocuments { get; }
        string[] GetFilesFromAllDirectories(string rootPath, string fileName);
        void CreateDirectory(string path);
		void CreateHiddenDirectory(string path);

        bool Exists(string name);
        string[] GetDirectories(string path);
        string[] GetFiles(string path);
    }
}