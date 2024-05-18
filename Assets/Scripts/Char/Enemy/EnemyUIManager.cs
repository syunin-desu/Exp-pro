using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour
{

    /// <summary>
    /// HPバー
    /// </summary>
    public CharHPBarUIManager charHPBarUIManager;

    /// <summary>
    /// 敵名テキスト
    /// </summary>
    public Text enemyText;



    public void isShowEnemyUI(bool isShow)
    {
        this.gameObject.SetActive(isShow);

    }

}
