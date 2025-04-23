using UnityEngine;

public class ItemMenuHowToButtonClicked : MonoBehaviour
{
    public ItemMenuManager imanager;

    public void HowToButtonClicked()
    {
        imanager.SetUpItemSelectedMenu();
    }
}
