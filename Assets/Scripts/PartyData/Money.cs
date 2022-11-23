using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 所持金クラス
public class Money : MonoBehaviour
{
    // 所持金
    private int hasMoney { get; set; }

    // 所持金を取得
    public int getMoney()
    {
        return this.hasMoney;
    }

    // お金を追加する(上限値9999999の場合は追加しない)
    public int increaseMoney(int increaseMoney)
    {
        int resultMoney = this.hasMoney + increaseMoney;
        return resultMoney > 9999999 ? 9999999 : resultMoney;
    }

    // 所持金を減らす(マイナスの場合は0にして返す)
    public int decreaseMoney(int decreaseMoney)
    {
        int resultMoney = this.hasMoney - decreaseMoney;
        return resultMoney < 0 ? 0 : resultMoney;
    }

    // 所持金を減らす
    public bool canDecreaseMoney(int decreaseMoney)
    {
        int resultMoney = this.hasMoney - decreaseMoney;

        // 所持金がマイナスになる場合はfalseを返す
        if (resultMoney < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


}
