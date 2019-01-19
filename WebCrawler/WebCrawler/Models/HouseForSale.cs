using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Models
{
    public class HouseForSale
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Area { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public string DetailUrl { get; set; }
    }
}
