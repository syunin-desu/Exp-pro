using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


public class selectedAbility : MonoBehaviour
{

    // eventSystemを取得するための変数宣言
    [SerializeField] EventSystem eventSystem;
    public AbilityManager abilityManager;
    public BattleManager battleManager;
    public PlayerManager playerManager;

    /// <summary>
    /// アビリティが選択されたときの処理
    /// </summary>
    /// <param name="charactor"></param>
    public void OnClickAbility(CharBase charactor)
    {
        // TODO 引数CharBaseでパーティの誰かを特定できるようにする

        GameObject selectedObj = eventSystem.currentSelectedGameObject.gameObject;
        string selectedAbility_DisplayName = selectedObj.GetComponentInChildren<Text>().text;

        string selected_abilityName = this.abilityManager.getAbilityNameForDisplayName(selectedAbility_DisplayName);
        battleManager.setAction_Ability(playerManager, selected_abilityName);


    }
}
