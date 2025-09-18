using TMPro;
using UnityEngine;

public class PlayerMenuOptionKeyConfigContentUIManager : MonoBehaviour
{
    public TextMeshProUGUI keyLabel;
    public TextMeshProUGUI buttonText;

    public void UpdateKeyContentTexts(string keyLabel, string buttonText)
    {
        this.keyLabel.text = keyLabel;
        this.buttonText.text = buttonText;
    }

    public void UpdateKeyText(string buttonText)
    {
        this.buttonText.text = buttonText;
    }
}
