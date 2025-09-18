using CONST;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.Behavior;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using static UnityEditor.Timeline.TimelinePlaybackControls;

/// <summary>
/// 周辺機器からの入力を受け取るクラス
/// </summary>
public class DeviceInputController : MonoBehaviour
{

    private QuestSceneInputActions questSceneInputActions;

    // クエストシーンで各メニューごとのInputMangerは列挙する
    public QuestInputManager questInputManager;
    public PlayerMenuInputManager playerMenuInputManager;
    public ItemMenuInputManager itemMenuInputManager;
    public AbilityMenuInputManager abilityMenuInputManager;
    public EquipMenuInputManager equipMenuInputManager;
    public SaveAndLoadMenuInputManager saveAndLoadMenuInputManager;
    public OptionMenuInputManager optionMenuInputManager;

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

    private void OnDestroy()
    {
        questSceneInputActions.Dispose();

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
        else if (questSceneInputActions.QuestScene.Enter.triggered)
        {
            currentMenuInputManager.KeyInput_Enter();
        }
        else if (questSceneInputActions.QuestScene.Up.triggered)
        {
            currentMenuInputManager.KeyInput_Up();
        }
        else if (questSceneInputActions.QuestScene.Down.triggered)
        {
            currentMenuInputManager.KeyInput_Down();
        }
        else if (questSceneInputActions.QuestScene.Left.triggered)
        {
            currentMenuInputManager.KeyInput_Left();
        }
        else if (questSceneInputActions.QuestScene.Right.triggered)
        {
            currentMenuInputManager.KeyInput_Right();
        }
        else if (questSceneInputActions.QuestScene.Pause.triggered)
        {
            currentMenuInputManager.KeyInput_Pause();
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
            case CONST.QUEST_MENU_STATUS.MenuStatus.EquipMenu:
                currentMenuInputManager = equipMenuInputManager;
                break;
            case CONST.QUEST_MENU_STATUS.MenuStatus.SaveAndLoadMenu:
                currentMenuInputManager = saveAndLoadMenuInputManager;
                break;
            case CONST.QUEST_MENU_STATUS.MenuStatus.OptionMenu:
                currentMenuInputManager = optionMenuInputManager;
                break;

        }
    }

    public void DisableInputAction()
    {
        this.questSceneInputActions.Disable();
    }

    public List<InputAction> GetInputDeviceInputList()
    {
        return questSceneInputActions.ToList();

    }

    public void UpdateActionKey(bool isKeyboard, string actionID, Delegate onComplete = null)
    {
        questSceneInputActions.Disable();
        var scheme = isKeyboard ? "Keyboard" : "Gamepad";
        var targetScheme = isKeyboard ? "Keyboard" : "Gamepad";
        var targetAction = questSceneInputActions.FindAction(actionID);
        int targetBindingIndex = targetAction.GetBindingIndex(InputBinding.MaskByGroup(scheme));

        InputActionRebindingExtensions.RebindingOperation rebindOperation;
        rebindOperation = targetAction.PerformInteractiveRebinding(targetBindingIndex)
            // TODO キャンセル処理の実装
            .WithControlsHavingToMatchPath(targetScheme)
            .OnComplete(r =>
            {
                r.Dispose();
                if (onComplete != null)
                {
                    var currentTargetAction = questSceneInputActions.FindAction(actionID);
                    onComplete.DynamicInvoke(currentTargetAction);
                }
                questSceneInputActions.Enable();
            })
            .OnCancel(r =>
            {
                r.Dispose();
                questSceneInputActions.Enable();
            })
            .Start();
    }

    public void InitializeDefaultKey(bool isKeyboard)
    {
        var scheme = isKeyboard ? "Keyboard" : "Gamepad";
        foreach (var action in questSceneInputActions)
        {
            foreach (var binding in action.bindings)
            {
                if (binding.effectivePath.Contains(scheme))
                {
                    action.RemoveBindingOverride(binding);
                }

            }

        }
        var test = this.GetInputDeviceInputList();
        Debug.Log(test);
    }
}
