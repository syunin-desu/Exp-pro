using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityScrollController : MonoBehaviour
{
    [SerializeField]
    RectTransform ability_contents = null;
    private List<string> ability_list = new List<string>();
    private RectTransform _contentArea;

    [SerializeField]
    private AbilityManager abilityManager;


    public delegate MonoBehaviour InstantiateItemViewDelegate();
    private InstantiateItemViewDelegate _instantiateItemViewDelegate;

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
