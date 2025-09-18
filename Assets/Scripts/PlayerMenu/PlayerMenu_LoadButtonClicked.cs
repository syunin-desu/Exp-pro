using UnityEngine;

public class PlayerMenu_LoadButtonClicked : MonoBehaviour, IButtonClicked
{
    public SaveAndLoadMenuManager SaveAndLoadMenuManager;
    public QuestManager questManager;
    public PlayerMenu_SelectMenuUIManager playerMenu_SelectMenuUIManager;
    public SelectedStatus playerMenu_SelectMenuStatus;

    public void OnClicked()
    {
        if (playerMenu_SelectMenuStatus.GetIsSelected())
        {
            questManager.UpdateCurrentMenuStatus(CONST.QUEST_MENU_STATUS.MenuStatus.SaveAndLoadMenu);
            SaveAndLoadMenuManager.SetUpSaveAndLoadManager(false);
        }
        else
        {
            ///全アイテムの選択中ステータスをFalseにする
            playerMenu_SelectMenuUIManager.ClearItemButtonSelected();
            playerMenu_SelectMenuUIManager.AllSelectedIconDisable();

            // このボタンを選択状態に更新
            playerMenu_SelectMenuUIManager.UpdateSelectedButtonColor(this.gameObject);
            playerMenu_SelectMenuStatus.UpdateIsSelected(true);
        }
    }
}
