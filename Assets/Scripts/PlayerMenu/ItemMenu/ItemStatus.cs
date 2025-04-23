using UnityEngine;

public class ItemStatus : MonoBehaviour
{

    public bool IsSelected;

    public bool GetIsSelected()
    {
        return IsSelected;
    }

    public void UpdateIsSelected(bool isSelected)
    {
        this.IsSelected = isSelected;
    }
}
