using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EquipMenuStatusUiManager : MonoBehaviour
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

    public List<EquipMenuUpdateParameterUIManager> UpdateUIs = new List<EquipMenuUpdateParameterUIManager>();

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

    public void ClearUpdateParameter()
    {
        foreach (var item in UpdateUIs)
        {
            item.UpdateParameterEnable(false);
        }
    }


    public void UpdateWillCharPrameter(
        bool isBeforeUpdate = false
        )
    {
        foreach (var ui in UpdateUIs)
        {
            switch (ui.parameterCategory)
            {
                case CONST.CHARCTOR.ParameterCategory.ATTACK:
                    ui.UpdateParameter(_playerManager.GetAttackParameter(), _playerManager.GetAttackParameter(isBeforeUpdate));
                    break;
                case CONST.CHARCTOR.ParameterCategory.DEFENCE:
                    ui.UpdateParameter(_playerManager.GetDefenceParameter(), _playerManager.GetDefenceParameter(isBeforeUpdate));
                    break;
                case CONST.CHARCTOR.ParameterCategory.MAXHP:
                    ui.UpdateParameter(_playerManager.GetMaxHp(), _playerManager.GetMaxHp(isBeforeUpdate));
                    break;
                case CONST.CHARCTOR.ParameterCategory.MAXMP:
                    ui.UpdateParameter(_playerManager.GetMaxMp(), _playerManager.GetMaxMp(isBeforeUpdate));
                    break;
                case CONST.CHARCTOR.ParameterCategory.STR:
                    ui.UpdateParameter(_playerManager.GetStrange(), _playerManager.GetStrange(isBeforeUpdate));
                    break;
                case CONST.CHARCTOR.ParameterCategory.DEF:
                    ui.UpdateParameter(_playerManager.GetDefence(), _playerManager.GetDefence(isBeforeUpdate));
                    break;
                case CONST.CHARCTOR.ParameterCategory.SPD:
                    ui.UpdateParameter(_playerManager.GetSpeed(), _playerManager.GetSpeed(isBeforeUpdate));
                    break;
                case CONST.CHARCTOR.ParameterCategory.INT:
                    ui.UpdateParameter(_playerManager.GetInteli(), _playerManager.GetInteli(isBeforeUpdate));
                    break;
                case CONST.CHARCTOR.ParameterCategory.MGC:
                    ui.UpdateParameter(_playerManager.GetMagicPoser(), _playerManager.GetMagicPoser(isBeforeUpdate));
                    break;
                case CONST.CHARCTOR.ParameterCategory.KID:
                    ui.UpdateParameter(_playerManager.GetKindness(), _playerManager.GetKindness(isBeforeUpdate));
                    break;
            }
        }
    }

    public void UpdateActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }
}
