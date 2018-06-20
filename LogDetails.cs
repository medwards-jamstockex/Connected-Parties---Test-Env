using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConnectedParties
{
    class LogDetails
    {
        public static void writeLog(string FileNPath, string data)
        {
            DateTime logTime = DateTime.Now;

            data = logTime.ToString() + ' ' + data + Environment.NewLine;

            File.AppendAllText(FileNPath, data);
        }
    }
}
