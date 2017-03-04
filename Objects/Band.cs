using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTrackerApp
{
  public class Band
  {
    private int _id;
    private string _name;

    public Band(string bandName, int bandId = 0)
    {
      _id = bandId;
      _name = bandName;
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


  }
}
