using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アビリティウインドウUI管理クラス
/// </summary>
public class AbilityUIManager : MonoBehaviour
{

    void Update()
    {
        // escキーが入力された場合ウインドウを閉じる
        if (Input.GetKey(KeyCode.Escape))
        {
            this.removeAbilityWindow();
        }
    }

    /// <summary>
    ///  アビリティウインドウを閉じる
    /// </summary>
    public void removeAbilityWindow()
    {
        var abilities = GameObject.FindGameObjectsWithTag("AbilityButton");

        //表示しているボタンの削除 
        foreach (var button in abilities)
        {
            Destroy(button);

        }

        // アビリティウインドウを閉じる
        this.manageShowAbilityWindow(false);
    }

    /// <summary>
    ///  アビリティウインドウを表示、非表示にする
    /// </summary>
    /// <param name="isShowAbilityWindow">アビリティウインドウの表示、非表示</param>
    public void manageShowAbilityWindow(bool isShowAbilityWindow)
    {
        this.gameObject.SetActive(isShowAbilityWindow);
    }
}