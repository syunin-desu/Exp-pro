using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バトル時の入力クラス
/// </summary>
public class BattleInputManager : MonoBehaviour
{
    public ItemUIManager itemUIManager;

    public AbilityUIManager abilityUIManager;

    public BattleManager battleManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (itemUIManager.getIsItemUIWindowActive())
            {
                itemUIManager.removeItemWindow();
            }
            else if (abilityUIManager.getIsAbilityWindowActive())
            {
                abilityUIManager.removeAbilityWindow();
            }
            else
            {
                battleManager.battleActionList.RemoveLatestAction();
            }
        }
    }
}
