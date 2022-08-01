#region

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEditor;
using UnityEngine;

#endregion

public class AutoRepo
{
    private static void DownloadFile(string url, string name, string directoryName)
    {
        using (var webClient = new WebClient())
        {
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            webClient.DownloadFile(url, directoryName + name);
        }
    }

    public static string Request(string url)
    {

        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.Accept = "application/vnd.github+json";
        request.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1521.3 Safari/537.36";
        var response = (HttpWebResponse)request.GetResponse();
        Stream dataStream = response.GetResponseStream();
        if (dataStream != null)
        {
            var reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
        return "ERROR";
    }

    public static void JsonParsing(string json, string repoName)
    {
        if(json == "ERROR") return;
        
        List<JsonParse.Root> directdl = JsonConvert.DeserializeObject<List<JsonParse.Root>>(json);
        if (directdl != null)
        {
            foreach (JsonParse.Root data in directdl)
            {
                if (data.type == "dir")
                {
                    DirJsonParsing(Request(data.url), data.name, repoName);
                }
                else
                {
                    DownloadFile(data.download_url, data.name, $"Assets/KiwiRepo/{repoName}/");
                }

            }
        }
        Debug.Log($"Done Assets/KiwiRepo/{repoName}");
        AssetDatabase.Refresh();
    }


    public static void DirJsonParsing(string json, string dirName, string reponame)
    {
        List<JsonParse.DirRoot> dirdl = JsonConvert.DeserializeObject<List<JsonParse.DirRoot>>(json);

        if (dirdl != null)
        {
            foreach (JsonParse.DirRoot dirRoot in dirdl)
            {
                if (dirRoot.type == "dir")
                {
                    DirJsonParsing(Request(dirRoot.url), dirRoot.name, reponame);
                }
                else
                {
                    DownloadFile(dirRoot.download_url, dirRoot.name, $"Assets/KiwiRepo/{reponame}/{dirName}/");
                }
            }
        }
    }
}
