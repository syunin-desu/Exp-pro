using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public Text hpText;
    public Text maxHpText;
    public Text mpText;

    public Text charName;

    public PartyMember _playerManager;

    void Update()
    {
        hpText.text = string.Format("{0}", _playerManager.GetHp());
        maxHpText.text = string.Format("{0}", _playerManager.GetMaxHp());
        mpText.text = string.Format("{0}", _playerManager.GetMp());
    }

    public void SetUpUI(CharBase player)
    {
        charName.text = string.Format("{0}", player.GetName());
        hpText.text = string.Format("{0}", player.GetHp());
        maxHpText.text = string.Format("{0}", player.GetMaxHp());
        mpText.text = string.Format("{0}", player.GetMp());
    }

}