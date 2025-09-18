using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class GameData : SerializedMonoBehaviour
{
    public static GameData instance;


    public List<SaveDataSummary> saveDataSummary = new();

    public int BGM_Volume = 8;
    public int SE_Volume = 8;


    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void UpdateAllSaveDataSummary(List<SaveDataSummary> summaryList)
    {
        this.saveDataSummary = summaryList;
    }

    public void UpdateSaveDataSummary(SaveDataSummary saveDataSummary)
    {
        this.saveDataSummary.Add(saveDataSummary);
    }

    public void UpdateBGMValue(int value)
    {
        this.BGM_Volume = value;
    }

    public void UpdateSEValue(int value)
    {
        this.SE_Volume = value;
    }

    public void InitializeOptionValue()
    {
        this.BGM_Volume = CONST.OPTION.OPTION_BGM_VALUE;
        this.SE_Volume = CONST.OPTION.OPTION_SE_VALUE;
    }

    public void UpdateLoadedData(GlobalSaveData globalSaveData)
    {
        this.saveDataSummary = globalSaveData.saveDataSummary;
        this.BGM_Volume = globalSaveData.bgmValue;
        this.SE_Volume = globalSaveData.seValue;
    }
}
