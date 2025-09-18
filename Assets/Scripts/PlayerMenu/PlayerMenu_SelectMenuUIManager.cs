using TMPro;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu_SelectMenuUIManager : MonoBehaviour
{
    /// <summary>
    /// メニューの各ボタンの選択状態を初期化する
    /// </summary>
    public void ClearItemButtonSelected()
    {
        // 既に表示されていれば表示内容を全削除
        var items = GameObject.FindGameObjectsWithTag("PlayerMenuSelectMenuButtons");

        foreach (var item in items)
        {
            item.GetComponent<SelectedStatus>().UpdateIsSelected(false);
        }
    }

    /// <summary>
    /// 全選択アイコンを非活性にする
    /// </summary>
    public void AllSelectedIconDisable()
    {
        var targetobjs = GameObject.FindGameObjectsWithTag("PlayerMenuSelectMenuButtons");

        foreach (var obj in targetobjs)
        {
            obj.GetComponent<Image>().color = Color.white;

        }
    }

    public void UpdateSelectedButtonColor(GameObject targetGameObject)
    {
        targetGameObject.GetComponent<Image>().color = CONST.UI.SELECTED_BUTTON_COLOR;
    }
}
