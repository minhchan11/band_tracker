using Nancy;
using System;
using System.Collections.Generic;

namespace BandTrackerApp
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

      Get["/bands"] = _ =>{
        return View["bands.cshtml", ModelMaker()];
      };
      Post["/bands"] = _ =>{
        Band newBand = new Band(Request.Form["band-name"]);
        newBand.Save();
        newBand.AddVenue(Venue.Find(Request.Form["add-venue"]));
        return View["bands.cshtml", ModelMaker()];
      };

      Delete["/bands"] = _ => {
        Band.DeleteAll();
        return View["bands.cshtml", ModelMaker()];
      };

      Post["/bands/search"] = _ => {
        List<Band> foundBand = Band.SearchName(Request.Form["search-band"]);
        return View["search.cshtml", foundBand];
      };

      Get["/bands/{id}"] = parameters => {
        var foundBand = Band.Find(parameters.id);
        var foundBandVenues = foundBand.GetVenues();
        Dictionary<string, object> model = ModelMaker();
        model.Add("band",foundBand);
        model.Add("band-venues", foundBandVenues);
        return View["band.cshtml", model];
      };

      Delete["/band/{id}/deleted"] = parameters => {
        Band foundBand = Band.Find(parameters.id);
        foundBand.DeleteThis();
        return View["bands.cshtml", ModelMaker()];
      };

      Patch["band/edit/{id}"] = parameters => {
        Band foundBand = Band.Find(parameters.id);
        foundBand.Update(Request.Form["band-name"]);
        var foundBandVenues = foundBand.GetVenues();
        Dictionary<string, object> model = ModelMaker();
        model.Add("band",foundBand);
        model.Add("band-venues", foundBandVenues);
        return View["band.cshtml", model];
      };

      Patch["band/addVenue/{id}"] = parameters => {
        var foundBand = Band.Find(parameters.id);
        foundBand.AddVenue(Venue.Find(Request.Form["add-new-venue"]));
        var foundBandVenues = foundBand.GetVenues();
        Dictionary<string, object> model = ModelMaker();
        model.Add("band",foundBand);
        model.Add("band-venues", foundBandVenues);
        return View["band.cshtml", model];
      };

      Delete["band/removeVenue/{id}"] = parameters => {
        Band foundBand = Band.Find(parameters.id);
        foundBand.DeleteVenue(Venue.Find(Request.Form["remove-venue"]));
        var foundBandVenues = foundBand.GetVenues();
        Dictionary<string, object> model = ModelMaker();
        model.Add("band",foundBand);
        model.Add("band-venues", foundBandVenues);
        return View["band.cshtml", model];
      };

      Delete["band/removeVenues/{id}"] = parameters => {
        Band foundBand = Band.Find(parameters.id);
        foundBand.DeleteVenues();
        Dictionary<string, object> model = ModelMaker();
        var foundBandVenues = foundBand.GetVenues();
        model.Add("band",foundBand);
        model.Add("band-venues", foundBandVenues);
        return View["band.cshtml", model];
      };

      Get["/venues"] = _ =>{
        return View["venues.cshtml", ModelMaker()];
      };

      Post["/venues"] = _ =>{
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
        return View["venues.cshtml", ModelMaker()];
      };

      Delete["/venues"] = _ => {
        Band.DeleteAll();
        return View["venues.cshtml", ModelMaker()];
      };

      Post["/venues/search"] = _ => {
        List<Venue> foundVenue = Venue.SearchName(Request.Form["search-venue"]);
        return View["search.cshtml", foundVenue];
      };

      Get["/venues/{id}"] = parameters => {
        var foundVenue = Venue.Find(parameters.id);
        var foundVenueBands = foundVenue.GetBands();
        Dictionary<string, object> model = ModelMaker();
        model.Add("venue",foundVenue);
        model.Add("venue-bands", foundVenueBands);
        return View["venue.cshtml", model];
      };

      Delete["/venue/{id}/deleted"] = parameters => {
        Venue foundVenue = Venue.Find(parameters.id);
        foundVenue.DeleteThis();
        return View["venues.cshtml", ModelMaker()];
      };

      Patch["venue/edit/{id}"] = parameters => {
        Venue foundVenue = Venue.Find(parameters.id);
        foundVenue.Update(Request.Form["venue-name"]);
        var foundVenueBands = foundVenue.GetBands();
        Dictionary<string, object> model = ModelMaker();
        model.Add("venue",foundVenue);
        model.Add("venue-bands", foundVenueBands);
        return View["venue.cshtml", model];
      };

      Patch["venue/addBand/{id}"] = parameters => {
        var foundVenue = Venue.Find(parameters.id);
        foundVenue.AddBand(Band.Find(Request.Form["add-new-band"]));
        var foundVenueBands = foundVenue.GetBands();
        Dictionary<string, object> model = ModelMaker();
        model.Add("venue",foundVenue);
        model.Add("venue-bands", foundVenueBands);
        return View["venue.cshtml", model];
      };

      Delete["venue/removeBand/{id}"] = parameters => {
        Venue foundVenue = Venue.Find(parameters.id);
        foundVenue.DeleteBand(Band.Find(Request.Form["remove-band"]));
        var foundVenueBands = foundVenue.GetBands();
        Dictionary<string, object> model = ModelMaker();
        model.Add("venue",foundVenue);
        model.Add("venue-bands", foundVenueBands);
        return View["venue.cshtml", model];
      };

      Delete["venue/removeBands/{id}"] = parameters => {
        Venue foundVenue = Venue.Find(parameters.id);
        foundVenue.DeleteBands();
        Dictionary<string, object> model = ModelMaker();
        var foundVenueBands = foundVenue.GetBands();
        model.Add("venue",foundVenue);
        model.Add("venue-bands", foundVenueBands);
        return View["venue.cshtml", model];
      };
    }
    public static Dictionary<string, object> ModelMaker()
    {
      Dictionary<string, object> model = new Dictionary<string, object>
      {
        {"Bands", Band.GetAll()},
        {"Venues", Venue.GetAll()},
      };
      return model;
    }
  }
}
