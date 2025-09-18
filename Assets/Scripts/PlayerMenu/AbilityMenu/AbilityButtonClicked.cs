using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class AbilityButtonClicked : MonoBehaviour, IButtonClicked
{
    public AbilityManager _abilityManager;
    public AbilityMenuManager abilityMenuManager;
    public AbilityMenuUIManager abilityMenuUIManager;
    public AbilityMenuScrollManager abilityMenuScrollManager;
    public PartyMember _playerManager;
    public SelectedStatus _abilityStatus;
    public string ability_id;
    public CONST.ACTION.TYPE abilityType;

    // UI指定
    [SerializeField]
    public TextMeshProUGUI selectedIcon;
    [SerializeField]
    public TextMeshProUGUI abilityName;
    [SerializeField]
    public TextMeshProUGUI abilityLabel;
    [SerializeField]
    public TextMeshProUGUI consumeMp;

    /// <summary>
    /// 表示されているアイテムがクリックした時の処理
    /// </summary>
    /// <param name="charactor">キャラ</param>
    public void OnClicked()
    {
        GameObject selectedObj = this.gameObject;

        // 選択されたOBJと一致していた場合はアイテム使用を実施
        string selectedAbilityID = selectedObj.GetComponent<AbilityButtonClicked>().ability_id;

        // managerクラスに選択中オブジェクトとして更新
        if (selectedObj.GetComponentInChildren<SelectedStatus>().GetIsSelected() == false)
        {
            //全アイテムの選択中ステータスをFalseにする
            abilityMenuScrollManager.UpdateAbilitySelected(selectedObj);
            return;
        }

        if (_playerManager.GetHp() == _playerManager.GetMaxHp())
        {
            Debug.Log("最大HPなので回復の必要ありませんでした。");
            return;
        }

        if (_playerManager.GetMp() < _abilityManager.getAbilityConsumeMPForID(selectedAbilityID))
        {
            Debug.Log("MPが足りません");
            return;
        }
        if (_abilityManager.GetAbilityActionType(selectedAbilityID) != CONST.ACTION.TYPE.Heal)
        {
            Debug.Log("実行できないアビリティです");
            return;
        }


        // アビリティの効果を実施
        abilityMenuManager.ExecuteAbilityOnPlayerMenu(_playerManager, selectedAbilityID);

    }
}
