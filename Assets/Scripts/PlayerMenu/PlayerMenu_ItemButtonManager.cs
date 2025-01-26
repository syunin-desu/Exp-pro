using UnityEngine;

public class PlayerMenu_ItemButtonManager : MonoBehaviour
{
    public ItemMenuManager itemMenuManager;

    public void ClickedItemButton()
    {
        itemMenuManager.SetUpItemMenu();
    }
}
