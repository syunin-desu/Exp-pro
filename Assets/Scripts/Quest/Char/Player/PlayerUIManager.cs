using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public Text hpText;
    public Text maxHpText;

    public Text charName;

    public PlayerManager _playerManager;

    void Update()
    {
        hpText.text = string.Format("{0}", _playerManager.GetHp());
        maxHpText.text = string.Format("{0}", _playerManager.GetMaxHp());
    }

    public void SetUpUI(CharBase player)
    {
        charName.text = string.Format("{0}", player.GetName());
        hpText.text = string.Format("{0}", player.GetHp());
        maxHpText.text = string.Format("{0}", player.GetMaxHp());
    }

}