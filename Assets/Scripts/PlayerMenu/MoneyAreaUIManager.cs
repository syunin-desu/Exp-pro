using UnityEngine;
using UnityEngine.UI;

public class MoneyAreaUIManager : MonoBehaviour
{
    public Text moneyText;

    private int hasMoney;

    public PlayerMenuUIManager playerMenuUIManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasMoney = PlayerData.instance.HasMoney.getHasMoney();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMenuUIManager.IsPlayerMenu())
        {
            moneyText.text = string.Format("{0}", this.hasMoney);
        }
    }
}
