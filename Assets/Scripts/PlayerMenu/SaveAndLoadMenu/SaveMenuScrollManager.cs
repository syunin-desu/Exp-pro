using NUnit.Framework;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class SaveMenuScrollManager : MonoBehaviour
{
    [SerializeField]
    RectTransform SaveContent;

    [SerializeField]
    public SaveManager saveManager;

    void Start()
    {
        SaveContent.gameObject.SetActive(false);
    }

    public List<RectTransform> SetUpSaveListUI()
    {
        var result = new List<RectTransform>();
        // リストの初期表示
        for (int count = 0; count < CONST.SAVE_AND_LOAD.SAVE_SLOT_COUNT; count++)
        {
            var saveSlot = GameObject.Instantiate(SaveContent) as RectTransform;
            saveSlot.SetParent(transform, false);

            int saveSlotCount = count + 1;
            saveSlot.GetComponent<SaveMenuSaveContentButton>().saveSlotNo = saveSlotCount;

            // セーブデータがすでに存在するかのチェック
            if (this.saveManager.IsSaveDataExist(saveSlotCount))
            {
                // サマリーにデータが存在するかチェック
                SaveDataSummary summary = GameData.instance.saveDataSummary.FirstOrDefault(s => s.saveNo == saveSlotCount);
                if (summary is null)
                {
                    saveSlot.GetComponent<SaveMenuSaveContentUIManager>().UpdateIsEmptySave(true);
                    continue;
                }
                saveSlot.GetComponent<SaveMenuSaveContentUIManager>().UpdateSaveContentUI(summary);
                saveSlot.GetComponent<SaveMenuSaveContentUIManager>().UpdateIsEmptySave(false);

            }
            else
            {
                saveSlot.GetComponent<SaveMenuSaveContentUIManager>().UpdateIsEmptySave(true);
            }

            saveSlot.GetComponent<SaveMenuSaveContentUIManager>().UpdateSaveSlotNo(saveSlotCount);

            saveSlot.gameObject.SetActive(true);

            result.Add(saveSlot);
        }

        return result;
    }
}
