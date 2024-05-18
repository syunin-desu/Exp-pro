using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 行動リストUI
/// </summary>
public class ActionRowNumberListUI : MonoBehaviour
{
    /// <summary>
    /// アイテムウインドウを表示、非表示にする
    /// </summary>
    /// <param name="isShowItemWindow">アイテムウインドウの表示、非表示</param>
    public void manageShowActionListUI(bool isShowActionListUI)
    {

        this.gameObject.SetActive(isShowActionListUI);
    }
}
