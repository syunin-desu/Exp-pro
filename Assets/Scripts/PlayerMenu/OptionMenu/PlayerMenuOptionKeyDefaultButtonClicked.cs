using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMenuOptionKeyDefaultButtonClicked : MonoBehaviour
{
    public DeviceInputController deviceInputController;
    public PlayerMenuOptionKeyConfigManager playerMenuOptionKeyConfigManager;
    public bool isKeyboard;

    public void OnclickedDefaultKeySettingButton()
    {
        deviceInputController.InitializeDefaultKey(isKeyboard);
        playerMenuOptionKeyConfigManager.ReInitializeKeyConfigView(isKeyboard);
    }
}
