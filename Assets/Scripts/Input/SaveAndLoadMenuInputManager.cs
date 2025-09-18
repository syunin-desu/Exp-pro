using UnityEngine;

public class SaveAndLoadMenuInputManager : PlayerMenuInputManager
{
    public SaveAndLoadMenuManager saveAndLoadMenuManager;

    public override void KeyInput_OpenMenu()
    {
        this.saveAndLoadMenuManager.CloseSaveAndLoadMenu();
        base.KeyInput_OpenMenu();
    }

    public override void KeyInput_Return()
    {
        this.saveAndLoadMenuManager.CloseSaveAndLoadMenu();
    }

    public override void KeyInput_Up()
    {
        saveAndLoadMenuManager.SaveAndLoadFromBasisKeyInput(CONST.MENU.SELECTEDTYPE.PREV);
    }

    public override void KeyInput_Down()
    {
        saveAndLoadMenuManager.SaveAndLoadFromBasisKeyInput(CONST.MENU.SELECTEDTYPE.NEXT);
    }

    public override void KeyInput_Enter()
    {
        saveAndLoadMenuManager.SaveAndLoadFromBasisKeyInput(CONST.MENU.SELECTEDTYPE.ENTER);
    }
}
