using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ItemMenuUIManager : MonoBehaviour
{
    [SerializeField]
    private QuestManager _questManager;

    private bool isItemMenu = false;

    private ListViewUtility listUtility = new ListViewUtility();


    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private RectTransform scrollContent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.SetActive(isItemMenu);
    }

    /// <summary>
    /// アイテムメニューの表示する
    /// </summary>
    public void ShowItemMenu()
    {
        isItemMenu = true;
        _questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.ItemMenu);
        this.gameObject.SetActive(isItemMenu);
    }

    /// <summary>
    /// アイテムメニューを閉じる
    /// </summary>
    public void CloseItemMenu()
    {
        isItemMenu = false;
        _questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.PlayerMainMenu);
        this.gameObject.SetActive(isItemMenu);
    }

    /// <summary>
    /// メニュー表示状態を返す
    /// </summary>
    /// <returns></returns>
    public bool IsItemMenu() { return isItemMenu; }

    /// <summary>
    /// 全選択アイコンを非活性にする
    /// </summary>
    public void AllSelectedIconDisable()
    {
        var targetobjs = GameObject.FindGameObjectsWithTag("PlayerMenuItemSelectedIcon");

        foreach (var obj in targetobjs)
        {
            obj.GetComponent<TextMeshProUGUI>()
                .alpha = 0;

        }
    }


    /// <summary>
    /// アイテム選択アイコンの更新
    /// </summary>
    public void UpdateItemSelectedIcon(TextMeshProUGUI obj, bool isEnable)
    {
        obj.alpha = isEnable ? 100 : 0;
    }

    public void UpdateHowButtonColor(GameObject targetGameObject)
    {
        targetGameObject.GetComponent<Image>().color = CONST.UI.SELECTED_BUTTON_COLOR;
    }

    /// <summary>
    /// ボタンを非選択状態にUI更新する
    /// </summary>
    public void AllSelectedHowButtonUnSelected(List<GameObject> targets)
    {
        foreach (var obj in targets)
        {
            obj.GetComponent<Image>().color = Color.white;

        }
    }

    public void resetListScroll(RectTransform targetContent)
    {
        this.listUtility.ScrollToTarget(this.scrollRect, scrollContent, targetContent);
    }
}
