using NUnit.Framework;
using System.Linq;

/// <summary>
/// プレーヤークラス
/// </summary>
public class PartyMember : CharBase
{

    void Start()
    {
        this.char_role = CONST.CHARCTOR.Role.PLAYER;

        // TODO: 現状はひとりのため
        var masterData = PlayerData.instance.PartyMember[0];
        this.SetParameter(masterData);

        // クラスアビリティを反映
        this.SetBattleClassSkills();

    }

    public void UpdateCharParam(CharParameter charParameter)
    {
        this.SetParameter(charParameter);
    }
}
