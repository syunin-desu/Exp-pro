using UnityEngine;

public class OptionMenuInputManager : PlayerMenuInputManager
{
    public PlayerMenu_OptionManager PlayerMenu_OptionManager;

    public override void KeyInput_OpenMenu()
    {
        this.Close_OptionMenu();
        base.KeyInput_OpenMenu();
    }

    public override void KeyInput_Return()
    {
        this.Close_OptionMenu();
    }

    private void Close_OptionMenu()
    {
        PlayerMenu_OptionManager.CloseOptionMenu();
    }
}
