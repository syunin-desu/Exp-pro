using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class EquipMenuUIManager : MonoBehaviour
{
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI headName;
    public TextMeshProUGUI bodyName;
    public TextMeshProUGUI Accessory1Name;
    public TextMeshProUGUI Accessory2Name;
    public ItemManager itemManager;

    private ListViewUtility listUtility = new ListViewUtility();


    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private RectTransform scrollContent;

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

    public void UpdateArmedEquipName(PlayerEquipData playerEquipData)
    {
        weaponName.text = playerEquipData.weaponData.displayName;
        headName.text = playerEquipData.armedHead.displayName;
        bodyName.text = playerEquipData.armedBody.displayName;
        Accessory1Name.text = playerEquipData.armedAccessory_1.displayName;
        Accessory2Name.text = playerEquipData.armedAccessory_2.displayName;

    }

    public void ClearEquipPartsSelected()
    {
        var targetobjs = GameObject.FindGameObjectsWithTag("PlayerMenuEquipSelectedIcon");

        foreach (var obj in targetobjs)
        {
            obj.GetComponent<TextMeshProUGUI>()
                .alpha = 0;

        }
    }

    public void ClearEquipItemSelected()
    {
        var targetobjs = GameObject.FindGameObjectsWithTag("PlayerMenuEquipItemSelectedIcon");

        foreach (var obj in targetobjs)
        {
            obj.GetComponent<TextMeshProUGUI>()
                .alpha = 0;

        }
    }

    public void UpdateEnableEquipParts(bool isEnable)
    {
        var targetobjs = GameObject.FindGameObjectsWithTag("PlayerMenuEquipButton");

        foreach (var obj in targetobjs)
        {
            obj.GetComponentInChildren<Button>().enabled = isEnable;

        }
    }

    public void UpdateEnableEquip(bool isEnable)
    {
        var targetobjs = GameObject.FindGameObjectsWithTag("PlayerMenuEquipItemButton");
        foreach (var obj in targetobjs)
        {
            obj.GetComponentInChildren<Button>().enabled = isEnable;

        }
    }

    public void UpdateEquipPartsSelectedIcon(TextMeshProUGUI obj, bool isEnable)
    {
        obj.alpha = isEnable ? 100 : 0;
    }

    public void resetListScroll(RectTransform targetContent)
    {
        this.listUtility.ScrollToTarget(this.scrollRect, scrollContent, targetContent);
    }
}
