using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMenuUIManager : MonoBehaviour
{
    private bool isAbilityMenu = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.SetActive(isAbilityMenu);
    }

    // <summary>
    /// アビリティメニューの表示する
    /// </summary>
    public void ShowItemMenu()
    {
        isAbilityMenu = true;
        this.gameObject.SetActive(isAbilityMenu);
    }

    /// <summary>
    /// アビリティメニューを閉じる
    /// </summary>
    public void CloseItemMenu()
    {
        isAbilityMenu = false;
        this.gameObject.SetActive(isAbilityMenu);
    }

    /// <summary>
    /// アビリティリストをクリアする
    /// </summary>
    public void ClearAbiltyList()
    {
        var items = GameObject.FindGameObjectsWithTag("PlayerMenuAbilityButton");

        //表示しているボタンの削除 
        foreach (var button in items)
        {
            Destroy(button);

        }

    }

    /// <summary>
    /// アビリティボタンの非活性を更新
    /// </summary>
    public void UpdateAbilityButtonEnable(bool enable)
    {
        var targetobjs = GameObject.FindGameObjectsWithTag("PlayerMenuAbilityButton");

        foreach (var obj in targetobjs)
        {
            obj.GetComponentInChildren<Button>().enabled = enable;
            if (obj.GetComponent<AbilityButtonClicked>().abilityType != CONST.ACTION.TYPE.Heal)
            {
                var texts = obj.GetComponentsInChildren<TextMeshProUGUI>();
                texts.First(t => t.name == "AbilityName").color = Color.gray;
                texts.First(t => t.name == "Label").color = Color.gray;
                texts.First(t => t.name == "ConsumeMP").color = Color.gray;

            }

        }
    }

    /// <summary>
    /// 全アビリティのテキスト色をデフォルトに修正
    /// </summary>
    public void ClearAllAbilityTextColor()
    {
        var targetobjs = GameObject.FindGameObjectsWithTag("PlayerMenuAbilityButton");
        foreach (var obj in targetobjs)
        {
            var texts = obj.GetComponentsInChildren<TextMeshProUGUI>();
            texts.First(t => t.name == "AbilityName").color = Color.white;
            texts.First(t => t.name == "Label").color = Color.white;
            texts.First(t => t.name == "ConsumeMP").color = Color.white;

        }
    }

    /// <summary>
    /// Howtoボタンの非活性を更新
    /// </summary>
    public void UpdatHowtoButtonEnable(bool enable)
    {
        var targetobjs = GameObject.FindGameObjectsWithTag("PlayerMenuAbilityHottoButton");

        foreach (var obj in targetobjs)
        {
            obj.GetComponentInChildren<Button>().enabled = enable;

        }
    }

    /// <summary>
    /// 全選択アイコンを非活性にする
    /// </summary>
    public void AllSelectedIconDisable()
    {
        var targetobjs = GameObject.FindGameObjectsWithTag("PlayerMenuAbilitySelectedIcon");

        foreach (var obj in targetobjs)
        {
            obj.GetComponent<TextMeshProUGUI>()
                .alpha = 0;

        }
    }


    /// <summary>
    /// アイテム選択アイコンの更新
    /// </summary>
    public void UpdateAbilitySelectedIcon(TextMeshProUGUI obj, bool isEnable)
    {
        obj.alpha = isEnable ? 100 : 0;
    }
}
