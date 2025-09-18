using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using System.Linq;
using UnityEngine.UI;

public class SaveAndLoadMenuManager : MonoBehaviour
{
    public SaveMenuUIManager saveMenuUIManager;
    public SaveMenuScrollManager scrollManager;
    public SaveManager saveManager;
    public LoadManager loadManager;

    [SerializeField]
    private MenuSelectionUtility gameObjectUtility = new MenuSelectionUtility();

    [SerializeField]
    private QuestManager _questManager;

    public List<RectTransform> slotObjs = new List<RectTransform>();

    public bool isSave;

    public void SetUpSaveAndLoadManager(bool isSave)
    {
        this.isSave = isSave;
        saveMenuUIManager.UpdateSaveMenuActive(true);
        slotObjs = scrollManager.SetUpSaveListUI();

        // 先頭スロットを選択状態に更新
        slotObjs[0].gameObject.GetComponent<SelectedStatus>().UpdateIsSelected(true);
        this.UpdateSelectedSlot(slotObjs[0].gameObject);
    }

    public void CloseSaveAndLoadMenu()
    {
        this.ClearSaveContent();
        saveMenuUIManager.UpdateSaveMenuActive(false);

        _questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.PlayerMainMenu);
    }

    public void ExecuteSave(int saveSlotNo, GameObject targetObj)
    {
        try
        {
            if (saveManager.SaveData(saveSlotNo))
            {
                // UI変更
                SaveDataSummary summary = GameData.instance.saveDataSummary.FirstOrDefault(s => s.saveNo == saveSlotNo);
                targetObj.GetComponent<SaveMenuSaveContentUIManager>().UpdateSaveContentUI(summary);
                targetObj.GetComponent<SaveMenuSaveContentUIManager>().UpdateIsEmptySave(false);
            }
        }
        catch
        {
            Debug.LogError("セーブに失敗しました。");
        }
    }

    public void ExecuteLoad(int saveSlotNo)
    {
        if (loadManager.LoadData(saveSlotNo))
        {
            // プレイヤーメニュー画面まで閉じる
            this.CloseSaveAndLoadMenu();
            _questManager.ClosePlayerMenu();
        }
        else
        {
            Debug.LogError("セーブNo_" + saveSlotNo.ToString() + ": ロードに失敗しました。");
        }
    }

    public void SaveAndLoadFromBasisKeyInput(CONST.MENU.SELECTEDTYPE selectedType)
    {
        gameObjectUtility.ClickedButtonsInALow(slotObjs.Select(i => i.gameObject).ToList(),
        selectedType);
    }

    public void UpdateSelectedSlot(GameObject gameObject)
    {
        // 選択状態を更新
        this.ClearAllSlotSelected(slotObjs.Select(i => i.gameObject).ToList());
        this.UpdateSlotSelected(gameObject);

        // スロットの表示位置を調整
        saveMenuUIManager.resetListScroll(gameObject.GetComponent<RectTransform>());


        saveMenuUIManager.InitializeAllSlotColor(slotObjs.Select(i => i.gameObject).ToList());
        saveMenuUIManager.UpdateSlotColor(gameObject);
    }

    private void ClearAllSlotSelected(List<GameObject> Slots)
    {
        foreach (var slot in Slots)
        {
            slot.gameObject.GetComponent<SelectedStatus>().UpdateIsSelected(false);
        }
    }

    private void UpdateSlotSelected(GameObject targetSlot)
    {
        targetSlot.gameObject.GetComponent<SelectedStatus>().UpdateIsSelected(true);
    }

    private void ClearSaveContent()
    {
        var items = GameObject.FindGameObjectsWithTag("PlayerMenuWroteButton");

        //表示しているボタンの削除 
        foreach (var button in items)
        {
            Destroy(button);

        }
    }
}
