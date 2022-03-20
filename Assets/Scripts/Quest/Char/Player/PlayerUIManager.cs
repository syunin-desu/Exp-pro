using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public Text hpText;
    public Text charName;

    public void SetUpUI(PlayerManager player)
    {
        charName.text = string.Format("{0}", player.GetName());
        hpText.text = string.Format("{0} / {1}", player.GetHp(), player.GetMaxHp());
    }
    public void UpdateUI(PlayerManager player)
    {
        hpText.text = string.Format("HP:{0}", player.GetHp());
    }

}