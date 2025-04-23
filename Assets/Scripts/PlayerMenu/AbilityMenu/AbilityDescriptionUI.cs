using TMPro;
using UnityEngine;

public class AbilityDescriptionUI : MonoBehaviour
{
    public TextMeshProUGUI AbilityDescription;
    private string discription;

    private void Update()
    {
        AbilityDescription.text = discription;
    }

    public void SetDiscription(string updateDescription)
    {
        this.discription = updateDescription;
    }
}
