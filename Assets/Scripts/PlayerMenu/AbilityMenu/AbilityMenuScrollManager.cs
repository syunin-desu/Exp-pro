using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine;

public class AbilityMenuScrollManager : MonoBehaviour
{
    [SerializeField]
    RectTransform ItemContents = null;

    [SerializeField]
    private AbilityManager abilityManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject.Find("Ability_Button").SetActive(false);
    }

    public void SetupAbilityUI(List<Ability_base> havingAbilityList)
    {
        foreach (Ability_base ability in havingAbilityList)
        {
            var itemContent = GameObject.Instantiate(ItemContents) as RectTransform;
            itemContent.SetParent(transform, false);
            itemContent.GetComponent<AbilityButtonClicked>().ability_id = ability.id;
            itemContent.GetComponent<AbilityButtonClicked>().abilityType = ability.Type;

            var texts = itemContent.GetComponentsInChildren<TextMeshProUGUI>();

            // ボタンを非活性にする
            itemContent.GetComponentInChildren<Button>().enabled = false;
            texts.First(t => t.name == "AbilityName").text = this.abilityManager.getAbilityDisplayNameForID(ability.id);
            texts.First(t => t.name == "ConsumeMP").text = ability.requiredMp.ToString();
            var selected_text = itemContent.Find("SelectedIcon").GetComponent<TextMeshProUGUI>();
            selected_text.alpha = 0;


            itemContent.gameObject.SetActive(true);
        }

    }

    /// <summary>
    /// アイテムボタンの選択状態をすべて解除する
    /// </summary>
    public void ClearAbilityButtonSelected()
    {
        // 既に表示されていれば表示内容を全削除
        var items = GameObject.FindGameObjectsWithTag("PlayerMenuAbilityButton");

        foreach (var item in items)
        {
            item.GetComponent<AbilityStatus>().UpdateIsSelected(false);
        }
    }
}
