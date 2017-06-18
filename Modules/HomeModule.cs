using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace BandTracker.Objects
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("listBands", Band.GetAll());
        model.Add("listVenues", Venue.GetAll());
        model.Add("show-info", null);
        return View["index.cshtml", model];
      };
    }
  }
}
