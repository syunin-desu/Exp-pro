using UnityEngine;

public class PlayerMenu_OptionManager : MonoBehaviour
{
    [SerializeField]
    private QuestManager _questManager;
    public PlayerMenuOptionKeyConfigManager playerMenuOptionKeyConfigManager;
    public PlayerMenuOptionUIManager playerMenuOptionUIManager;

    public void OpenKeyConfigMenu(bool isKeyConfig)
    {
        playerMenuOptionKeyConfigManager.IntializeKeyConfigView(isKeyConfig);
    }

    public void SetOptionMenu()
    {
        _questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.OptionMenu);
        playerMenuOptionUIManager.UpdateOptionMenuisActive(true);
    }

    public void CloseOptionMenu()
    {
        _questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.PlayerMainMenu);
        playerMenuOptionUIManager.UpdateOptionMenuisActive(false);
    }
}
