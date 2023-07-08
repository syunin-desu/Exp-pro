using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �N�G�X�g�֌W�̃f�[�^
/// �N�G�X�g�V�[�����ɐ��������
/// </summary>
public class QuestData : SerializedMonoBehaviour
{

    /// <summary>
    /// �C���X�^���X
    /// </summary>
    public static QuestData instance;

    // �C�x���g�^�C�����X�g

    // ���݂̊K�w
    public int currentFloor;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
