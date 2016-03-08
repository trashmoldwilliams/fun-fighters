using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Fighters
{
  public class Fighter
  {
    private int _id;
    private string _name;
    private int _wins;
    private int _losses;
    private string _images;
    private double _hp;
    private double _maxhp;
    private double _mp;
    private double _maxmp;
    private double _attack;
    private double _speed;
    private double _accuracy;
    private double _luck;

    // private Move _currentMove;
    private double _defense;
    private double _burn;

    public Fighter(string Name, string Image, double Hp, double Mp, double Attack, double Speed, double Accuracy, double Luck, int Wins = 0, int Losses = 0, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _hp = Hp;
      _maxhp = Hp;
      _mp = Mp;
      _maxmp = Mp;
      _images = Image;
      _attack = Attack;
      _speed = Speed;
      _accuracy = Accuracy;
      _luck = Luck;
      _wins = Wins;
      _losses = Losses;

      // _battleId = ;
      _defense = 1;
      _burn = 0;
    }

    public override bool Equals(System.Object otherFighter)
    {
        if (!(otherFighter is Fighter))
        {
          return false;
        }
        else
        {
          Fighter newFighter = (Fighter) otherFighter;
          bool idEquality = this.GetId() == newFighter.GetId();
          bool nameEquality = this.GetName() == newFighter.GetName();
          bool imageEquality = this.GetImage() == newFighter.GetImage();
          bool hpEquality = this.GetHp() == newFighter.GetHp();
          bool mpEquality = this.GetMp() == newFighter.GetMp();
          bool attackEquality = this.GetAttack() == newFighter.GetAttack();
          bool speedEquality = this.GetSpeed() == newFighter.GetSpeed();
          bool accuracyEquality = this.GetAccuracy() == newFighter.GetAccuracy();
          bool luckEquality = this.GetLuck() == newFighter.GetLuck();
          bool maxHpEquality = this.GetMaxHp() == newFighter.GetMaxHp();
          bool maxMpEquality = this.GetMaxMp() == newFighter.GetMaxMp();
          return (idEquality && nameEquality && imageEquality && hpEquality && mpEquality && speedEquality && attackEquality && accuracyEquality && luckEquality && maxMpEquality && maxHpEquality);
        }
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetImage()
    {
      return _images;
    }
    public double GetHp()
    {
      return _hp;
    }
    public double GetMaxHp()
    {
      return _maxhp;
    }
    public double GetMp()
    {
      return _mp;
    }
    public double GetMaxMp()
    {
      return _maxmp;
    }
    public double GetAttack()
    {
      return _attack;
    }
    public double GetSpeed()
    {
      return _speed;
    }
    public double GetAccuracy()
    {
      return _accuracy;
    }
    public double GetLuck()
    {
      return _luck;
    }
    public double GetDefense()
    {
      return _defense;
    }
    public int GetWins()
    {
      return _wins;
    }
    public int GetLosses()
    {
      return _losses;
    }

    public void SetHp(double Hp)
    {
      _hp = Hp;
    }

    public void SetDefense(double Defense)
    {
      _defense = Defense;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO fighters (name, image, hp, mp, attack, speed, accuracy, luck, wins, losses) OUTPUT INSERTED.id VALUES (@fighterName, @fighterImage, @fighterHp, @fighterMp, @fighterAttack, @fighterSpeed, @fighterAccuracy, @fighterLuck, @fighterWins, @fighterLoss)", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@fighterName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      SqlParameter ImageParameter = new SqlParameter();
      ImageParameter.ParameterName = "@fighterImage";
      ImageParameter.Value = this.GetImage();
      cmd.Parameters.Add(ImageParameter);

      SqlParameter hpParameter = new SqlParameter();
      hpParameter.ParameterName = "@fighterHp";
      hpParameter.Value = this.GetHp();
      cmd.Parameters.Add(hpParameter);

      SqlParameter mpParameter = new SqlParameter();
      mpParameter.ParameterName = "@fighterMp";
      mpParameter.Value = this.GetMp();
      cmd.Parameters.Add(mpParameter);

      SqlParameter attackParameter = new SqlParameter();
      attackParameter.ParameterName = "@fighterAttack";
      attackParameter.Value = this.GetAttack();
      cmd.Parameters.Add(attackParameter);

      SqlParameter speedParameter = new SqlParameter();
      speedParameter.ParameterName = "@fighterSpeed";
      speedParameter.Value = this.GetSpeed();
      cmd.Parameters.Add(speedParameter);

      SqlParameter accuracyParameter = new SqlParameter();
      accuracyParameter.ParameterName = "@fighterAccuracy";
      accuracyParameter.Value = this.GetAccuracy();
      cmd.Parameters.Add(accuracyParameter);

      SqlParameter luckParameter = new SqlParameter();
      luckParameter.ParameterName = "@fighterLuck";
      luckParameter.Value = this.GetLuck();
      cmd.Parameters.Add(luckParameter);

      SqlParameter winsParameter = new SqlParameter();
      winsParameter.ParameterName = "@fighterWins";
      winsParameter.Value = this.GetWins();
      cmd.Parameters.Add(winsParameter);

      SqlParameter lossParameter = new SqlParameter();
      lossParameter.ParameterName = "@fighterLoss";
      lossParameter.Value = this.GetLosses();
      cmd.Parameters.Add(lossParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM fighters;", conn);
      cmd.ExecuteNonQuery();
    }

    public static Fighter Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM fighters WHERE id = @fighterId;", conn);
      SqlParameter FighterIdParameter = new SqlParameter();
      FighterIdParameter.ParameterName = "@fighterId";
      FighterIdParameter.Value = id.ToString();
      cmd.Parameters.Add(FighterIdParameter);
      rdr = cmd.ExecuteReader();

      int foundFighterId = 0;
      string foundFighterName = null;
      int foundWins = 0;
      int foundLosses = 0;
      string foundImages =  null;
      int foundHp = 0;
      int foundMp = 0;
      int foundAttack = 0;
      int foundSpeed = 0;
      int foundAccuracy = 0;
      int foundLuck = 0;

      while(rdr.Read())
      {
        foundFighterId = rdr.GetInt32(0);
        foundFighterName = rdr.GetString(1);
        foundWins = rdr.GetInt32(2);
        foundLosses = rdr.GetInt32(3);
        foundImages =  rdr.GetString(4);
        foundHp = rdr.GetInt32(5);
        foundMp = rdr.GetInt32(6);
        foundAttack = rdr.GetInt32(7);
        foundSpeed = rdr.GetInt32(8);
        foundAccuracy = rdr.GetInt32(9);
        foundLuck = rdr.GetInt32(10);
      }
      Fighter foundFighter = new Fighter(foundFighterName, foundImages, foundHp, foundMp, foundAttack, foundSpeed, foundAccuracy, foundAccuracy, foundWins, foundLosses, foundFighterId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundFighter;
    }

    public void UpdateRecord(string outcome)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE fighters SET @model = @model + 1 WHERE id = @fighters;", conn);

      SqlParameter outcomeParameter= new SqlParameter();
      outcomeParameter.ParameterName = "@model";
      outcomeParameter.Value = outcome;
      cmd.Parameters.Add(outcomeParameter);

      SqlParameter fighterIdParameter = new SqlParameter();
      fighterIdParameter.ParameterName = "@fighters";
      fighterIdParameter.Value = this.GetId();
      cmd.Parameters.Add(fighterIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
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
    public static List<Fighter> GetAll()
    {
      List<Fighter> allFighters = new List<Fighter>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM fighters;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int foundFighterId = rdr.GetInt32(0);
        string foundFighterName = rdr.GetString(1);
        int foundWins = rdr.GetInt32(2);
        int foundLosses = rdr.GetInt32(3);
        string foundImages =  rdr.GetString(4);
        int foundHp = rdr.GetInt32(5);
        int foundMp = rdr.GetInt32(6);
        int foundAttack = rdr.GetInt32(7);
        int foundSpeed = rdr.GetInt32(8);
        int foundAccuracy = rdr.GetInt32(9);
        int foundLuck = rdr.GetInt32(10);
        Fighter foundFighter = new Fighter(foundFighterName, foundImages, foundHp, foundMp, foundAttack, foundSpeed, foundAccuracy, foundAccuracy, foundWins, foundLosses, foundFighterId);
        allFighters.Add(foundFighter);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allFighters;
    }
    public void DeleteFighter()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM fighters WHERE id = @FighterId;", conn);
      SqlParameter fighterIdParameter = new SqlParameter();
      fighterIdParameter.ParameterName = "@FighterId";
      fighterIdParameter.Value = this.GetId();

      cmd.Parameters.Add(fighterIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
