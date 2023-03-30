using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム売買View
/// </summary>
public class SellItemView : MonoBehaviour
{

    /// <summary>
    /// アクションボタンView
    /// </summary>
    [SerializeField]
    ShopActionView shopActions;

    // ショップシーン起動時はショップ画面は非表示にする
    void Start()
    {
        // 初期は非表示
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        // escキーが入力された場合ウインドウを閉じる
        if (Input.GetKey(KeyCode.Escape))
        {
            this.closeSellItemView();
        }
    }

    /// <summary>
    /// アイテム売買Viewの表示、非表示を切り替える
    /// </summary>
    /// <param name="isVisible">表示状態かどうか</param>
    public void changeVisibleSellItemView(bool isVisible)
    {
        this.gameObject.SetActive(isVisible);
    }

    /// <summary>
    /// 売買リスト画面を閉じるときの処理
    /// </summary>
    public void closeSellItemView()
    {
        this.changeVisibleSellItemView(false);
        // ショップ行動ボタンを活性表示させる
        shopActions.changeEnableShotActionView(true);

    }
}
