/// <summary>
/// プレーヤークラス
/// </summary>
public class PartyMember : CharBase
{

    void Start()
    {
        this.char_role = CONST.CHARCTOR.PLAYER;

        // TODO: 現状はひとりのため
        var member = GameData.instance.PartyMember[0];
        this.SetParameter(member);
    }
}
