using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Fighters
{
  public class BattleTest
  {
    public BattleTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Fun_Fighters_Test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_Punches()
    {
      Fighter firstFighter = new Fighter("Midas",1,12,4,100,3,50,100);
      Fighter SecondFighter = new Fighter("Midas",1,12,4,5,0,10,100);

      Punch jab = new Punch(1, "JAB", -0.5, 100);
      Punch hook = new Punch(1, "HOOK", -1, 65);
      Punch uppercut = new Punch(1, "UPPERCUT", -2, 30);

      Battle currentBattle = new Battle(firstFighter, SecondFighter);

      // double output = currentBattle.ExecuteLockon(firstFighter);

      currentBattle.ExecuteLockon(firstFighter);

      Assert.Equal(72, firstFighter.GetAccuracy());
    }
  }
}
