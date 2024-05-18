using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIManager : MonoBehaviour
{

    /// <summary>
    /// アイテムウインドウを閉じる
    /// </summary>
    public void removeItemWindow()
    {

        var items = GameObject.FindGameObjectsWithTag("ItemButton");

        //表示しているボタンの削除 
        foreach (var button in items)
        {
            Destroy(button);

        }

        // アビリティウインドウを閉じる
        this.manageShowItemWindow(false);
    }

    /// <summary>
    /// アイテムウインドウを表示、非表示にする
    /// </summary>
    /// <param name="isShowItemWindow">アイテムウインドウの表示、非表示</param>
    public void manageShowItemWindow(bool isShowItemWindow)
    {

        this.gameObject.SetActive(isShowItemWindow);
    }

    /// <summary>
    /// アイテムウインドウがアクティブかどうかを返す
    /// </summary>
    /// <returns></returns>
    public bool getIsItemUIWindowActive()
    {
        return this.gameObject.activeSelf;
    }
}
