using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �s�����X�gUI
/// </summary>
public class ActionRowNumberListUI : MonoBehaviour
{
    /// <summary>
    /// �A�C�e���E�C���h�E��\���A��\���ɂ���
    /// </summary>
    /// <param name="isShowItemWindow">�A�C�e���E�C���h�E�̕\���A��\��</param>
    public void manageShowActionListUI(bool isShowActionListUI)
    {

        this.gameObject.SetActive(isShowActionListUI);
    }
}
