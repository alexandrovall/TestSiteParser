using System;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using TestSiteParser.Parser.Interface;


namespace TestSiteParser.Parser
{
    internal sealed class MySiteParser : IParser
    {
        private static readonly string TdTag = "td";
        private static readonly string TrTag = "tr";
        private static readonly string HrefTag = "href";
        private static readonly string RootTableId = "dnn_ctr691_View_procResultList";
        private static readonly string AuctionTableId = "dnn_ctr691_procDetail_pDgeneralTable";
        private static readonly int RowsCount = 5;

        private readonly WebClient _client;
        private readonly string _url;
        private readonly HtmlDocument _html;

        public MySiteParser(string url)
        {
            _url = url;
            _html = new HtmlDocument();
            _client = new WebClient
            {
                Encoding = Encoding.GetEncoding("utf-8")
            };
        }

        public string Parse()
        {
            string dataString;
            try
            {
                var firstAuctionUrl = GetFirstAuctionUrl();
                dataString = GetDataForAuction(firstAuctionUrl);
                return dataString;
            }
            catch (Exception e)
            {
                dataString = e.Message + Environment.NewLine;
                return dataString;
            }
        }

        private string GetDataForAuction(string url)
        {
            var count = 0;
            var sb = new StringBuilder();
            _html.LoadHtml(_client.DownloadString(url));
            var table = _html.GetElementbyId(AuctionTableId);
            var trNodes = table.ChildNodes.Where(x => TrTag == x.Name);
            foreach (var trItem in trNodes)
            {
                if (count < RowsCount)
                {
                    var tdNodes = trItem.ChildNodes.Where(x => TdTag == x.Name).ToArray();
                    if (0 != tdNodes.Length)
                    {
                        foreach (var tdItem in tdNodes)
                        {
                            sb.Append(tdItem.InnerText);
                            sb.Append(" ");
                        }
                        sb.Append(Environment.NewLine);
                        count++;
                    }
                }
            }
            return sb.ToString();
        }

        private string GetFirstAuctionUrl()
        {
            var firstAuctionUrl = String.Empty;
            _html.LoadHtml(_client.DownloadString(_url));
            var table = _html.GetElementbyId(RootTableId);
            var trNodes = table.ChildNodes.Where(x => TrTag == x.Name);
            foreach (var trItem in trNodes)
            {
                var tdNodes = trItem.ChildNodes.Where(x => TdTag == x.Name).ToArray();
                if (0 != tdNodes.Length)
                {
                    firstAuctionUrl = tdNodes[2].ChildNodes.First().Attributes[HrefTag].Value;
                    return firstAuctionUrl;
                }
            }
            return firstAuctionUrl;
        }
    }
}
