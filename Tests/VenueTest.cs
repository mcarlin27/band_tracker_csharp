using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace BandTracker.Objects
{
  [Collection("BandTracker")]
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Venue.DeleteAll();
    }
    [Fact]
    public void Test_VenueDatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueForSameInfo()
    {
      //Arrange, Act
      Venue firstVenue = new Venue("Madison Square Garden", "NYC");
      Venue secondVenue = new Venue("Madison Square Garden", "NYC");
      //Assert
      Assert.Equal(firstVenue, secondVenue);
    }
    [Fact]
    public void Test_Save_SavesVenueToDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Madison Square Garden", "NYC");
      testVenue.Save();
      //Act
      List<Venue> result = Venue.GetAll();
      List<Venue> expectedResult = new List<Venue>{testVenue};
      //Assert
      Assert.Equal(result, expectedResult);
    }
    [Fact]
    public void Test_Save_AssignsIdToVenueInDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Madison Square Garden", "NYC");
      testVenue.Save();
      //Act
      Venue savedVenue = Venue.GetAll()[0];
      int testId = testVenue.Id;
      int expectedId = savedVenue.Id;
      //Assert
      Assert.Equal(testId, expectedId);
    }
    [Fact]
    public void Test_Find_FindsVenueInDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Madison Square Garden", "NYC");
      testVenue.Save();
      //Act
      Venue foundVenue = Venue.Find(testVenue.Id);
      //Assert
      Assert.Equal(testVenue, foundVenue);
    }
    [Fact]
    public void Test_UpdateVenue_ReturnsTrueIfVenueInfoIsTheSame()
    {
      //Arrange
      Venue firstTestVenue = new Venue("Madison Square Garden", "NYC");
      firstTestVenue.Save();
      Venue secondTestVenue = new Venue("Crystal Ballroom", "Portland, OR", firstTestVenue.Id);
      //Act
      secondTestVenue.UpdateVenue("Madison Square Garden", "NYC");
      //Assert
      Assert.Equal(firstTestVenue, secondTestVenue);
    }
  }
}
