using UnityEngine;

public class PlayerMenu_AbilityButtonMangaer : MonoBehaviour
{
    public AbilityMenuManager abilityManager;

    public void ClickedAbilityButton()
    {
        abilityManager.SetAbilityMenu();
    }
}
