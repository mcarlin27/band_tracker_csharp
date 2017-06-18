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
  }
}
