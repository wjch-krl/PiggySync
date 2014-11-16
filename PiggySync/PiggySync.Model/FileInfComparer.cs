using System;
using System.Collections.Generic;

namespace PiggySync.Model
{
    public class FileInfComparer : IEqualityComparer<FileInf>
    {
        public bool Equals(FileInf x, FileInf y)
        {
            return x.FileName == y.FileName && x.Path == y.Path;
        }

        public int GetHashCode(FileInf obj)
        {
            return String.Format("{0}_{1}", obj.FileName, obj.Path).GetHashCode();
        }
    }
}