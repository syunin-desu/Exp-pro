using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 買うボタンView
/// </summary>
public class BuyButtonView : MonoBehaviour
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
    /// 買うボタンが押下されたときの処理
    /// </summary>
    public void onSelectedBuyButton()
    {
        sellItem.changeVisibleSellItemView(true);
        // 売買、出るボタンを非活性にする
        shopActions.changeEnableShotActionView(false);

    }
}
