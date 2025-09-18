using UnityEngine;

public class PlayerMenu_EquipButtonManager : MonoBehaviour, IButtonClicked
{
    public EquipMenuManager equipMenuManager;
    public PlayerMenu_SelectMenuUIManager playerMenu_SelectMenuUIManager;
    public SelectedStatus playerMenu_SelectMenuStatus;

    public void OnClicked()
    {
        if (playerMenu_SelectMenuStatus.GetIsSelected())
        {
            equipMenuManager.ShowEquipMenu();
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
