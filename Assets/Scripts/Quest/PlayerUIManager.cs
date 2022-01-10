using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public Text hpText;
    public Text atText;

    public void SetUpUI(PlayerManager player)
    {
        hpText.text = string.Format("HP:{0}", player.HP);
        atText.text = string.Format("AT:{0}", player.STRANGE);
    }
    public void UpdateUI(PlayerManager player)
    {
        hpText.text = string.Format("HP:{0}", player.HP);
    }

}