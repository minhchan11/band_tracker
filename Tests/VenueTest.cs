using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;


namespace BandTrackerApp
{
  public class VenueTest: IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Venue.DeleteAll();
      Venue.DeleteAll();
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_ZeroOutput()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void OverrideBool_SameVenue_ReturnsEqual()
    {
      //Arrange, Act
      Venue venueOne = new Venue ("Peasant");
      Venue venueTwo = new Venue ("Peasant");

      //Assert
      Assert.Equal(venueTwo, venueOne);
    }

    [Fact]
    public void Save_OneVenue_VenueSavedToDatabase()
    {
      //Arrange
      Venue testVenue = new Venue ("Peasant");

      //Act
      testVenue.Save();
      List<Venue> output = Venue.GetAll();
      List<Venue> verify = new List<Venue>{testVenue};

      //Assert
      Assert.Equal(verify, output);
    }

    [Fact]
    public void Save_OneVenue_VenueSavedWithCorrectID()
    {
      //Arrange
      Venue testVenue = new Venue ("Peasant");
      testVenue.Save();
      Venue savedVenue = Venue.GetAll()[0];

      //Act
      int output = savedVenue.GetId();
      int verify = testVenue.GetId();

      //Assert
      Assert.Equal(verify, output);
    }

    [Fact]
    public void SaveGetAll_ManyVenues_ReturnListOfVenues()
    {
      //Arrange
      Venue venueOne = new Venue ("Peasant");
      venueOne.Save();
      Venue venueTwo = new Venue ("Delicious");
      venueTwo.Save();

      //Act
      List<Venue> output = Venue.GetAll();
      List<Venue> verify = new List<Venue>{venueOne, venueTwo};

      //Assert
      Assert.Equal(verify, output);
    }

    [Fact]
    public void Find_OneVenueId_ReturnVenueFromDatabase()
    {
      //Arrange
      Venue testVenue = new Venue ("Peasant");
      testVenue.Save();

      //Act
      Venue foundVenue = Venue.Find(testVenue.GetId());

      //Assert
      Assert.Equal(testVenue, foundVenue);
    }

    [Fact]
    public void SearchName_Name_ReturnVenueFromDatabase()
    {
      //Arrange
      Venue testVenue = new Venue ("Peasant");
      testVenue.Save();

      //Act
      List<Venue> output = Venue.SearchName("Peasant");
      List<Venue> verify = new List<Venue>{testVenue};

      //Assert
      Assert.Equal(verify, output);
    }

    [Fact]
    public void AddBand_OneBand_BandAddedToJoinTable()
    {
      //Arrange
      Venue testVenue = new Venue ("Peasant");
      testVenue.Save();
      Band testBand = new Band("Pot Pie");
      testBand.Save();
      testVenue.AddBand(testBand);

      //Act
      List<Band> output = testVenue.GetBands();
      List<Band> verify = new List<Band>{testBand};

      //Assert
      Assert.Equal(verify, output);
    }

    [Fact]
    public void Venue_Delete_RemoveObjectFromDatabase()
    {
      Venue testVenue = new Venue ("Peasant");
      testVenue.Save();

      testVenue.DeleteThis();

      Assert.Equal(0, Venue.GetAll().Count);
    }

    [Fact]
    public void Venue_Update_UpdateDatabaseAndLocalObject()
    {
      Venue testVenue = new Venue ("Peasant");
      testVenue.Save();

      testVenue.Update("Ultra Poor");
      Venue expectedVenue = new Venue("Ultra Poor", testVenue.GetId());

      Assert.Equal(expectedVenue, Venue.Find(testVenue.GetId()));
    }

    [Fact]
    public void Venue_Save_NoSaveOnDuplicateVenue()
    {
      Venue testVenue = new Venue ("Peasant");
      testVenue.Save();
      Venue secondVenue = new Venue ("Peasant");
      secondVenue.Save();

      Assert.Equal(1, Venue.GetAll().Count);
    }


  }
}
