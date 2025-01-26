using UnityEngine;

// キー押下時の実施アクションインターフェース
// 各メニューのキー押下アクションクラスで継承させる
public interface IInputAction
{

    // メニューボタン押下
    void KeyInput_OpenMenu() { }

    // 戻るボタン押下
    void KeyInput_Return() { }

}
