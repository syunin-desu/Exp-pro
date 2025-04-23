using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class HavingItem
{
    public string id;
    public string name;
    public int count;

}

/// <summary>
/// プレイヤーが所持しているアイテムに関しての管理クラス
/// </summary>
public class HadItem : MonoBehaviour
{

    private List<HavingItem> havingItems;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        havingItems = PlayerData.instance.HaveItemList.Select(v => new HavingItem
        {
            id = v.Key.id,
            name = v.Key.name,
            count = v.Value
        }).ToList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// アイテム個数を減少させる
    /// </summary>
    /// <param name="itemName">対象アイテム名</param>
    /// <param name="reduceItemCount">減少させる個数</param>
    public void reduceItemCount(string itemID, int reduceItemCount)
    {
        var targetItem = this.havingItems.Find(item => item.id == itemID);

        //アイテム数を減少させる
        targetItem.count -= reduceItemCount;

        // アイテムがなくなったら削除
        if (targetItem.count <= 0)
        {
            this.havingItems.Remove(targetItem);
        }

    }

    //所持アイテム
    public List<HavingItem> GetHavingItem()
    {
        return this.havingItems;
    }

#nullable enable
    // アイテムの個数を取得
    public int? GetItemCount(string itemID)
    {
        return this.havingItems.FirstOrDefault(i => i.id == itemID)?.count;
    }

#nullable disable

}
