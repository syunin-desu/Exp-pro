using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// HPバーUI
/// </summary>
public class CharHPBarUIManager : MonoBehaviour
{
    [SerializeField] private Image healthImage;

    public float duration = 0.5f;
    public float strength = 20f;
    public int vibrate = 100;

    /// <summary>
    /// HPバーに値を設定する
    /// </summary>
    /// <param name="value"></param>
    public void SetGauge(float value)
    {
        // DoTweenを連結して動かす
        healthImage.DOFillAmount(value, duration);
    }
}
