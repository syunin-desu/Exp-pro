using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//敵を管理するもの(ステータス/クリック検出)
//char_Baseから継承
public class EnemyManager : CharBase
{

    Action tapAction; //クリックされた時に実行したい関数

    public void AddEventListenerOnTap(Action action)
    {
        tapAction += action;
    }

    public void OnTap()
    {
        Debug.Log("クリックされた");
        tapAction();
    }
}
