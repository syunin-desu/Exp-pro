using UnityEngine;

public class Title_LoadButtonClicked : MonoBehaviour
{
    public SaveAndLoadMenuManager SaveAndLoadMenuManager;

    public void OnClickedLoadButton()
    {
        SaveAndLoadMenuManager.SetUpSaveAndLoadManager(false);
    }
}
