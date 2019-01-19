using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebCrawler.Models;

namespace WebCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            int abc = 1;
            List<HouseForSale> list = new List<HouseForSale>();
            for (int i = 1; i <= 10; i++)
            {
                GetHouseForSales(list, ref count, i);
                Console.WriteLine($"Finish Page {i}");
            }
            var document = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                                         new XElement("HouseForSales",                                          
                                                list.Select(x => new XElement("HouseForSale",
                                                                    new XElement(nameof(x.Id), x.Id),
                                                                    new XElement(nameof(x.Title), x.Title),
                                                                    new XElement(nameof(x.Description), x.Description),
                                                                    new XElement(nameof(x.Price), x.Price),
                                                                    new XElement(nameof(x.Area), x.Area),
                                                                    new XElement(nameof(x.Location), x.Location),
                                                                    new XElement(nameof(x.ImageUrl), x.ImageUrl),
                                                                    new XElement(nameof(x.DetailUrl), x.DetailUrl)

                                                ))
                                         ));
            document.Save("data.xml");
            Console.WriteLine("Finish All");
            Console.ReadLine();
        }
        static void GetHouseForSales(List<HouseForSale> list, ref int count, int pageNumber)
        {
            string url = $@"https://batdongsan.com.vn/nha-dat-ban/p{pageNumber}";
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDocument = htmlWeb.Load(url);
            HtmlNodeCollection nodes = htmlDocument.DocumentNode
                                        ?.SelectSingleNode("//div[@class='product-list product-list-page']//div[@class='Main']")
                                        ?.SelectNodes(".//div[@class='vip0 search-productItem']");

         
            foreach (var item in nodes ?? Enumerable.Empty<HtmlNode>())
            {
                HouseForSale houseForSale = new HouseForSale()
                {
                    Id = ++count,
                    Title = item.SelectSingleNode(".//div[@class='p-title']//h3//a")?.InnerText.Trim() ?? "Unknow",
                    Description = item.SelectSingleNode(".//div[@class='p-main']//div[@class='p-main-text']")?.InnerText.Trim() ?? "Unknow",
                    Price = item.SelectSingleNode(".//span[@class='product-price']")?.InnerText.Trim() ?? "Unknow",
                    Area = item.SelectSingleNode(".//span[@class='product-area']")?.InnerText.Trim() ?? "Unknow",
                    Location = item.SelectSingleNode(".//span[@class='product-city-dist']")?.InnerText.Trim() ?? "Unknow",
                    ImageUrl = item.SelectSingleNode(".//div[@class='p-main']//img[@class='product-avatar-img']").GetAttributeValue("src", null),
                    DetailUrl = item.SelectSingleNode(".//div[@class='p-title']//h3//a").GetAttributeValue("href", null),
                };
                list.Add(houseForSale);
            }
        }
    }
}
