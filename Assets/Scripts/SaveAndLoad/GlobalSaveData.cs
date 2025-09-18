using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class SaveDataSummary
{
    public int saveNo;
    public int currentFloorNo;
    public string currentFloorName;
    public string charName;
    public CONST.CHARCTOR.Class charClass;
    public string savedTime;
}

[System.Serializable]
public class GlobalSaveData
{


    public List<SaveDataSummary> saveDataSummary = new();
    public int bgmValue;
    public int seValue;
}

