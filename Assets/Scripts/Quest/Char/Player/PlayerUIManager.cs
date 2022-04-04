using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public Text hpText;
    public Text maxHpText;

    public Text charName;

    public void SetUpUI(CharBase player)
    {
        charName.text = string.Format("{0}", player.GetName());
        hpText.text = string.Format("{0}", player.GetHp());
        maxHpText.text = string.Format("{0}", player.GetMaxHp());
    }
    public void UpdateUI(CharBase player)
    {
        hpText.text = string.Format("{0}", player.GetHp());
        maxHpText.text = string.Format("{0}", player.GetMaxHp());
    }

}