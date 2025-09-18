using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class AbilityMenuManager : MonoBehaviour
{
    [SerializeField]
    private QuestManager _questManager;
    public AbilityManager abilityManager;
    public AbilityMenuUIManager abilityMenuUIManager;
    public AbilityMenuScrollManager abilityMenuScrollManager;
    public AbilityDescriptionUI itemMenuDescriptionUI;
    public PartyMember partyMember;

    [SerializeField]
    private CONST.ABILITY_MENU_STATUS.MenuStatus currentStatus;
    [SerializeField]
    private MenuSelectionUtility gameObjectUtility = new MenuSelectionUtility();

    public List<RectTransform> AbilityButtonList = new List<RectTransform>();

    // インスペクターから設定
    public List<GameObject> AbilityCategoryButtons = new List<GameObject>();

    public PartyMember wplayerParam;

    public void SetAbilityMenu()
    {
        _questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.AbilityMenu);
        this.currentStatus = CONST.ABILITY_MENU_STATUS.MenuStatus.HowAbility;
        abilityMenuUIManager.ShowItemMenu();
        //デフォルト、Magicアビリティを表示する
        List<Ability_base> hasAbilityList = partyMember.GetHavingAbilitiesForCategory(CONST.ABILITY.Category.Magic); ;

        // 選択状態の初期化
        this.ClearButtonSelectedInList(AbilityCategoryButtons);
        this.AbilityCategoryButtons[0].gameObject.GetComponent<SelectedStatus>().UpdateIsSelected(true);
        abilityMenuUIManager.UpdateHowAbilityButtonColor(this.AbilityCategoryButtons, this.AbilityCategoryButtons[0]);

        // アビリティをリスト表示する + 選択制御のためのアビリティgameObjectを取得
        AbilityButtonList = abilityMenuScrollManager.SetupAbilityUI(hasAbilityList);
    }

    /// <summary>
    /// 表示アビリティの更新があった際に、リストを再描画する
    /// </summary>
    public void UpdateAbilityMenu(List<Ability_base> havingAbilityList)
    {
        // アビリティをリスト表示する + 選択制御のためのアビリティgameObjectを取得
        AbilityButtonList = abilityMenuScrollManager.SetupAbilityUI(havingAbilityList);
    }

    public async void ExecuteAbilityOnPlayerMenu(CharBase performChar, string execAbilityID)
    {
        await abilityManager.execAbility(performChar, null, execAbilityID, abilityManager.getAbilityActionsForID(execAbilityID));
    }

    public CONST.ABILITY_MENU_STATUS.MenuStatus GetCurrentAbilityMenuStatus()
    {
        return currentStatus;
    }

    public void UpdateAbilityMenuStatus(CONST.ABILITY_MENU_STATUS.MenuStatus targetAbilityMenuStatus)
    {
        currentStatus = targetAbilityMenuStatus;
    }

    public void CloseAbilityMenu()
    {
        // 各アビリティの選択状態、説明表示をクリア
        this.ClearAbilitySelected();
        // アビリティボタンを活性化する
        abilityMenuUIManager.UpdateAbilityButtonEnable(false);
        // howtoボタン群を非活性にする
        abilityMenuUIManager.UpdatHowtoButtonEnable(true, AbilityCategoryButtons.Select(i => i.gameObject).ToList());
        abilityMenuUIManager.ClearAbiltyList();
        abilityMenuUIManager.CloseItemMenu();
        _questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.PlayerMainMenu);

        // PlayerDataを更新
        PlayerData.instance.UpdatePartyMemberParam(wplayerParam.GetCharParameters());
    }
    public void CloseAbilitySelectedMenu()
    {
        // 各アビリティの選択状態、説明表示をクリア
        this.ClearAbilitySelected();
        // 
        // アビリティ選択状態に移行
        this.UpdateAbilityMenuStatus(CONST.ABILITY_MENU_STATUS.MenuStatus.HowAbility);
        // howtoボタン群を活性にする
        abilityMenuUIManager.UpdatHowtoButtonEnable(true, AbilityCategoryButtons.Select(i => i.gameObject).ToList());
        abilityMenuUIManager.UpdateAbilityButtonEnable(false);

        // PlayerDataを更新
        PlayerData.instance.UpdatePartyMemberParam(wplayerParam.GetCharParameters());
    }

    public void AbilityButtonFromBasisKeyInput(CONST.MENU.SELECTEDTYPE selectedType)
    {
        switch (this.currentStatus)
        {
            case CONST.ABILITY_MENU_STATUS.MenuStatus.HowAbility:
                gameObjectUtility.ClickedButtonsInALow(AbilityCategoryButtons,
                    selectedType
                    );
                break;
            case CONST.ABILITY_MENU_STATUS.MenuStatus.SelectAbility:
                gameObjectUtility.ClickedButtonsInTwoLow(AbilityButtonList.Select(i => i.gameObject).ToList(),
                selectedType);
                break;
        }
    }

    public void AbilityButtonFromKeyInputShiftColumn(CONST.MENU.SELECTEDTYPE selectedType)
    {
        switch (this.currentStatus)
        {
            case CONST.ABILITY_MENU_STATUS.MenuStatus.SelectAbility:
                gameObjectUtility.ClickedButtonsInTwoLow(AbilityButtonList.Select(i => i.gameObject).ToList(),
                selectedType);
                break;
        }
    }

    public void ChangedHowButtonSelection(CONST.ABILITY.Category targetCategory, GameObject selectedGameObject)
    {
        // 現在のアビリティ表示をクリアする
        abilityMenuUIManager.ClearAbiltyList();
        abilityMenuScrollManager.ClearAbilityButtonSelected(this.AbilityButtonList.Select(i => i.gameObject).ToList());

        // AbilityListを更新
        List<Ability_base> hasAbilityList = partyMember.GetHavingAbilitiesForCategory(
            targetCategory);
        AbilityButtonList = abilityMenuScrollManager.SetupAbilityUI(hasAbilityList);

        // 選択状態の更新
        this.ClearButtonSelectedInList(AbilityCategoryButtons);
        selectedGameObject.GetComponent<SelectedStatus>().UpdateIsSelected(true);
        abilityMenuUIManager.AllSelectedHowButtonUnSelected(AbilityCategoryButtons);
        abilityMenuUIManager.UpdateHowAbilityButtonColor(this.AbilityCategoryButtons, selectedGameObject);
    }

    public void ChangeToSelectAbility()
    {
        // アビリティ選択状態に移行
        this.UpdateAbilityMenuStatus(CONST.ABILITY_MENU_STATUS.MenuStatus.SelectAbility);
        // アビリティボタンを活性化する
        abilityMenuUIManager.UpdateAbilityButtonEnable(true);
        // howtoボタン群を非活性にする
        abilityMenuUIManager.UpdatHowtoButtonEnable(false, AbilityCategoryButtons.Select(i => i.gameObject).ToList());

        // アビリティ選択画面のセットアップ処理
        this.abilityMenuScrollManager.SetUpAbilitySelected(AbilityButtonList[0].gameObject);

    }

    public void UpdateDiscription(string discription)
    {
        itemMenuDescriptionUI.SetDiscription(discription);
    }

    public void ClearAbilitySelected()
    {
        abilityMenuScrollManager.ClearAbilityButtonSelected(this.AbilityButtonList.Select(i => i.gameObject).ToList());
        abilityMenuUIManager.AllSelectedIconDisable(AbilityButtonList.Select(i => i.gameObject).ToList());
        //TODO: 非活性色のままになる
        abilityMenuUIManager.ClearAllAbilityTextColor();
        this.UpdateDiscription("");
    }

    private void ClearButtonSelectedInList(List<GameObject> targets)
    {
        foreach (var item in targets)
        {
            item.GetComponent<SelectedStatus>().UpdateIsSelected(false);
        }
    }
}
