using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Wox.Plugin.IP138
{

    public class Main : IPlugin
    {

        private string ip138Url = @"http://ip138.com/ips138.asp?ip=";

        public void Init(PluginInitContext context)
        {
            
        }

        public List<Result> Query(Query query)
        {
            List<Result> results = new List<Result>();

            string ip = query.Search;

            var client = new WebClient();
            string html = Encoding.GetEncoding("GBK").GetString(client.DownloadData(ip138Url + ip));

            Match li = Regex.Match(html, @"<ul class=""ul1"">(.*?)</ul>");
            MatchCollection matches = Regex.Matches(li.Groups[1].Value, @"<li>(.+?)：(.+?)</li>");
            foreach (Match match in matches)
            {
                results.Add(new Result()
                {
                    Title = match.Groups[2].Value,
                    SubTitle = match.Groups[1].Value,
                    IcoPath = "Images\\ip138.ico"
                });
            }

            return results;
        }

    }
}
