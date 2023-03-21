using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ショップ画面UI管理クラス
/// </summary>
public class ShopMenuUIManager : MonoBehaviour
{
    // ショップシーン起動時はショップ画面は非表示にする
    void Start()
    {
        this.changeVisibleShopMenu(false);
    }

    /// <summary>
    /// ショップ画面の表示/非表示を切り替える
    /// </summary>
    /// <param name="isVisible">ショップ画面を表示させるかどうか</param>
    public void changeVisibleShopMenu(bool isVisible)
    {
        this.gameObject.SetActive(isVisible);
    }
}
