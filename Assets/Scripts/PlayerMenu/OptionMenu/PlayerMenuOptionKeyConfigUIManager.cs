using System.Xml.Serialization;
using UnityEngine;

public class PlayerMenuOptionKeyConfigUIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.SetActive(false);
    }


    public void UpdatePlayerMenuDisplay(bool isDisplay)
    {
        this.gameObject.SetActive(isDisplay);

    }
}
