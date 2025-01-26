using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuStatusUIManager : MonoBehaviour
{
    public Text hpText;
    public Text maxHpText;
    public Text mpText;
    public Text maxMpText;

    public TextMeshProUGUI charName;

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
        }
    }
}
