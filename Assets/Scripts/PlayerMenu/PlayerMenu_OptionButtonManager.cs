using System.Xml.Serialization;
using UnityEngine;

public class PlayerMenu_OptionButtonManager : MonoBehaviour, IButtonClicked
{
    public PlayerMenu_OptionManager optionManager;
    public PlayerMenu_SelectMenuUIManager playerMenu_SelectMenuUIManager;
    public SelectedStatus playerMenu_SelectMenuStatus;

    public void OnClicked()
    {
        if (playerMenu_SelectMenuStatus.GetIsSelected())
        {
            optionManager.SetOptionMenu();

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
