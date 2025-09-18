using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using System;

public class PlayerMenuManager : MonoBehaviour
{
    public List<GameObject> selectableObjects;
    public PlayerMenu_SelectMenuUIManager playerMenu_SelectMenuUIManager;
    [SerializeField]
    private PlayerMenuUIManager playerMenuUIManager;
    [SerializeField]
    private MenuSelectionUtility gameObjectUtility = new MenuSelectionUtility();

    public void InitializePlayerMenuSelectButtons()
    {
        ///メニュー選択ボタンの選択中ステータスをFalseにする
        playerMenu_SelectMenuUIManager.ClearItemButtonSelected();
        playerMenu_SelectMenuUIManager.AllSelectedIconDisable();

        // アイテムを初期選択状態にする
        this.ClearButtonSelectedInList();
        playerMenu_SelectMenuUIManager.AllSelectedIconDisable();
        var targetObject = selectableObjects[0];
        targetObject.GetComponent<SelectedStatus>().UpdateIsSelected(true);
        playerMenu_SelectMenuUIManager.UpdateSelectedButtonColor(targetObject);
    }

    public void ClosePlayerMenu()
    {
        this.ClearButtonSelectedInList();
        playerMenu_SelectMenuUIManager.AllSelectedIconDisable();
        this.playerMenuUIManager.ClosePlayerMenu();
    }

    public void MenuButtonFromKeyInput(CONST.MENU.SELECTEDTYPE sekectedType)
    {

        gameObjectUtility.ClickedButtonsInALow(selectableObjects, sekectedType);
    }

    private void ClearButtonSelectedInList()
    {
        foreach (var item in selectableObjects)
        {
            item.GetComponent<SelectedStatus>().UpdateIsSelected(false);
        }
    }

}
