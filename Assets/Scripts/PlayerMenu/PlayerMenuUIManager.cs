using UnityEngine;

public class PlayerMenuUIManager : MonoBehaviour
{
    private bool isPlayerMenu = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.SetActive(isPlayerMenu);
    }

    /// <summary>
    /// プレイヤーメニューの表示する
    /// </summary>
    public void ShowPlayerMenu()
    {
        isPlayerMenu = true;
        this.gameObject.SetActive(isPlayerMenu);
    }

    /// <summary>
    /// プレイヤーメニューを閉じる
    /// </summary>
    public void ClosePlayerMenu()
    {
        isPlayerMenu = false;
        this.gameObject.SetActive(isPlayerMenu);
    }

    /// <summary>
    /// メニュー表示状態を返す
    /// </summary>
    /// <returns></returns>
    public bool IsPlayerMenu() { return isPlayerMenu; }
}
