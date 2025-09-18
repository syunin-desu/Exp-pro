using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "CreateData/Create CardProperty")]
public class CardProperty : ScriptableObject
{
    public int orderID;

    public int parallelNumber;

    public int cardCount;

    [ValueDropdown("card_types")]
    public CONST.QUEST.CardType cartType;


    // TODO: モック実装ではあるが、将来的にはレベルごとのテーブルを指定する方式にしたい
    public int creditValue;

    public List<string> itemName;

    public string enemyDataName;

    /// <summary>
    /// アビリティ発動時に実行されるアクション
    /// </summary>
    private static List<CONST.QUEST.CardType> card_types = Enum.GetValues(typeof(CONST.QUEST.CardType))
                                                         .Cast<CONST.QUEST.CardType>()
                                                         .ToList();
}
