using CONST;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 周辺機器からの入力を受け取るクラス
/// </summary>
public class DeviceInputController : MonoBehaviour
{

    public QuestSceneInputActions questSceneInputActions;

    // クエストシーンで各メニューごとのInputMangerは列挙する
    public QuestInputManager questInputManager;
    public PlayerMenuInputManager playerMenuInputManager;
    public ItemMenuInputManager itemMenuInputManager;
    public AbilityMenuInputManager abilityMenuInputManager;
    public EquipMenuInputManager equipMenuInputManager;

    [SerializeField]
    private QuestManager _questManager;

    // この変数に格納されているInputManagerが適用される
    private IInputAction currentMenuInputManager;

    private CONST.QUEST_MENU_STATUS.MenuStatus menuStatus;

    private void Start()
    {
        questSceneInputActions = new QuestSceneInputActions();
        questSceneInputActions.Enable();
        menuStatus = QUEST_MENU_STATUS.MenuStatus.Main;
        currentMenuInputManager = questInputManager;
    }

    // Update is called once per frame
    void Update()
    {
        // 毎フレームメニューが変化したかチェック
        if (menuStatus != _questManager.GetCurrentMenuStatus())
        {

            // メニューが切り替わったらInputManagerを更新
            menuStatus = _questManager.GetCurrentMenuStatus();
            this.ChangeInputManager(menuStatus);
            Debug.Log("更新後のMenuStatus:" + menuStatus);
        }

        if (questSceneInputActions.QuestScene.OpenMenu.triggered)
        {
            currentMenuInputManager.KeyInput_OpenMenu();
        }
        else if (questSceneInputActions.QuestScene.Return.triggered)
        {
            currentMenuInputManager.KeyInput_Return();
        }

    }

    public void ChangeInputManager(CONST.QUEST_MENU_STATUS.MenuStatus currentStatus)
    {
        switch (currentStatus)
        {
            case CONST.QUEST_MENU_STATUS.MenuStatus.Main:
                currentMenuInputManager = questInputManager;
                break;
            case CONST.QUEST_MENU_STATUS.MenuStatus.PlayerMainMenu:
                currentMenuInputManager = playerMenuInputManager;
                break;
            case CONST.QUEST_MENU_STATUS.MenuStatus.ItemMenu:
                currentMenuInputManager = itemMenuInputManager;
                break;
            case CONST.QUEST_MENU_STATUS.MenuStatus.AbilityMenu:
                currentMenuInputManager = abilityMenuInputManager;
                break;
        }
    }

    public void DisableInputAction()
    {
        this.questSceneInputActions.Disable();
    }

}
