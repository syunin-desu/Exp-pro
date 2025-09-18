using CONST;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using static CONST.EQUIP;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class EquipMenuManager : MonoBehaviour
{
    [SerializeField]
    private QuestManager _questManager;
    public EquipMenuUIManager equipMenuUIManager;
    public EquipMenuScrollManager equipMenuScrollManager;

    [SerializeField]
    private CONST.EQUIP_MENU_STATUS.MenuStatus currentStatus;

    public EquipMenuStatusUiManager equipMenuStatusUiManager;

    public PartyMember wpartyMember;
    public HadItem hadItem;

    public ItemManager itemManager;

    public List<GameObject> equipPartsButtonObj;

    public List<RectTransform> equipObjList;

    public EquipMenuDescriptionUI equipMenuDescriptionUI;

    private MenuSelectionUtility gameObjectUtility = new MenuSelectionUtility();

    private void Update()
    {
        PlayerEquipData targetEquipList = wpartyMember.GetArmedEquip();
        equipMenuUIManager.UpdateArmedEquipName(targetEquipList);
    }

    public void ShowEquipMenu()
    {
        _questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.EquipMenu);
        this.currentStatus = CONST.EQUIP_MENU_STATUS.MenuStatus.SelectEquipParts;
        equipMenuUIManager.ShowEquipMenu();
        var hasItemList = hadItem.GetHavingItem();
        PlayerEquipData targetEquipList = wpartyMember.GetArmedEquip();
        equipMenuScrollManager.SetupEquipListUI(targetEquipList);

        // 初期表示、Wepon部位を選択状態にする
        equipPartsButtonObj[0].GetComponent<EquipMenuArmedPartsButtonClicked>().OnClicked();


        equipMenuStatusUiManager.UpdateActive(true);
    }

    public void CloseEquipMenu()
    {
        this.ClearEquipPartsButtonSelected(equipPartsButtonObj);
        this.InitializeBeforeEquipUpdateParameter();
        this.equipMenuScrollManager.ClearHadEquipList(equipObjList.Select(e => e.GetComponent<GameObject>()).ToList());
        equipMenuUIManager.CloseEquipMenu();
        _questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.PlayerMainMenu);

        // PlayerDataを更新
        PlayerData.instance.UpdatePartyMemberParam(wpartyMember.GetCharParameters());
    }

    public void ClearEquipPartsButtonSelected(List<GameObject> targetObjs)
    {

        foreach (var item in targetObjs)
        {
            item.GetComponent<SelectedStatus>().UpdateIsSelected(false);
        }
    }

    public PartyMember GetBeforeEquipUpdatedParameter(string itemID, CONST.ITEM.CATEGORY partsCategory)
    {
        // 装備一時枠の初期化
        this.InitializeBeforeEquipUpdateParameter();
        this.wpartyMember.UpdateBeforeUpdateEquip(this.wpartyMember.GetArmedEquip());

        // 装備を更新
        EquipBase targetEquip = itemManager.GetEquipDataFromMaster(itemID);
        if (targetEquip is null)
        {
            return new PartyMember();
        }
        // 装備の一時選択枠に登録する
        EquipBase equipedItem = this.wpartyMember.UpdateEquip(partsCategory,
            targetEquip,
            this.GetCurrentSelectedArmedParts(equipPartsButtonObj),
            true);
        return this.wpartyMember;
    }

    public void InitializeBeforeEquipUpdateParameter()
    {
        var NoneEquips = itemManager.GetAllNoneEquip();
        var NoneEquipData = new PlayerEquipData()
        {
            weaponData = NoneEquips.FirstOrDefault(e => e.category == CONST.ITEM.CATEGORY.WEAPON_ITEM) as WeaponData,
            armedBody = NoneEquips.FirstOrDefault(e => e.category == CONST.ITEM.CATEGORY.BODY_EQUIP_ITEM) as BodyData,
            armedHead = NoneEquips.FirstOrDefault(e => e.category == CONST.ITEM.CATEGORY.HEAD_EQUIP_ITEM) as HeadData,
            armedAccessory_1 = NoneEquips.FirstOrDefault(e => e.category == CONST.ITEM.CATEGORY.ACCESSORY_ITEM) as AccessoryData,
            armedAccessory_2 = NoneEquips.FirstOrDefault(e => e.category == CONST.ITEM.CATEGORY.ACCESSORY_ITEM) as AccessoryData,
        };
        this.wpartyMember.UpdateBeforeUpdateEquip(NoneEquipData);
    }

    public void EquipButtonFromBasisKeyInput(CONST.MENU.SELECTEDTYPE selectedType)
    {
        switch (this.currentStatus)
        {
            case CONST.EQUIP_MENU_STATUS.MenuStatus.SelectEquipParts:
                gameObjectUtility.ClickedButtonsInALow(equipPartsButtonObj,
                    selectedType
                    );
                break;
            case CONST.EQUIP_MENU_STATUS.MenuStatus.SelectEquip:
                gameObjectUtility.ClickedButtonsInALow(equipObjList.Select(i => i.gameObject).ToList(),
                selectedType);
                break;
        }
    }


    public void UpdateArmedEquip(string itemID, CONST.ITEM.CATEGORY partsCategory)
    {
        // 装備を更新
        EquipBase targetEquip = itemManager.GetEquipDataFromMaster(itemID);
        if (targetEquip is null)
        {
            return;
        }
        // 本チャン枠に登録する
        EquipBase equipedItem = this.wpartyMember.UpdateEquip(partsCategory,
            targetEquip,
            this.GetCurrentSelectedArmedParts(this.equipPartsButtonObj),
            false);

        if (targetEquip.Name != "None")
        {
            this.hadItem.reduceItemCount(itemID, 1);
        }

        // アイテムから装備更新したアイテムの削除と
        // 装備していたアイテムを追加
        if (equipedItem.Name != "None")
        {
            this.hadItem.AddItem(new HavingItem()
            {
                id = equipedItem.id,
                Name = equipedItem.Name,
                category = equipedItem.category,
                count = 1, // 複数を同タイミングで着脱する予定はないため一つとする
            });
        }
    }

    public CONST.EQUIP.PARTS_CATEGORY GetCurrentSelectedArmedParts(List<GameObject> targetObj)
    {
        // 既に表示されていれば表示内容を全削除
        return targetObj.FirstOrDefault(i => i.GetComponent<SelectedStatus>().GetIsSelected())
            .GetComponent<EquipMenuArmedPartsButtonClicked>().partsCategory;
    }

    public CONST.ITEM.CATEGORY ConvertEquipPartsCategoryToItemCategory(CONST.EQUIP.PARTS_CATEGORY partsCategpory)
    {
        switch (partsCategpory)
        {
            case CONST.EQUIP.PARTS_CATEGORY.WEPON:
                return CONST.ITEM.CATEGORY.WEAPON_ITEM;
            case CONST.EQUIP.PARTS_CATEGORY.HEAD:
                return CONST.ITEM.CATEGORY.HEAD_EQUIP_ITEM;
            case CONST.EQUIP.PARTS_CATEGORY.BODY:
                return CONST.ITEM.CATEGORY.BODY_EQUIP_ITEM;
            case CONST.EQUIP.PARTS_CATEGORY.ACCESSORY1:
            case CONST.EQUIP.PARTS_CATEGORY.ACCESSORY2:
                return CONST.ITEM.CATEGORY.ACCESSORY_ITEM;
            default:
                return CONST.ITEM.CATEGORY.NONE;
        }
    }

    public void UpdateCurrentEquipMenuStatus(CONST.EQUIP_MENU_STATUS.MenuStatus targetStatus)
    {
        this.currentStatus = targetStatus;
    }

    public CONST.EQUIP_MENU_STATUS.MenuStatus GetCurrentEquipMenuStatus()
    {
        return this.currentStatus;
    }

    public void CloseEquipSeletedMenu()
    {
        // 部位選択ボタンを有効化する
        equipMenuUIManager.UpdateEnableEquipParts(true);
        // 装備ボタンを無効化する
        equipMenuUIManager.UpdateEnableEquip(false);
        // パラメーター更新表示を非表示にする
        equipMenuStatusUiManager.ClearUpdateParameter();
        this.UpdateCurrentEquipMenuStatus(CONST.EQUIP_MENU_STATUS.MenuStatus.SelectEquipParts);
    }

    public void OnClickeArmedParts(GameObject targetGameObject, CONST.EQUIP.PARTS_CATEGORY partsCategory)
    {
        // 全部位の選択状態をクリアし、クリックされたOBJのみ選択状態にする
        this.ClearEquipPartsButtonSelected(equipPartsButtonObj);
        equipMenuUIManager.ClearEquipPartsSelected();
        equipMenuScrollManager.ClearHadEquipList(equipObjList.Select(e => e.gameObject).ToList());
        targetGameObject.GetComponent<SelectedStatus>().UpdateIsSelected(true);
        equipMenuUIManager.UpdateEquipPartsSelectedIcon(targetGameObject.gameObject.GetComponent<EquipMenuArmedPartsButtonClicked>().isSelectedIcon, true);

        CONST.ITEM.CATEGORY itemCategory = this.ConvertEquipPartsCategoryToItemCategory(partsCategory);
        List<RectTransform> displayEquipList = new List<RectTransform>();
        // 外すボタンの設定
        if (partsCategory != CONST.EQUIP.PARTS_CATEGORY.WEPON)
        {
            displayEquipList.Add(equipMenuScrollManager.SetUpRemoveButton(itemCategory));
        }
        // 装備リストを更新
        List<HavingItem> hadItem = this.hadItem.GetEquipItem(itemCategory);
        var equipRects = equipMenuScrollManager.SetUpHadEquipList(hadItem);
        displayEquipList.AddRange(equipRects);
        equipObjList = displayEquipList;

    }

    public void OnArmedPartsSelected()
    {
        // 部位選択ボタンを無効化する
        equipMenuUIManager.UpdateEnableEquipParts(false);
        // 装備ボタンを有効化する
        equipMenuUIManager.UpdateEnableEquip(true);
        this.UpdateCurrentEquipMenuStatus(CONST.EQUIP_MENU_STATUS.MenuStatus.SelectEquip);

        // 装備の先頭(外すボタン)に選択状態を設定
        equipObjList[0].GetComponent<IButtonClicked>().OnClicked();

    }

    public void UpdateEquipSelected(string itemID, CONST.ITEM.CATEGORY partsCategory, GameObject targetEquipObj)
    {
        // 装備アイテム選択アイコンの初期化
        equipMenuUIManager.ClearEquipItemSelected();
        equipMenuScrollManager.ClearEquipItemButtonSelectedStatus(this.equipObjList.Select(e => e.gameObject).ToList());

        // 選択されたアイテムを選択状態にする
        targetEquipObj.GetComponentInChildren<SelectedStatus>().UpdateIsSelected(true);
        TextMeshProUGUI target_obj = targetEquipObj.transform.Find("SelectedIcon").GetComponent<TextMeshProUGUI>();
        equipMenuUIManager.UpdateEquipPartsSelectedIcon(target_obj, true);

        // パラメータ変化表示更新
        this.GetBeforeEquipUpdatedParameter(itemID, partsCategory);
        equipMenuStatusUiManager.UpdateWillCharPrameter(true);

        // Description を更新
        string targetDescription = itemManager.GetEquipDataFromMaster(itemID).description_equip;
        equipMenuDescriptionUI.SetDiscription(targetDescription);
    }


    public void UpdateEquip(string itemID, CONST.ITEM.CATEGORY partsCategory)
    {
        // 装備更新
        this.UpdateArmedEquip(itemID, partsCategory);

        // UI反映
        equipMenuScrollManager.ClearHadEquipList(equipObjList.Select(e => e.gameObject).ToList());

        CONST.ITEM.CATEGORY currentSelectedCategory = this.ConvertEquipPartsCategoryToItemCategory(this.GetCurrentSelectedArmedParts(this.equipPartsButtonObj));
        List<HavingItem> hadItem = this.hadItem.GetEquipItem(currentSelectedCategory);
        List<RectTransform> displayEquipList = new List<RectTransform>();
        // 外すボタンの設定
        if (partsCategory != CONST.ITEM.CATEGORY.WEAPON_ITEM)
        {
            displayEquipList.Add(equipMenuScrollManager.SetUpRemoveButton(this.ConvertEquipPartsCategoryToItemCategory(this.GetCurrentSelectedArmedParts(this.equipPartsButtonObj))));
        }
        var equipRects = equipMenuScrollManager.SetUpHadEquipList(hadItem);

        displayEquipList.AddRange(equipRects);
        equipObjList = displayEquipList;

        this.CloseEquipSeletedMenu();
    }
}
