using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveManager : MonoBehaviour
{
    string saveFilepath;
    string saveGlobalFilePath;
    string saveFileExtension;
    string saveKeyPath;
    string saveDataKey;
    string saveGeneralDataKey;

    private void Awake()
    {
        // パス名取得
        saveFilepath = Application.dataPath + CONST.SAVE_AND_LOAD.SAVEFILE_STRAGEPATH;
        saveGlobalFilePath = Application.dataPath + CONST.SAVE_AND_LOAD.GLOBALSAVEDATA_STRAGEPATH;
        saveFileExtension = CONST.SAVE_AND_LOAD.SAVEFILEEXTENTION;
        saveKeyPath = CONST.SAVE_AND_LOAD.SAVEKEYPATH;
        saveDataKey = CONST.SAVE_AND_LOAD.SAVEDATAKLEY;
        saveGeneralDataKey = CONST.SAVE_AND_LOAD.GENERALSAVEDATAKLEY;
    }

    public bool SaveData(int saveNumber)
    {
        try
        {
            var charData = PlayerData.instance;
            var questData = QuestData.instance;
            var encrypter = new Encrypter();
            var keyDecorder = new KeyDecorder();

            SaveData targetSaveData = this.CreateSaveData(charData, questData);

            // Save実行
            string saveDataPath = saveFilepath + saveNumber.ToString() + saveFileExtension;
            string saveDataJson = JsonUtility.ToJson(targetSaveData);
            ES3.Save<String>(saveDataKey, saveDataJson, saveDataPath);
            Debug.Log("セーブ完了: " + saveDataPath);

            // グローバルファイルのセーブ
            SaveDataSummary summary = this.CreateSaveDataSummary(charData, questData, saveNumber);
            GameData.instance.UpdateSaveDataSummary(summary);
            GlobalSaveData tagetGlobalSaveData = this.CreateGlobalSaveData(GameData.instance);

            string globalSaveDataPath = saveGlobalFilePath + saveFileExtension;
            string globalSaveDataJson = JsonUtility.ToJson(tagetGlobalSaveData);
            ES3.Save<String>(saveGeneralDataKey, globalSaveDataJson, globalSaveDataPath);
            Debug.Log("グローバルデータのセーブ完了: " + globalSaveDataPath);

            return true;
        }
        catch (IOException e)
        {
            Debug.Log(e);
            return false;
        }
    }

    public bool IsSaveDataExist(int saveSlotNumber)
    {
        string failPath = saveFilepath + saveSlotNumber.ToString() + saveFileExtension;
        var result = ES3.FileExists(failPath);
        return result;
    }

    private SaveDataSummary CreateSaveDataSummary(PlayerData charData, QuestData questData, int saveNo)
    {
        return new SaveDataSummary()
        {
            saveNo = saveNo,
            currentFloorNo = questData.currentFloor,
            currentFloorName = questData.currentFloorName,
            charName = charData.PartyMember[0].Name,
            charClass = charData.PartyMember[0].charClass,
            // TODO: 一旦仮の値とする
            savedTime = "HH:MM:SS"
        };
    }

    private GlobalSaveData CreateGlobalSaveData(GameData gameData)
    {
        return new GlobalSaveData()
        {
            saveDataSummary = gameData.saveDataSummary,
            bgmValue = gameData.BGM_Volume,
            seValue = gameData.SE_Volume,

        };
    }

    private SaveData CreateSaveData(PlayerData charData, QuestData questData)
    {
        SaveData targetSaveData = new SaveData();
        targetSaveData.PartyMember = charData.PartyMember;
        targetSaveData.HaveItemList = charData.HaveItemList;
        targetSaveData.HasMoney = charData.HasMoney.getHasMoney();
        targetSaveData.currentFloor = questData.currentFloor;
        targetSaveData.currentFloorName = questData.currentFloorName;
        targetSaveData.currentCardList = questData.currentCardList;
        targetSaveData.canSelectCardNumber = questData.canSelectCardNumber;

        return targetSaveData;
    }
}
