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
      }; //homepage with lists of venues/bands, buttons to add venue/band
      Get["/bands/new"] = _ => {
        Dictionary<string, string> model = new Dictionary<string, string>{};
        model.Add("form-type", "new-band");
        return View["form.cshtml", model];
      }; //returns form to add new band
      Post["/bands/new"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Band band = new Band(Request.Form["band-name"]);
        band.Save();
        model.Add("listBands", Band.GetAll());
        model.Add("listVenues", Venue.GetAll());
        model.Add("newBand", band);
        model.Add("show-info", "new-band-info");
        return View["index.cshtml", model];
      }; //posts from form adding new band
      Get["/venues/new"] = _ => {
        Dictionary<string, string> model = new Dictionary<string, string>{};
        model.Add("form-type", "new-venue");
        return View["form.cshtml", model];
      }; //returns form to add new venue
      Post["/venues/new"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue venue = new Venue(Request.Form["venue-name"], Request.Form["venue-city"]);
        venue.Save();
        model.Add("listBands", Band.GetAll());
        model.Add("listVenues", Venue.GetAll());
        model.Add("newVenue", venue);
        model.Add("show-info", "new-venue-info");
        return View["index.cshtml", model];
      }; //posts from form adding new venue
      Get["/bands/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band selectedBand = Band.Find(parameters.id);
        List<Venue> bandVenues = selectedBand.GetVenues();
        model.Add("band", selectedBand);
        model.Add("venues", bandVenues);
        return View["band.cshtml", model];
      }; //retrieves individual band pages
      Get["/venues/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue selectedVenue = Venue.Find(parameters.id);
        List<Band> venueBands = selectedVenue.GetBands();
        model.Add("venue", selectedVenue);
        model.Add("bands", venueBands);
        return View["venue.cshtml", model];
      }; //retrieves individual venue pages
      Get["/venue/{id}/edit"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue selectedVenue = Venue.Find(parameters.id);
        string venueEdit = Request.Query["venue-edit"];
        model.Add("form-type", venueEdit);
        model.Add("venue", selectedVenue);
        return View["edit.cshtml", model];
      }; //edit individual venue
      Patch["/venue/{id}/edit"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue selectedVenue = Venue.Find(parameters.id);
        selectedVenue.UpdateVenue(Request.Form["venue-name"], Request.Form["venue-city"]);
        List<Band> venueBands = selectedVenue.GetBands();
        model.Add("venue", selectedVenue);
        model.Add("bands", venueBands);
        return View["venue.cshtml", model];
      }; //returns edited venue page
      Get["venue/{id}/delete"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue selectedVenue = Venue.Find(parameters.id);
        string venueDelete = Request.Query["venue-delete"];
        model.Add("form-type", venueDelete);
        model.Add("venue", selectedVenue);
        return View["delete.cshtml", model];
      }; //delete individual venue
      Delete["venue/{id}/delete"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue selectedVenue = Venue.Find(parameters.id);
        selectedVenue.Delete();
        model.Add("listBands", Band.GetAll());
        model.Add("listVenues", Venue.GetAll());
        model.Add("show-info", null);
        return View["index.cshtml", model];
      }; //returns confirmation of deleted venue
    }
  }
}
