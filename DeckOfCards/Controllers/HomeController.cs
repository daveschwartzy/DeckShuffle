using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace DeckOfCards.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Deck()
        {
            HttpWebRequest WR = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/new/draw/?count=5");
            WR.UserAgent = ".NET Framework Test";

            HttpWebResponse Response = (HttpWebResponse)WR.GetResponse();

            HttpStatusCode status = Response.StatusCode;
            if (status == HttpStatusCode.OK)
            {

                StreamReader reader = new StreamReader(Response.GetResponseStream());

                string Deck = reader.ReadToEnd();

                JObject JsonData = JObject.Parse(Deck);

                ViewBag.Cards = JsonData["cards"];

                return View();
            }
            else
            {
                ViewBag.Error = status;
                return View();
            }
        }
    }
}