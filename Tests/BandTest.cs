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
      Band firstBand = new Band("Modest Mouse");
      Band secondBand = new Band("Modest Mouse");
      //Assert
      Assert.Equal(firstBand, secondBand);
    }
  }
}
