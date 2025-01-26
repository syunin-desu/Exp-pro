using UnityEngine;

public class ItemMenuUseButton : MonoBehaviour
{
    [SerializeField]
    private ItemMenuManager _itemMenuManager;

    public void SelectedUseButton()
    {
        _itemMenuManager.UpdateCanSelectHowItemButtons(false);
        _itemMenuManager.UpdateCanSelectItemButton(true);
        _itemMenuManager.SetCurrentItemMenuStatus(CONST.ITEM_MENU_STATUS.MenuStatus.SelectItem);
    }
}
