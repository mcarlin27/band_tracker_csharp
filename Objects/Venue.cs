using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.IO;

namespace BandTracker.Objects
{
  public class Venue
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }

    public Venue(string name, string city, int id = 0)
    {
      Id = id;
      Name = name;
      City = city;
    }

    public override bool Equals(System.Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = this.Id == newVenue.Id;
        bool nameEquality = this.Name == newVenue.Name;
        bool cityEquality = this.City == newVenue.City;
        return (idEquality && nameEquality && cityEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name, city) OUTPUT INSERTED.id VALUES (@VenueName, @VenueCity);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@VenueName";
      nameParameter.Value = this.Name;

      SqlParameter cityParameter = new SqlParameter();
      cityParameter.ParameterName = "@VenueCity";
      cityParameter.Value = this.City;

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(cityParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static List<Venue> GetAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Venue> venues = new List<Venue>{};
      while (rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        string venueCity = rdr.GetString(2);
        Venue newVenue = new Venue(venueName, venueCity, venueId);
        venues.Add(newVenue);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return venues;
    }

    public void UpdateVenue(string newName, string newCity)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @NewName, city = @NewCity OUTPUT INSERTED.name, INSERTED.city WHERE id = @VenueId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter newCityParameter = new SqlParameter();
      newCityParameter.ParameterName = "@NewCity";
      newCityParameter.Value = newCity;
      cmd.Parameters.Add(newCityParameter);

      SqlParameter VenueIdParameter = new SqlParameter();
      VenueIdParameter.ParameterName = "@VenueId";
      VenueIdParameter.Value = this.Id;
      cmd.Parameters.Add(VenueIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Name = rdr.GetString(0);
        this.City = rdr.GetString(1);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);
      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = id.ToString();
      cmd.Parameters.Add(venueIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundVenueId = 0;
      string foundVenueName = null;
      string foundVenueCity = null;
      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0);
        foundVenueName = rdr.GetString(1);
        foundVenueCity = rdr.GetString(2);
      }
      Venue foundVenue = new Venue(foundVenueName, foundVenueCity, foundVenueId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundVenue;
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
