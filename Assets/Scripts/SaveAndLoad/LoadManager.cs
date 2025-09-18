using System.IO;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    string saveFilepath;
    string saveGlobalFilePath;
    string saveFileExtension;
    string saveKeyPath;
    string saveDataKey;
    string saveGeneralDataKey;

    [SerializeField]
    private PartyMember wpartyMember;

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

    private void Start()
    {
        /// <summary>
        /// GameDataに起動時のデータをセットする
        /// </summary>
        this.LoadGlobalData();
    }

    public bool LoadData(int saveNumber)
    {
        try
        {
            string saveDataPath = saveFilepath + saveNumber.ToString() + saveFileExtension;
            if (!ES3.FileExists(saveDataPath))
            {
                Debug.LogWarning("セーブファイルが見つかりません");
                return false;
            }
            string jsonData = ES3.Load<string>(saveDataKey, saveDataPath);

            SaveData LoadedData = JsonUtility.FromJson<SaveData>(jsonData);
            this.UpdateAllDataFromLoadedData(LoadedData);

            return true;
        }
        catch (IOException e)
        {
            Debug.Log(e);
            return false;
        }
    }

    private bool LoadGlobalData()
    {
        try
        {
            string saveGlobalFilePath = Application.dataPath + CONST.SAVE_AND_LOAD.GLOBALSAVEDATA_STRAGEPATH + CONST.SAVE_AND_LOAD.SAVEFILEEXTENTION;
            if (!ES3.FileExists(saveGlobalFilePath))
            {
                Debug.LogWarning("セーブファイルが見つかりません");
                return false;
            }
            string jsonData = ES3.Load<string>(CONST.SAVE_AND_LOAD.GENERALSAVEDATAKLEY, saveGlobalFilePath);

            GlobalSaveData LoadedData = JsonUtility.FromJson<GlobalSaveData>(jsonData);
            GameData.instance.UpdateLoadedData(LoadedData);

            return true;
        }
        catch (IOException e)
        {
            Debug.LogError(e);
            return false;
        }
    }

    private void UpdateAllDataFromLoadedData(SaveData loadData)
    {
        var charData = PlayerData.instance;
        var questData = QuestData.instance;

        // MasterData更新
        charData.UpdateLoadedData(loadData);
        questData.UpdataLoadedData(loadData);
        wpartyMember.UpdateCharParam(loadData.PartyMember[0]);

    }
}
