using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuStatusUIManager : MonoBehaviour
{
    //public Text attack;
    //public Text defence;
    public Text maxHP;
    public Text currentHP;
    public Text maxMp;
    public Text currentMP;

    public TextMeshProUGUI playerName;
    public TextMeshProUGUI str;
    public TextMeshProUGUI def;
    public TextMeshProUGUI spd;
    public TextMeshProUGUI mgc;
    public TextMeshProUGUI inteligence;
    public TextMeshProUGUI kid;

    public PartyMember _playerManager;

    public PlayerMenuUIManager playerMenuUIManager;


    // Update is called once per frame
    void Update()
    {
        if (playerMenuUIManager.IsPlayerMenu())
        {
            playerName.text = _playerManager.GetName();
            //attack.text = string.Format("{0}", _playerManager.GetAttackParameter());
            //defence.text = string.Format("{0}", _playerManager.GetDefenceParameter());
            maxHP.text = string.Format("{0}", _playerManager.GetMaxHp());
            maxMp.text = string.Format("{0}", _playerManager.GetMaxMp());
            currentHP.text = _playerManager.GetHp().ToString();
            currentMP.text = _playerManager.GetMp().ToString();
            def.text = _playerManager.GetDefence().ToString();
            str.text = _playerManager.GetStrange().ToString();
            spd.text = _playerManager.GetSpeed().ToString();
            mgc.text = _playerManager.GetMagicPoser().ToString();
            inteligence.text = _playerManager.GetSpeed().ToString();
            kid.text = _playerManager.GetKindness().ToString();
        }
    }
}
