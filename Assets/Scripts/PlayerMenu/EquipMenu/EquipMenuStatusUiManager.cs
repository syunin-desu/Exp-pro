using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipMenuStatusUiManager : MonoBehaviour
{
    public Text hpText;
    public Text maxHpText;
    public Text mpText;
    public Text maxMpText;

    public TextMeshProUGUI charName;
    public TextMeshProUGUI str;
    public TextMeshProUGUI def;
    public TextMeshProUGUI spd;
    public TextMeshProUGUI inteligence;

    public PartyMember _playerManager;

    public PlayerMenuUIManager playerMenuUIManager;

    // Update is called once per frame
    void Update()
    {
        if (playerMenuUIManager.IsPlayerMenu())
        {
            hpText.text = string.Format("{0}", _playerManager.GetHp());
            maxHpText.text = string.Format("{0}", _playerManager.GetMaxHp());
            mpText.text = string.Format("{0}", _playerManager.GetMp());
            maxMpText.text = string.Format("{0}", _playerManager.GetMaxMp());
            charName.text = _playerManager.GetName();
            def.text = _playerManager.GetDefence().ToString();
            str.text = _playerManager.GetStrange().ToString();
            spd.text = _playerManager.GetSpeed().ToString();
            inteligence.text = _playerManager.GetSpeed().ToString();
        }
    }
}
