using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeachResort.Models
{
    public class FeaturedRoom
    {
        public HtmlString MainImage { get; set; }
        public List<HtmlString> OtherImages { get; set; }
        public HtmlString Desc { get; set; }
        public HtmlString Price { get; set; }
        public HtmlString Size { get; set; }
        public HtmlString Breakfast { get; set; }
        public HtmlString Pet { get; set; }
        public string Name { get; set; }
        public HtmlString Extras { get; set; }
    }
}