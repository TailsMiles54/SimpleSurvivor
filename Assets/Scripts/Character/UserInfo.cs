using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Settings;
using UnityEngine;

[Serializable]
public class UserInfo
{
    public UserInfo(Wallet wallet, string name, Levels level, Parameters parameters)
    {
        Wallet = wallet;
        Name = name;
        Level = level;
        Parameters = parameters;
    }
    
    public Wallet Wallet { get; private set; }
    public string Name { get; private set; }
    public Levels Level { get; private set; }
    public Parameters Parameters { get; private set; }

    public void AddMainExp(int exp)
    {
        var mainLevel = Level.LevelList.First(x => x.LevelType == LevelType.MainLevel);
        mainLevel.AddExp(exp);
        SaveUserData();
    }

    public void SaveUserData()
    {
        SaveDataManager.SaveUserData(this);
    }
}

[Serializable]
public class Levels
{
    public Levels(List<Level> levels)
    {
        LevelList = levels;
    }
    
    public List<Level> LevelList;
}

[Serializable]
public class Level
{
    public Level(LevelType levelType)
    {
        LevelType = levelType;
        CurrentLevel = 1;
        MaxLevel = 99;
        CurrentExp = 0;
    }
    
    public LevelType LevelType {get; private set;}
    public int CurrentLevel {get; private set;}
    public int MaxLevel {get; private set;}
    public int CurrentExp {get; private set;}
    
    public int ExpToNext => SettingsProvider.Get<LevelSettings>().MainLevels[CurrentLevel].MaxExp;

    public void AddExp(int exp)
    {
        if (CurrentExp + exp >= ExpToNext)
        {
            exp -= ExpToNext - CurrentExp;
            LevelUp();
            AddExp(exp);
        }
        else
        {
            CurrentExp += exp;
        }
    }

    private void LevelUp()
    {
        CurrentLevel++;
        CurrentExp = 0;
    }
}

[Serializable]
public class Wallet
{
    public int Gems { get; private set; }
    public int Gold { get; private set; }
}

public enum LevelType
{
    MainLevel,
    JobLevel
}