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
            Teamsv1Check();
            TeamsV2Check();
        }

        static void Teamsv1Check()
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

        static void TeamsV2Check()
        {
           string localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appSettingsFile = Path.Combine(localAppDataFolder, "Packages\\MSTeams_8wekyb3d8bbwe\\LocalCache\\Microsoft\\MSTeams\\app_settings.json");
            if (!File.Exists(appSettingsFile))
            {
                Console.WriteLine("No Teams v2 app settings file");
                return;
            }

            string storagedata = File.ReadAllText(appSettingsFile);
            if (!storagedata.Contains("\"open_file_in_desktop_app\""))
            {
                //Missing setting.
                Console.WriteLine("open_file_in_desktop_app missing - adding and setting true.");
                storagedata = storagedata.Substring(0, storagedata.Length - 1); //trim closing brace.
                storagedata = storagedata + ",\"open_file_in_desktop_app\":true}";
            }
            else if (storagedata.Contains("\"open_file_in_desktop_app\":false"))
            {
                //Update to true.
                Console.WriteLine("open_file_in_desktop_app missing - setting false.");
                storagedata = storagedata.Replace("\"open_file_in_desktop_app\":false", "\"open_file_in_desktop_app\":true");
            }

            //Console.Write(storagedata);
            using (StreamWriter writer = new StreamWriter(appSettingsFile))
            {
                writer.Write(storagedata);
            }
            //"open_file_in_desktop_app":true
            //C:\Users\gici\AppData\Local\Packages\MSTeams_8wekyb3d8bbwe\LocalCache\Microsoft\MSTeams\app_settings.json

        }
    }
}
