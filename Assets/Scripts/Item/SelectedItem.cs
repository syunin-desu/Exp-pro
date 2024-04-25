using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedItem : MonoBehaviour
{
    // eventSystemを取得するための変数宣言
    [SerializeField] EventSystem eventSystem;
    public ItemManager _itemManager;
    public BattleManager _battleManager;
    public PartyMember _playerManager;


    /// <summary>
    /// 表示されているアイテムがクリックした時の処理
    /// </summary>
    /// <param name="charactor">キャラ</param>
    public void OnClickItem(CharBase charactor)
    {
        // TODO 引数CharBaseでパーティの誰かを特定できるようにする
        GameObject selectedObj = eventSystem.currentSelectedGameObject.gameObject;
        string selectedItemDisplayName = selectedObj.GetComponentInChildren<Text>().text;

        string selectedAbilityName = _itemManager.getItemNameForDisplayName(selectedItemDisplayName);
        _battleManager.setAction_Item(_playerManager, selectedAbilityName);


    }
}
