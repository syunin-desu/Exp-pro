using UnityEngine;

public class ItemMenuHowToButtonClicked : MonoBehaviour, IButtonClicked
{
    public ItemMenuManager imanager;

    public void OnClicked()
    {
        if (this.gameObject.GetComponent<SelectedStatus>().GetIsSelected())
        {
            imanager.SetUpItemSelectedMenu();

        }
        else
        {
            imanager.ResetHowButtonSelected(this.gameObject);

        }
    }
}
