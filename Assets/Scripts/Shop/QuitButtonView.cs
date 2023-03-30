using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 出るボタンView
/// </summary>
public class QuitButtonView : MonoBehaviour
{
    /// <summary>
    /// ショップメニューUI
    /// </summary>
    [SerializeField]
    GameObject shopMenu;

    /// <summary>
    /// 準備画面メニューUI
    /// </summary>
    [SerializeField]
    GameObject prepareMainMenu;

    /// <summary>
    /// 出るボタンを押したときの処理
    /// </summary>
    public void quitButtonClickedAction()
    {
        shopMenu.SetActive(false);
        prepareMainMenu.SetActive(true);
    }
}
