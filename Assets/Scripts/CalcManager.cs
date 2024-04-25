

// 計算関連クラス群


/// <summary>
/// battle関連計算クラス
/// </summary>
public class Calc_Battle_Manager 
{
    /// <summary>
    /// 属性によるダメージ率の増減を計算し返す
    /// </summary>
    /// <param name="weakElement">弱点属性</param>
    /// <param name="strongElement">耐性属性</param>
    /// <param name="targetChar">対象のキャラ</param>
    /// <returns>ダメージ率</returns>
    public float calcElementDamageRate(CONST.UTILITY.Element Element, CharBase targetChar)
    {
        bool haveTargetCharWeakElement = targetChar.GetWeakElement().Contains(Element);
        bool haveTargetCharStrongElement = targetChar.GetStrongElement().Contains(Element);
        float elementDamageRate = 1;
        if (haveTargetCharWeakElement)
        {
            elementDamageRate = haveTargetCharWeakElement ? CONST.BATTLE_RATE.RATE_WEAK_ELEMENT : 1;
        }
        else if (haveTargetCharStrongElement)
        {
            elementDamageRate = haveTargetCharStrongElement ? CONST.BATTLE_RATE.RATE_STRONG_ELEMENT : 1;

        }
        return elementDamageRate;
    }

    /// <summary>
    /// 魔法ダメージを計算する
    /// 実行者のINT * Power * 属性によるダメージ倍率
    /// </summary>
    /// <param name="performerInt"></param>
    /// <param name="power"></param>
    /// <param name="elementDamageRate"></param>
    /// <returns></returns>
    public int calcMagicAbilityDamage(int performerInt, int power, float elementDamageRate)
    {
        return (int)((performerInt * power) * elementDamageRate);
    }

}