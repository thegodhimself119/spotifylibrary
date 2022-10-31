using Newtonsoft.Json.Linq;
using RestSharp;
using System.Diagnostics;

namespace spotifylibrary;

public static class  spotify
{

    public static void SearchYoutube(String SongName,String Directory)
    {
        try
        {
            var pro = new Process
            {
                StartInfo =
            {
                FileName = @"C:\\Windows\\system32\\cmd.exe",
                WorkingDirectory =@$"{Directory}",
                Arguments = $"/C yt-dlp.exe ytsearch:{SongName} --no-playlist --extract-audio --audio-format mp3",
                CreateNoWindow = false


            }

            };

            pro.Start();
        }
        catch (System.IO.IOException e)
        {
            Console.WriteLine("couldnt download");
        }

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


    public static String ProcessUrlSpotify(this String url, String type)
    {
        String spotlink = "";
        if (type == "playlist")
        {
             spotlink = "https://open.spotify.com/playlist/";
        }
        else if (type == "album")
        {
            spotlink = "https://open.spotify.com/album/";
        }
        else if (type =="song"|| type == "track")
        {
            spotlink = "https://open.spotify.com/track/";
        }
       
        if (url.Contains(spotlink) == true && url.Contains("?"))
        {

            String processed = url.Substring(url.IndexOf(spotlink) + spotlink.Length, url.Length - spotlink.Length);
            String processed2 = processed.Substring(0,processed.IndexOf("?"));
            return processed2;
        }

        else if (url.Contains(spotlink) == true)
        {
            String processed = url.Substring(url.IndexOf(spotlink) + spotlink.Length, url.Length - spotlink.Length);
            return processed;
        }

        else
        {
            Console.WriteLine("invalid link");
            return "";
        }
    }

    public static void GetFromSpotify(String Url, String ApiKey, String DirectoryOfYt)
    {
        
        String songname = "";
        var client = new RestClient($"https://api.apilayer.com/spotify/playlist_tracks?id={Url}");

        var request = new RestRequest();

        request.AddHeader("apikey", $"{ApiKey}");
        RestResponse response = client.Execute(request);


        

        JObject obj = JObject.Parse(response.Content);
        int total =(int) obj["total"];
        if (total > 99) 
        {
            total = 100;
        }
        
        for (int i = 0; i <total ; i++)
        {
            var title = obj["items"][i]["track"];
            songname = "";

            var trackName = title["name"];
            JArray artists = (JArray)title["album"]["artists"];
            for (int k = 0; k < artists.Count; k++)
            {

                songname = songname + trackName + " " + artists[k]["name"];

            }
            SearchYoutube(songname,DirectoryOfYt);


        }
    }


}






