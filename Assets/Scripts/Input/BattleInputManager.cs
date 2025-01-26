using UnityEngine;

/// <summary>
/// バトル時の入力クラス
/// </summary>
public class BattleInputManager : MonoBehaviour
{
    public ItemUIManager itemUIManager;

    public AbilityUIManager abilityUIManager;

    public BattleManager battleManager;

    private delegate void windowClosedAction();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.PressedEscape();
        }
    }


    /// <summary>
    /// ESCが押されたときの処理
    /// </summary>
    private void PressedEscape()
    {
        // アイテムウインドウが表示されているとき
        if (itemUIManager.getIsItemUIWindowActive())
        {
            itemUIManager.removeItemWindow();
        }
        // アビリティウインドウが表示されているとき
        else if (abilityUIManager.getIsAbilityWindowActive())
        {
            abilityUIManager.removeAbilityWindow();
        }
        // ウインドウが開かれていないとき
        else
        {
            battleManager.battleActionList.RemoveLatestAction();
        }
    }
}
