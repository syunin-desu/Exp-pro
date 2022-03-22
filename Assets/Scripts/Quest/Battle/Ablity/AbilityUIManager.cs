using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUIManager : MonoBehaviour
{
    [SerializeField]
    public GameObject ability_contents;
    private List<string> ability_list = new List<string>();
    private RectTransform _contentArea;
    /// <summary>
    /// アビリティのパラメータを設定する
    /// </summary>
    public void Setup_abilityUI(List<string> ability_List)
    {

        var content = GameObject.Find("Ability_UI_Content");
        foreach (var ability in ability_List)
        {
            GameObject abilityObj = Instantiate(ability_contents);
            abilityObj.AddComponent<CanvasRenderer>();
            abilityObj.transform.SetParent(content.transform);
        }
    }
}
