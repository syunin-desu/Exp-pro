using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 準備シーンメインウインドウUI管理クラス
/// </summary>
public class PrepareSceneMainWindowUIManager : MonoBehaviour
{

    /// <summary>
    /// 準備シーン、メインメニューの表示/非表示を切り替える
    /// </summary>
    /// <param name="isVisible"> 準備シーン、メインメニューを表示するかどうか</param>
    public void changeVisibleMainMenu(bool isVisible)
    {
        // 準備シーンメインメニューを表示または非表示にする
        this.gameObject.SetActive(isVisible);
    }
}
