using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Composite.Core;
using System.Xml;

namespace Composite.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(AppSettings.MenuFilePath);
            List<Menu> menus = new List<Menu>();
            foreach (XmlNode nodeOuter in doc.DocumentElement.ChildNodes)
            {
                Menu menu = new Menu();
                menu.Text = nodeOuter.ChildNodes[0].InnerText;
                menu.NavigateUrl = nodeOuter.ChildNodes[1].InnerText;
                menu.OpenInNewWindow = bool.Parse(nodeOuter.Attributes["newWindow"].Value);
                menu.Children = new List<IMenuComponent>();
                foreach (XmlNode nodeInner in nodeOuter.ChildNodes[2].ChildNodes)
                {
                    MenuItem menuItem = new MenuItem();
                    menuItem.Text = nodeInner.ChildNodes[0].InnerText;
                    menuItem.NavigateUrl = nodeInner.ChildNodes[1].InnerText;
                    menu.Children.Add(menuItem);
                }
                menus.Add(menu);
            }
            return View(menus);
        }

    }
}
