using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharBase
{
    // TODO UIManagerもプロパティに含めないか検討
    // Attackメソッドをオーバーライドし、その中でUIの値の更新も行う
    public CharData charData;


    void Start()
    {
        this.char_role = CONST.CHARCTOR.PLAYER;
        this.SetParameter(this.charData);
    }
}
