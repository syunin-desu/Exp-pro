using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTextUI : MonoBehaviour
{
    /// <summary>
    /// �߂�e�L�X�g�̕\����\�����Ǘ�
    /// </summary>
    public void ManageReturnTextUI(bool isShowReturnTextUI)
    {
        this.gameObject.SetActive(isShowReturnTextUI);
    }
}
