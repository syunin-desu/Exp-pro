using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInputManager : MonoBehaviour, IInputAction
{
    [SerializeField]
    private QuestManager _questManager;


    /// <summary>
    /// TABƒL[‰Ÿ‰º
    /// </summary>
    public void KeyInput_OpenMenu()
    {
        _questManager.ShowPlayerMenu();
    }

}
