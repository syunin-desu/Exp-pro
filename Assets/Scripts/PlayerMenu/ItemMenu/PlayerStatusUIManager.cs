using TMPro;
using UnityEngine;

public class PlayerStatusUIManager : MonoBehaviour
{
    public TextMeshProUGUI charName;
    public TextMeshProUGUI maxHpText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI maxMpText;
    public TextMeshProUGUI mpText;

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
