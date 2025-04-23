using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class AbilityButtonClicked : MonoBehaviour
{
    // eventSystemを取得するための変数宣言
    [SerializeField] EventSystem eventSystem;
    public AbilityManager _abilityManager;
    public AbilityMenuManager abilityMenuManager;
    public AbilityMenuUIManager abilityMenuUIManager;
    public AbilityMenuScrollManager abilityMenuScrollManager;
    public PartyMember _playerManager;
    public AbilityStatus _abilityStatus;
    public string ability_id;
    public CONST.ACTION.TYPE abilityType;

    /// <summary>
    /// 表示されているアイテムがクリックした時の処理
    /// </summary>
    /// <param name="charactor">キャラ</param>
    public void OnClickAbility()
    {
        GameObject selectedObj = eventSystem.currentSelectedGameObject.gameObject;

        // 選択されたOBJと一致していた場合はアイテム使用を実施
        string selectedItemID = selectedObj.GetComponent<AbilityButtonClicked>().ability_id;

        // managerクラスに選択中オブジェクトとして更新
        if (selectedObj.GetComponentInChildren<AbilityStatus>().GetIsSelected() == false)
        {
            //全アイテムの選択中ステータスをFalseにする
            abilityMenuScrollManager.ClearAbilityButtonSelected();
            abilityMenuUIManager.AllSelectedIconDisable();

            // 選択されたアイテムを選択状態にする
            selectedObj.GetComponentInChildren<AbilityStatus>().UpdateIsSelected(true);
            TextMeshProUGUI target_obj = selectedObj.transform.Find("SelectedIcon").GetComponent<TextMeshProUGUI>();
            abilityMenuUIManager.UpdateAbilitySelectedIcon(target_obj, true);

            // Description を更新
            string targetIAbilityID = selectedObj.GetComponent<AbilityButtonClicked>().ability_id;
            abilityMenuManager.UpdateDiscription(_abilityManager.GetAbilityDiscriptionfromMaster(selectedItemID) ?? "");
            return;
        }

        if (_playerManager.GetHp() == _playerManager.GetMaxHp())
        {
            Debug.Log("最大HPなので回復の必要ありませんでした。");
            return;
        }

        if (_playerManager.GetMp() < _abilityManager.getAbilityConsumeMPForID(selectedItemID))
        {
            Debug.Log("MPが足りません");
            return;
        }
        if (_abilityManager.GetAbilityActionType(selectedItemID) != CONST.ACTION.TYPE.Heal)
        {
            Debug.Log("実行できないアビリティです");
            return;
        }


        // アビリティの効果を実施
        abilityMenuManager.ExecuteAbilityOnPlayerMenu(_playerManager, selectedItemID);

    }
}
