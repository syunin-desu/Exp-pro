using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharBase
{
    // TODO UIManagerもプロパティに含めないか検討
    // Attackメソッドをオーバーライドし、その中でUIの値の更新も行う

    void Start()
    {
        this.char_role = Const.CO.PLAYER;
    }
}
