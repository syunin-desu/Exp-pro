using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アビリティが選択された際の実行処理
/// </summary>
public class selectedAbility : MonoBehaviour
{

    // eventSystemを取得するための変数宣言
    [SerializeField] EventSystem eventSystem;
    /// <summary>
    ///  アビリティ実行管理クラス
    /// </summary>
    public AbilityManager abilityManager;

    /// <summary>
    ///  バトル管理クラス
    /// </summary>
    public BattleManager battleManager;

    /// <summary>
    /// プレイヤー管理クラス
    /// </summary>
    public PlayerManager playerManager;

    /// <summary>
    /// アビリティが選択されたときの処理
    /// </summary>
    /// <param name="charactor"></param>
    public void OnClickAbility(CharBase charactor)
    {
        GameObject selectedObj = eventSystem.currentSelectedGameObject.gameObject;
        string selectedAbility_DisplayName = selectedObj.GetComponentInChildren<Text>().text;

        // 実行アビリティ名を取得する
        // TODO: 名前で取得させているが、Enumやクラスを渡す形にしたい
        string selected_abilityName = this.abilityManager.getAbilityNameForDisplayName(selectedAbility_DisplayName);

        // 選択されたアビリティをアクションリストに追加
        battleManager.setAction_Ability(playerManager, selected_abilityName);


    }
}
