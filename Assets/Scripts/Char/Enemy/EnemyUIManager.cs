using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour
{

    /// <summary>
    /// HP�o�[
    /// </summary>
    public CharHPBarUIManager charHPBarUIManager;

    /// <summary>
    /// �G���e�L�X�g
    /// </summary>
    public Text enemyText;



    public void isShowEnemyUI(bool isShow)
    {
        this.gameObject.SetActive(isShow);

    }

}
