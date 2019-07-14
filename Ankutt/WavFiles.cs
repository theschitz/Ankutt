using System.Collections.Generic;
using System.IO;

namespace Ankutt
{
    public class WavFiles
    {
        private static readonly string filepath = @"resources\sounds";
        public List<string> fileList = new List<string>();
        public WavFiles()
        {
            DirectoryInfo d = new DirectoryInfo(filepath);
            foreach (var file in d.GetFiles("*.wav"))
            {
                fileList.Add(file.FullName);
            }
        }
    }
}
