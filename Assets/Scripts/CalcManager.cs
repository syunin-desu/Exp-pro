

// �v�Z�֘A�N���X�Q


/// <summary>
/// battle�֘A�v�Z�N���X
/// </summary>
public class Calc_Battle_Manager 
{
    /// <summary>
    /// �����ɂ��_���[�W���̑������v�Z���Ԃ�
    /// </summary>
    /// <param name="weakElement">��_����</param>
    /// <param name="strongElement">�ϐ�����</param>
    /// <param name="targetChar">�Ώۂ̃L����</param>
    /// <returns>�_���[�W��</returns>
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
    /// ���@�_���[�W���v�Z����
    /// ���s�҂�INT * Power * �����ɂ��_���[�W�{��
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