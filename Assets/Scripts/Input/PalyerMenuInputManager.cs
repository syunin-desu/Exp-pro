using UnityEngine;

public class PlayerMenuInputManager : MonoBehaviour, IInputAction
{
    public QuestManager questManager;

    /// <summary>
    /// TABƒL[‰Ÿ‰º
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
}
