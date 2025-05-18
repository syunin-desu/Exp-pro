using TMPro;
using UnityEngine;

public class EquipMenuDescriptionUI : MonoBehaviour
{
    public TextMeshProUGUI ItemDescription;
    private string discription;

    private void Update()
    {
        ItemDescription.text = discription;
    }

    /// <summary>
    /// DiscriptionÇçXêV
    /// </summary>
    /// <param name="updateDiscription"></param>
    public void SetDiscription(string updateDiscription)
    {
        this.discription = updateDiscription;
    }
}
