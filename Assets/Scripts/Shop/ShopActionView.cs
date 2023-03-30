using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ショップ行動ボタンView
/// </summary>
public class ShopActionView : MonoBehaviour
{
    /// <summary>
    /// ショップ行動ボタンの活性/非活性を変更する
    /// </summary>
    /// <param name="isEnable">活性状態かどうか</param>
    public void changeEnableShotActionView(bool isEnable)
    {
        // ショップ行動ボタンViewの子コンポーネントを取得する
        var targetButtons = this.gameObject.GetComponentsInChildren<Button>();

        foreach (var button in targetButtons)
        {
            // ボタンの活性/非活性を更新する
            button.interactable = isEnable;
        };
    }
}
