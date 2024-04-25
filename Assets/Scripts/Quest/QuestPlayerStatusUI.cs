using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPlayerStatusUI : MonoBehaviour
{
    public Text hpText;
    public Text maxHpText;
    public Text mpText;
    public Text maxMpText;

    public Text charName;

    public PartyMember _playerManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = string.Format("{0}", _playerManager.GetHp());
        maxHpText.text = string.Format("{0}", _playerManager.GetMaxHp());
        mpText.text = string.Format("{0}", _playerManager.GetMp());
        maxMpText.text = string.Format("{0}", _playerManager.GetMaxMp());
        charName.text =  _playerManager.GetName();
    }
}
