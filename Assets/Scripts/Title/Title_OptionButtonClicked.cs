using UnityEngine;

public class Title_OptionButtonClicked : MonoBehaviour
{
    public PlayerMenuOptionUIManager optionUIManager;

    public void OnClickedOptionMenuButton()
    {
        optionUIManager.UpdateOptionMenuisActive(true);
    }
}
