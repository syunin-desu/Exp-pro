using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MenuSelectionUtility
{
    public void ClickedButtonsInALow(List<GameObject> targetGameObject, CONST.MENU.SELECTEDTYPE menuSelectedType)
    {
        var currentSelectedObject = targetGameObject.FirstOrDefault(b => b.GetComponent<SelectedStatus>().IsSelected);
        if (currentSelectedObject is null)
        {
            currentSelectedObject = targetGameObject[0];
        }

        var updatedButtonIndex = this.GetSelectedIndexOneLineList(targetGameObject, menuSelectedType, currentSelectedObject);

        // currentButtonIndexが
        // 1. 0より少ない
        // 2. targetGameObject.Count()より大きい
        // 3. 想定外のSelectedTypeが選択された場合
        // の時は後続処理は実施しない
        if (updatedButtonIndex < 0 ||
            updatedButtonIndex >= targetGameObject.Count() ||
            updatedButtonIndex == CONST.ILLEGALVALUE.ILLEGALMENUINDEX
            )
            return;

        targetGameObject[updatedButtonIndex].GetComponent<IButtonClicked>().OnClicked();
    }

    public void ClickedButtonsInTwoLow(List<GameObject> targetGameObject, CONST.MENU.SELECTEDTYPE menuSelectedType)
    {
        var currentSelectedObject = targetGameObject.FirstOrDefault(b => b.GetComponent<SelectedStatus>().IsSelected);
        if (currentSelectedObject is null)
        {
            currentSelectedObject = targetGameObject[0];
        }
        int updatedButtonIndex = GetSelectedIndexTwoLineList(targetGameObject, menuSelectedType, currentSelectedObject);

        // currentButtonIndexが
        // 1. 0より少ない
        // 2. targetGameObject.Count()より大きい
        // 3. currentItemHowtoButtonIndexが0または偶数の状態からPrevボタンが押下された場合
        // 4. currentItemHowtoButtonIndexが奇数の状態からNextボタンが押下された場合
        // 5. 想定外のSelectedTypeが選択された場合
        // の時は後続処理は実施しない
        if (updatedButtonIndex < 0 ||
            updatedButtonIndex >= targetGameObject.Count() ||
            ((targetGameObject.IndexOf(currentSelectedObject) % 2 == 0 || targetGameObject.IndexOf(currentSelectedObject) == 0) && targetGameObject.IndexOf(currentSelectedObject) - 1 == updatedButtonIndex) ||
            ((targetGameObject.IndexOf(currentSelectedObject) % 2 != 0) && targetGameObject.IndexOf(currentSelectedObject) + 1 == updatedButtonIndex) ||
            updatedButtonIndex == CONST.ILLEGALVALUE.ILLEGALMENUINDEX
            )
        {
            return;
        }

        targetGameObject[updatedButtonIndex].GetComponent<IButtonClicked>().OnClicked();

    }

    /// <summary>
    /// 一列リストの選択後インデックスを取得(非循環型)
    /// </summary>
    /// <returns></returns>
    private int GetSelectedIndexOneLineList(List<GameObject> targetGameObject, CONST.MENU.SELECTEDTYPE menuSelectedType, GameObject targetObj)
    {
        var currentSelectedHowtoObject = targetObj;
        switch (menuSelectedType)
        {
            case CONST.MENU.SELECTEDTYPE.NEXT:
                return targetGameObject.IndexOf(currentSelectedHowtoObject) + 1;
            case CONST.MENU.SELECTEDTYPE.PREV:
                return targetGameObject.IndexOf(currentSelectedHowtoObject) - 1;
            // 選択肢を循環させる場合
            //case CONST.MENU.SELECTEDTYPE.NEXT:
            //    return (targetGameObject.IndexOf(currentSelectedHowtoObject) + 1) % targetGameObject.Count;
            //case CONST.MENU.SELECTEDTYPE.PREV:
            //    return ((targetGameObject.IndexOf(currentSelectedHowtoObject) - 1) +
            //        targetGameObject.Count) % targetGameObject.Count;
            case CONST.MENU.SELECTEDTYPE.ENTER:
                return targetGameObject.IndexOf(currentSelectedHowtoObject);
            default:
                Debug.Log("ClickedHowItemButtons:想定外の操作が行われました");
                return CONST.ILLEGALVALUE.ILLEGALMENUINDEX;
        }
    }

    /// <summary>
    /// 2列リストの選択後インデックスを取得
    /// </summary>
    /// <param name="targetGameObject"></param>
    /// <param name="menuSelectedType"></param>
    /// <returns></returns>
    private int GetSelectedIndexTwoLineList(List<GameObject> targetGameObject, CONST.MENU.SELECTEDTYPE menuSelectedType, GameObject targetObj)
    {
        var currentSelectedHowtoObject = targetObj;
        switch (menuSelectedType)
        {
            // ↓
            case CONST.MENU.SELECTEDTYPE.NEXT:
                return targetGameObject.IndexOf(currentSelectedHowtoObject) + 2;
            // ↑
            case CONST.MENU.SELECTEDTYPE.PREV:
                return targetGameObject.IndexOf(currentSelectedHowtoObject) - 2;
            // →
            case CONST.MENU.SELECTEDTYPE.NEXTCOLUMN:
                return targetGameObject.IndexOf(currentSelectedHowtoObject) + 1;
            // ←
            case CONST.MENU.SELECTEDTYPE.PREVCOLUMN:
                return targetGameObject.IndexOf(currentSelectedHowtoObject) - 1;
            case CONST.MENU.SELECTEDTYPE.ENTER:
                return targetGameObject.IndexOf(currentSelectedHowtoObject);
            default:
                Debug.Log("ClickedHowItemButtons:想定外の操作が行われました");
                return CONST.ILLEGALVALUE.ILLEGALMENUINDEX;
        }
    }
}
