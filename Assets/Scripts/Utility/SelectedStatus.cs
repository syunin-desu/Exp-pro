using UnityEngine;

public class SelectedStatus : MonoBehaviour
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
