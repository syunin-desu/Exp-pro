using UnityEngine;

public class PlayerMenuOptionDefaultButtonClicked : MonoBehaviour
{

    public void OnClickedDefaultValueButton()
    {
        GameData.instance.InitializeOptionValue();
    }
}
