using NUnit.Framework;
using System.Linq;

/// <summary>
/// プレーヤークラス
/// </summary>
public class PartyMember : CharBase
{

    void Start()
    {
        this.char_role = CONST.CHARCTOR.PLAYER;

        // TODO: 現状はひとりのため
        var masterData = PlayerData.instance.PartyMember[0];
        this.SetParameter(masterData);
    }
}
