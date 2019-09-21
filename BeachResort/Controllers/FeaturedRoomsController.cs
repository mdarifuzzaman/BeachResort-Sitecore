using BeachResort.Models;
using Sitecore.Mvc.Helpers;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeachResort.Controllers
{
    public class FeaturedRoomsController: Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/FeaturedRooms/Index.cshtml", CreateModel(true));
        }

        public ActionResult Featured(string name)
        {
            var model = CreateModel(false);
            var room = model.SingleOrDefault(e => e.Name == name);
            if(room == null)
            {
                return View("~/Views/Error.cshtml");
            }

            return View("~/Views/FeaturedRooms/Featured.cshtml", room);
        }

        public ActionResult RoomList()
        {
            var rooms = CreateModel(false);
            return View("~/Views/FeaturedRooms/RoomList.cshtml", rooms);
        }

        private static IEnumerable<FeaturedRoom> CreateModel(bool isFeatured)
        {

            var items = Sitecore.Context.Database.SelectItems("fast:/sitecore/content/Home/Featured Rooms/*");
            var model = new List<FeaturedRoom>();
            foreach(var item in items)
            {
                
                var featuredRoom = new FeaturedRoom();
                featuredRoom.Name = item.Name;
                featuredRoom.MainImage = new HtmlString(FieldRenderer.Render(item, "MainImage"));
                featuredRoom.Price = new HtmlString(FieldRenderer.Render(item, "Price"));
                featuredRoom.Desc = new HtmlString(FieldRenderer.Render(item, "Details"));
                featuredRoom.Extras = new HtmlString(FieldRenderer.Render(item, "Extras"));
                var name = item.Name;
                featuredRoom.OtherImages = new List<HtmlString>();
                var childrenImages = Sitecore.Context.Database.SelectItems($"fast:/sitecore/content/Home/Featured Rooms/{name}/*");
                foreach(var image in childrenImages)
                {
                    featuredRoom.OtherImages.Add(new HtmlString(FieldRenderer.Render(image, "Image")));
                }

                if (isFeatured && item.Fields["Featured"].Value == "1")
                {
                    model.Add(featuredRoom);
                }

                if(!isFeatured)
                {
                    model.Add(featuredRoom);
                }

                
            }

            return model;
        }
    }
}