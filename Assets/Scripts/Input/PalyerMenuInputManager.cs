using UnityEngine;

public class PlayerMenuInputManager : MonoBehaviour, IInputAction
{
    public QuestManager questManager;
    public PlayerMenuManager playerMenuManager;

    /// <summary>
    /// TABÉLÅ[âüâ∫éû
    /// </summary>
    public virtual void KeyInput_OpenMenu()
    {
        this.Close_PlayerMenu();
    }

    public virtual void KeyInput_Return()
    {
        this.Close_PlayerMenu();
    }

    private protected void Close_PlayerMenu()
    {

        questManager.ClosePlayerMenu();
    }

    public virtual void KeyInput_Up()
    {
        playerMenuManager.MenuButtonFromKeyInput(CONST.MENU.SELECTEDTYPE.PREV);
    }

    public virtual void KeyInput_Down()
    {
        playerMenuManager.MenuButtonFromKeyInput(CONST.MENU.SELECTEDTYPE.NEXT);
    }

    public virtual void KeyInput_Enter()
    {
        playerMenuManager.MenuButtonFromKeyInput(CONST.MENU.SELECTEDTYPE.ENTER);
    }

    public virtual void KeyInput_Left()
    {

    }

    public virtual void KeyInput_Right()
    {
    }
}
