using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIManager : MonoBehaviour
{
    // TODO UI共通処理を持つ親クラスを作成し、それを継承するような構造にする

    void Update()
    {
        // escキーが入力された場合ウインドウを閉じる
        if (Input.GetKey(KeyCode.Escape))
        {
            this.removeItemWindow();
        }
    }

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
}
