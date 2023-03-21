using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CONST;

public class Money : MonoBehaviour
{
    /// <summary>
    ///  所持金
    /// </summary>
    /// <value></value>
    private int hasMoney { get; set; }

    /// <summary>
    /// 現在の所持金を取得
    /// </summary>
    /// <returns></returns>
    public int getHasMoney()
    {
        return this.hasMoney;
    }

    /// <summary>
    /// /// お金を増加かさせる(上限を上回った場合は上限にする)
    /// </summary>
    /// <param name="increasedMoney"></param>
    public void increaseHasMoney(int increasedMoney)
    {
        var resultMoney = this.hasMoney + increasedMoney;

        this.hasMoney = resultMoney > CONST.MONEY.MAX_AMMAUNTT_MONEY ?
         CONST.MONEY.MAX_AMMAUNTT_MONEY :
         resultMoney;
    }
}
