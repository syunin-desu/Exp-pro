using UnityEngine;

public class ItemMenuUIManager : MonoBehaviour
{
    [SerializeField]
    private QuestManager _questManager;

    private bool isItemMenu = false;

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
}
