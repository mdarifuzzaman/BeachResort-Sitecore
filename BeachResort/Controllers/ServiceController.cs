using BeachResort.Models;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeachResort.Controllers
{
    public class ServiceController:Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/Service/Index.cshtml", CreateModel());
        }

        private static List<ServiceModel> CreateModel()
        {
            var item = RenderingContext.Current.ContextItem;
            Sitecore.Data.Fields.MultilistField refMultilistField = item.Fields["Services"];

            var services = new List<ServiceModel>();

            if (refMultilistField != null)
            {
                Item[] items = refMultilistField.GetItems();
                for(int i=0; i < items.Length; i++)
                {
                    var serviceItem = items[i];
                    var serviceModel = new ServiceModel
                    {
                        Title = new HtmlString(FieldRenderer.Render(serviceItem, "Title")),
                        ServiceImage = new HtmlString(FieldRenderer.Render(serviceItem, "ServiceIcon")),
                        Info = new HtmlString(FieldRenderer.Render(serviceItem, "ServiceInfo"))
                    };

                    services.Add(serviceModel);
                }
            }

            return services;
        }
    }
}