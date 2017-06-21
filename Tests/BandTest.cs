using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace BandTracker.Objects
{
  [Collection("BandTracker")]
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
    [Fact]
    public void Test_BandDatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Band.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueForSameInfo()
    {
      //Arrange, Act
      Band firstBand = new Band("Modest Mouse");
      Band secondBand = new Band("Modest Mouse");
      //Assert
      Assert.Equal(firstBand, secondBand);
    }
    [Fact]
    public void Test_Save_SavesBandToDatabase()
    {
      //Arrange
      Band testBand = new Band("Modest Mouse");
      testBand.Save();
      //Act
      List<Band> result = Band.GetAll();
      List<Band> expectedResult = new List<Band>{testBand};
      //Assert
      Assert.Equal(result, expectedResult);
    }
    [Fact]
    public void Test_Save_AssignsIdToBandInDatabase()
    {
      //Arrange
      Band testBand = new Band("MOdest Mouse");
      testBand.Save();
      //Act
      Band savedBand = Band.GetAll()[0];
      int testId = testBand.Id;
      int expectedId = savedBand.Id;
      //Assert
      Assert.Equal(testId, expectedId);
    }
    [Fact]
    public void Test_Find_FindsBandInDatabase()
    {
      //Arrange
      Band testBand = new Band("Modest Mouse");
      testBand.Save();
      //Act
      Band foundBand = Band.Find(testBand.Id);
      //Assert
      Assert.Equal(testBand, foundBand);
    }
    [Fact]
    public void Test_UpdateBand_ReturnsTrueIfBandInfoIsTheSame()
    {
      //Arrange
      Band firstTestBand = new Band("Modest Mouse");
      firstTestBand.Save();
      Band secondTestBand = new Band("Rilo Kiley", firstTestBand.Id);
      //Act
      secondTestBand.UpdateBand("Modest Mouse");
      //Assert
      Assert.Equal(firstTestBand, secondTestBand);
    }
    [Fact]
    public void Test_Delete_ReturnsTrueIfListsAreTheSame()
    {
      //Arrange
      Band firstTestBand = new Band("Modest Mouse");
      firstTestBand.Save();
      Band secondTestBand = new Band("Rilo Kiley");
      secondTestBand.Save();
      Band thirdTestBand = new Band("Pink Floyd");
      thirdTestBand.Save();
      List<Band> expectedList = new List<Band>{firstTestBand, secondTestBand};
      //Act
      thirdTestBand.Delete();
      List<Band> resultList = Band.GetAll();
      //Assert
      Assert.Equal(resultList, expectedList);
    }
    [Fact]
    public void Test_AddVenue_AddVenueToBand()
    {
      //Arrange
      Venue newVenue = new Venue("Madison Square Garden", "NYC");
      newVenue.Save();
      Band testBand = new Band("Modest Mouse");
      testBand.Save();
      //Act
      testBand.AddVenue(newVenue);
      List<Venue> testBandVenues = testBand.GetVenues();
      List<Venue> expectedList = new List<Venue>{newVenue};
      //Assert
      Assert.Equal(expectedList, testBandVenues);
    }
  }
}
