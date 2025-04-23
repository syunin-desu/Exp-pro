using UnityEngine;

public class PlayerMenu_EquipButtonManager : MonoBehaviour
{
    public EquipMenuManager equipMenuManager;

    public void ClickedEquipButton()
    {
        equipMenuManager.ShowEquipMenu();
    }
}
