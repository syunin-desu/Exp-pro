using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuStatusUIManager : MonoBehaviour
{
    public Text attack;
    public Text defence;
    public Text maxHP;
    public Text maxMp;

    public Text str;
    public Text def;
    public Text spd;
    public Text mgc;
    public Text inteligence;
    public Text kid;



    public PartyMember _playerManager;

    public PlayerMenuUIManager playerMenuUIManager;

    // Update is called once per frame
    void Update()
    {
        if (playerMenuUIManager.IsPlayerMenu())
        {
            attack.text = string.Format("{0}", _playerManager.GetAttackParameter());
            defence.text = string.Format("{0}", _playerManager.GetDefenceParameter());
            maxHP.text = string.Format("{0}", _playerManager.GetMaxHp());
            maxMp.text = string.Format("{0}", _playerManager.GetMaxMp());
            def.text = _playerManager.GetDefence().ToString();
            str.text = _playerManager.GetStrange().ToString();
            spd.text = _playerManager.GetSpeed().ToString();
            mgc.text = _playerManager.GetMagicPoser().ToString();
            inteligence.text = _playerManager.GetSpeed().ToString();
            kid.text = _playerManager.GetKindness().ToString();
        }
    }
}
