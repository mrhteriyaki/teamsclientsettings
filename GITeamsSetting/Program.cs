using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace GITeamsSetting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string storagefile = roaming + "\\Microsoft\\Teams\\storage.json";

            if (!File.Exists(storagefile))
            {
                Console.WriteLine("storage file missing");
                return;
            }

            string storagedata = File.ReadAllText(storagefile);

            if (!storagedata.Contains("\"fileOpenInDesktopByDefault\""))
            {
                //Missing setting.
                Console.WriteLine("fileOpenInDesktopByDefault missing - adding and setting true.");
                storagedata = storagedata.Substring(0, storagedata.Length - 1); //trim closing brace.
                storagedata = storagedata + ",\"fileOpenInDesktopByDefault\":true}";
            }
            else if (storagedata.Contains("\"fileOpenInDesktopByDefault\":false"))
            {
                //Update to true.
                Console.WriteLine("fileOpenInDesktopByDefault missing - setting false.");
                storagedata = storagedata.Replace("\"fileOpenInDesktopByDefault\":false", "\"fileOpenInDesktopByDefault\":true");
            }

            //Console.Write(storagedata);
            using (StreamWriter writer = new StreamWriter(storagefile))
            {
                writer.Write(storagedata);
            }
            
        }
    }
}
