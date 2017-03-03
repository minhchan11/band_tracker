using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTrackerApp
{
  public class Venue
  {
    private int _id;
    private string _name;

    public Venue(string venueName, int venueId = 0)
    {
      _id = venueId;
      _name = venueName;
    }

    public int GetId()
    {
      return _id;
    }
    public void SetId(int newId)
    {
      _id = newId;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public static void DeleteAll()
    {
      DB.DeleteAll("venues");
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        Venue newVenue = new Venue(venueName, venueId);
        allVenues.Add(newVenue);
      }

      DB.CloseSqlConnection(conn, rdr);
      return allVenues;
    }

    public override bool Equals(System.Object randomVenue)
    {
      if(!(randomVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) randomVenue;
        bool idEquality = (this.GetId() == newVenue.GetId());
        bool nameEquality = (this.GetName() == newVenue.GetName());
        return (idEquality && nameEquality);
      }
    }

    public void Save()
    {
      int potentialId = this.IsNewVenue();
      if (potentialId == -1)
      {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name) OUTPUT INSERTED.id VALUES (@VenueName) ;", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueName", this.GetName()));
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        potentialId = rdr.GetInt32(0);
      }

      DB.CloseSqlConnection(conn, rdr);
      }
      this.SetId(potentialId);
    }

    public void DeleteThis()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @TargetId; DELETE FROM bands_venues WHERE venue_id = @TargetId;", conn);
      cmd.Parameters.Add(new SqlParameter("@TargetId", this.GetId()));

      cmd.ExecuteNonQuery();
      DB.CloseSqlConnection(conn);
    }

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @NewName WHERE id = @TargetId;", conn);
      cmd.Parameters.Add(new SqlParameter("@NewName", newName));
      cmd.Parameters.Add(new SqlParameter("@TargetId", this.GetId()));
      cmd.ExecuteNonQuery();

      this.SetName(newName);
      DB.CloseSqlConnection(conn);
    }

    public int IsNewVenue()
    {
      // Checks to see if an ingredient exists in the database already. If it does, returns the id. Otherwise, returns -1
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT id FROM venues WHERE name = @VenueName", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueName", this.GetName()));
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = -1;
      while (rdr.Read())
      {
        foundId = rdr.GetInt32(0);
      }

      DB.CloseSqlConnection(conn, rdr);
      return foundId;
    }

    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueId", id.ToString()));
      SqlDataReader rdr = cmd.ExecuteReader();

      int venueId = 0;
      string venueName = null;

      while (rdr.Read())
      {
        venueId = rdr.GetInt32(0);
        venueName = rdr.GetString(1);
      }

      Venue foundVenue = new Venue(venueName, venueId);

      DB.CloseSqlConnection(conn, rdr);
      return foundVenue;
    }

    public static List<Venue> SearchName(string venueName)
    {
      List<Venue> foundVenues = new List<Venue>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE name LIKE @VenueName", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueName", "%" + venueName + "%"));
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string newName = rdr.GetString(1);
        Venue foundVenue = new Venue(newName, venueId);
        foundVenues.Add(foundVenue);
      }

      DB.CloseSqlConnection(conn, rdr);
      return foundVenues;
    }

    public void AddBand(Band newBand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);
      cmd.Parameters.Add(new SqlParameter("@BandId", newBand.GetId().ToString()));
      cmd.Parameters.Add(new SqlParameter("@VenueId", this.GetId().ToString()));
      cmd.ExecuteNonQuery();

      DB.CloseSqlConnection(conn);
    }

    public void DeleteBand(Band newBand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM bands_venues (band_id, venue_id) WHERE band_id = @BandId AND venue_id = @VenueId;", conn);
      cmd.Parameters.Add(new SqlParameter("@BandId", newBand.GetId().ToString()));
      cmd.Parameters.Add(new SqlParameter("@VenueId", this.GetId().ToString()));
      cmd.ExecuteNonQuery();

      DB.CloseSqlConnection(conn);
    }

    public void DeleteBands()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM bands_venues (venue_id, venue_id) WHERE venue_id = @VenueId;", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueId", this.GetId().ToString()));
      cmd.ExecuteNonQuery();

      DB.CloseSqlConnection(conn);
    }

    public List<Band> GetBands()
    {
      List<Band> allBands = new List<Band>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM bands JOIN bands_venues ON (bands.id = bands_venues.band_id) JOIN venues ON (venues.id = bands_venues.venue_id) WHERE venues.id = @VenueId;", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueId", this.GetId().ToString()));
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int newId = rdr.GetInt32(0);
        string newName = rdr.GetString(1);
        Band newBand = new Band(newName, newId);
        allBands.Add(newBand);
      }

      DB.CloseSqlConnection(conn, rdr);
      return allBands;
    }
  }
}
