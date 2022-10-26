using Newtonsoft.Json.Linq;
using RestSharp;
using System.Diagnostics;

namespace spotifylibrary
{
    public class spotify
    {

        public static void SearchYoutube(String SongName,String Directory)
        {
            var pro = new Process
            {
                StartInfo =
                {
                    FileName = @"C:\\Windows\\system32\\cmd.exe",
                    WorkingDirectory =@$"{Directory}",
                    Arguments = $"/C yt-dlp.exe ytsearch:{SongName} --no-playlist --extract-audio --audio-format mp3",
                    CreateNoWindow = true


                }

            };

            pro.Start();

            //alternate 
            /* System.Diagnostics.Process process = new System.Diagnostics.Process();
             System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
             startInfo.FileName = "cmd.exe";
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
             startInfo.WorkingDirectory = @"F:\ytdlp\";
             startInfo.Arguments = "/C yt-dlp.exe ytsearch:hello --no-playlist --extract-audio --audio-format mp3";
             process.StartInfo = startInfo;
             process.Start();*/
        }


        public static String ProcessUrl(String url)
        {

            string spotlink = "https://open.spotify.com/playlist/";
            if (url.Contains(spotlink) == true)
            {

                String processed = url.Substring(url.IndexOf(spotlink) + spotlink.Length, url.Length - spotlink.Length);
                return processed;
            }

            else
            {
                return "invalid playlist link";
            }
        }

        public static void GetFromSpotify(String Url, String ApiKey, String DirectoryOfYt)
        {
            String newUrl = ProcessUrl(Url);
            String songname = "";
            var client = new RestClient($"https://api.apilayer.com/spotify/playlist_tracks?id={newUrl}");

            var request = new RestRequest();

            request.AddHeader("apikey", $"{ApiKey}");
            RestResponse response = client.Execute(request);


            

            JObject obj = JObject.Parse(response.Content);
            var name = obj["name"];
            var image = obj["images"][0]["url"];

            JArray title = (JArray)obj["tracks"]["items"];
            for (int i = 0; i < title.Count; i++)
            {
                songname = "";
                var trackName = title[i]["track"]["name"];
                JArray artists = (JArray)title[i]["track"]["album"]["artists"];

                for (int k = 0; k < artists.Count; k++)
                {

                    songname = songname + trackName + " " + artists[k]["name"];

                }
                SearchYoutube(songname,DirectoryOfYt);



            }
        }


    }

}

 


    
