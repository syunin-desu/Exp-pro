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

    [SerializeField]
    private AbilityMenuUIManager abilityMenuUIManager;

    [SerializeField]
    private AbilityMenuManager abilityMenuManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject.Find("Ability_Button").SetActive(false);
    }

    public List<RectTransform> SetupAbilityUI(List<Ability_base> havingAbilityList)
    {
        List<RectTransform> resultObj = new List<RectTransform>();
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

            resultObj.Add(itemContent);
            itemContent.gameObject.SetActive(true);
        }

        return resultObj;

    }

    public void SetUpAbilitySelected(GameObject firstAbility)
    {
        this.UpdateAbilitySelected(firstAbility);
    }

    /// <summary>
    /// アビリティボタン選択状態をすべて解除する
    /// </summary>
    public void ClearAbilityButtonSelected(List<GameObject> AbilityButtons)
    {

        foreach (var item in AbilityButtons)
        {
            item.GetComponent<SelectedStatus>().UpdateIsSelected(false);
        }
    }

    // アビリティの選択状態を更新する
    public void UpdateAbilitySelected(GameObject selectedObj)
    {
        var selectedAbilityID = selectedObj.GetComponent<AbilityButtonClicked>().ability_id;

        //全アビリティの選択中ステータスをFalseにする
        this.ClearAbilityButtonSelected(this.abilityMenuManager.AbilityButtonList.Select(i => i.gameObject).ToList());
        abilityMenuUIManager.AllSelectedIconDisable(this.abilityMenuManager.AbilityButtonList.Select(i => i.gameObject).ToList());
        abilityMenuUIManager.resetListScroll(selectedObj.GetComponent<RectTransform>());

        // 選択されたアイテムを選択状態にする
        selectedObj.GetComponentInChildren<SelectedStatus>().UpdateIsSelected(true);
        TextMeshProUGUI target_obj = selectedObj.transform.Find("SelectedIcon").GetComponent<TextMeshProUGUI>();
        abilityMenuUIManager.UpdateAbilitySelectedIcon(target_obj, true);

        // Description を更新
        string targetIAbilityID = selectedObj.GetComponent<AbilityButtonClicked>().ability_id;
        abilityMenuManager.UpdateDiscription(abilityManager.GetAbilityDiscriptionfromMaster(selectedAbilityID) ?? "");

    }
}
