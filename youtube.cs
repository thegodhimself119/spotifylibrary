using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace spotifylibrary
{
    internal class youtube
    {
        public static void DownloadSong(String songname,String Directory)
        {
            var pro = new Process
            {
                StartInfo =
                {
                    FileName = @"C:\\Windows\\system32\\cmd.exe",
                    WorkingDirectory =@$"{Directory}",
                    Arguments = $"/C yt-dlp.exe ytsearch:{songname} --no-playlist --extract-audio --audio-format mp3",
                    CreateNoWindow = true


                }

            };

            pro.Start();
        }

        public static void init()
        {

        }

    }
}
