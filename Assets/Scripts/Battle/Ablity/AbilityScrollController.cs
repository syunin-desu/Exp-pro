using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// バトル時のアビリティ選択画面の中身を管理
/// </summary>
// TODO: 名前を後で帰る(AbilityScrollContentsManager)
public class AbilityScrollController : MonoBehaviour
{
    /// <summary>
    /// テスト用に指定(本実装では複数中に入ることになる)
    /// </summary>
    [SerializeField]
    RectTransform ability_contents = null;
    private RectTransform _contentArea;

    /// <summary>
    /// アビリティ実行クラス
    /// </summary>
    [SerializeField]
    private AbilityManager abilityManager;

    void Start()
    {
        GameObject.Find("Ability_button").SetActive(false);
    }


    /// <summary>
    /// アビリティのパラメータを設定する
    /// </summary>
    public void Setup_abilityUI(List<string> ability_List)
    {
        var content = GameObject.Find("Ability_UI_Content");
        foreach (string ability in ability_List)
        {
            var ability_content = GameObject.Instantiate(ability_contents) as RectTransform;
            ability_content.SetParent(transform, false);

            var text = ability_content.GetComponentInChildren<Text>();
            text.text = this.abilityManager.getAbilityDisplayName(ability);
            ability_content.gameObject.SetActive(true);
        }
    }
}
