using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMenuOptionKeyConfigSettingButtonClicked : MonoBehaviour
{
    public DeviceInputController deviceInputController;
    public PlayerMenuOptionKeyConfigContentUIManager playerMenuOptionKeyConfigContentUIManager;

    delegate void OnComlete(InputAction updatedAction);

    public string actionID;
    public bool isKeyboard;

    public void OnclickedKeySettingButton()
    {
        OnComlete onComlete = UpdateUI;
        deviceInputController.UpdateActionKey(isKeyboard, actionID, onComlete);
    }

    public void UpdateUI(InputAction updatedAction = null)
    {
        string SelectTarget = isKeyboard ? "Keyboard" : "Gamepad";
        string UpdatedKey = updatedAction.bindings.FirstOrDefault(b => b.effectivePath.Contains(SelectTarget)).effectivePath.Split("/")[1];
        var test = deviceInputController.GetInputDeviceInputList();
        Debug.Log(test);
        playerMenuOptionKeyConfigContentUIManager.UpdateKeyText(UpdatedKey);
    }
}
