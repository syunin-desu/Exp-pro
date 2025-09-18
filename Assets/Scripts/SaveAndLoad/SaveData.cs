using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    // パーティーデータ
    // TODO:現状はひとりとする
    public List<CharParameter> PartyMember = new List<CharParameter>();

    // 所持アイテムデータと所持数
    public Dictionary<BaseItemData, int> HaveItemList = new Dictionary<BaseItemData, int>();

    // 所持金
    public int HasMoney;

    // 現在のフロア
    public int currentFloor;

    // フロア名
    public string currentFloorName;

    // 現在のフロアのカード状況
    public List<BaseCardProperty> currentCardList;

    // 選択可能なカード枚数
    public int canSelectCardNumber;
}
