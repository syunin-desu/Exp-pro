using UnityEngine;

public class PlayerMenuOptionKeyConfigClicked : MonoBehaviour
{
    public PlayerMenu_OptionManager optionManager;

    public void OnClickedKeyConfigButtonClicked()
    {
        optionManager.OpenKeyConfigMenu(true);
    }

    public void OnClickedGamepadConfigButtonClicked()
    {
        optionManager.OpenKeyConfigMenu(false);
    }
}
