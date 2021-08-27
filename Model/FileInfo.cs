using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MassTextModifier.Model
{
    public class FileInfo
    {
        public string FileName { get { return Path.GetFileName(this.FilePath); } }
        public string FilePath { get; set; }
    }
}