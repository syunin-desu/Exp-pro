using UnityEngine;

public class ItemMenuArrangeButtonClicked : MonoBehaviour, IButtonClicked
{
    public ItemMenuManager imanager;

    public void OnClicked()
    {
        if (this.gameObject.GetComponent<SelectedStatus>().GetIsSelected())
        {
            // TBD
        }
        else
        {
            imanager.ResetHowButtonSelected(this.gameObject);
        }

    }
}
