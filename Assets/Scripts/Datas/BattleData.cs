using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// battle�f�[�^
/// �o������G����ۊǂ��Abattle�V�[���\�����ɌĂяo��
/// </summary>
public class BattleData : MonoBehaviour
{
    /// <summary>
    /// �C���X�^���X
    /// </summary>
    public static BattleData instance;

    // �\������G���X�g

    // 

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
