using System.Linq;
using UnityEngine;

public class SaveMenuSaveContentButton : MonoBehaviour, IButtonClicked
{
    public SaveAndLoadMenuManager saveAndLoadMenuManager;
    public SelectedStatus selectedStatus;
    public int saveSlotNo;

    public void OnClicked()
    {
        if (!selectedStatus.GetIsSelected())
        {
            saveAndLoadMenuManager.UpdateSelectedSlot(this.gameObject);
            return;
        }

        if (saveAndLoadMenuManager.isSave)
        {
            saveAndLoadMenuManager.ExecuteSave(this.saveSlotNo, this.gameObject);
        }
        else
        {
            saveAndLoadMenuManager.ExecuteLoad(this.saveSlotNo);

            // TODO:シーンの再描画
        }


        // セーブに失敗した場合はUI変更なし
    }
}
