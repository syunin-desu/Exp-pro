using UnityEngine;

public class EquipMenuUIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    /// アビリティメニューの表示する
    /// </summary>
    public void ShowEquipMenu()
    {
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// アビリティメニューを閉じる
    /// </summary>
    public void CloseEquipMenu()
    {
        this.gameObject.SetActive(false);
    }
}
