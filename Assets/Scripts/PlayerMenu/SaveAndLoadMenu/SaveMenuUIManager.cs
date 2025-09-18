using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine.UI;

public class SaveMenuUIManager : MonoBehaviour
{
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

    public void UpdateSaveMenuActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }

    public void resetListScroll(RectTransform targetContent)
    {
        this.listUtility.ScrollToTarget(this.scrollRect, scrollContent, targetContent);
    }

    public void InitializeAllSlotColor(List<GameObject> slots)
    {
        foreach (var slot in slots)
        {
            slot.gameObject.GetComponent<Image>().color = new Color(0, 0, 255, 0);
        }
    }


    public void UpdateSlotColor(GameObject targetSlot)
    {
        targetSlot.gameObject.GetComponent<Image>().color = new Color(0, 0, 255, 1);
    }
}
