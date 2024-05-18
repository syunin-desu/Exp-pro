using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// HP�o�[UI
/// </summary>
public class CharHPBarUIManager : MonoBehaviour
{
    [SerializeField] private Image healthImage;

    public float duration = 0.5f;
    public float strength = 20f;
    public int vibrate = 100;

    /// <summary>
    /// HP�o�[�ɒl��ݒ肷��
    /// </summary>
    /// <param name="value"></param>
    public void SetGauge(float value)
    {
        // DoTween��A�����ē�����
        healthImage.DOFillAmount(value, duration);
    }
}
