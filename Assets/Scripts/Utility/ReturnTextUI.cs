using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTextUI : MonoBehaviour
{
    /// <summary>
    /// 戻るテキストの表示非表示を管理
    /// </summary>
    public void ManageReturnTextUI(bool isShowReturnTextUI)
    {
        this.gameObject.SetActive(isShowReturnTextUI);
    }
}
