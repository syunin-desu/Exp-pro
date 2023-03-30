using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 売るボタンView
/// </summary>
public class SellButtonView : MonoBehaviour
{
    /// <summary>
    /// 売買リストView
    /// </summary>
    [SerializeField]
    SellItemView sellItem;

    /// <summary>
    /// アクションボタンView
    /// </summary>
    [SerializeField]
    ShopActionView shopActions;

    /// <summary>
    /// 売るボタンが押下されたときの処理
    /// </summary>
    public void onSelectedSellButton()
    {
        sellItem.changeVisibleSellItemView(true);
        // 売買、出るボタンを非活性にする
        shopActions.changeEnableShotActionView(false);

    }
}
