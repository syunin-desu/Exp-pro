using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System;
using UnityEditor;

public class KeyInfo
{
    public bool isKeyBoard;
    public string keyFunction;
    public string keyName;
    public string effectivePath;
    public string id;

}

public class PlayerMenuOptionKeyConfigManager : MonoBehaviour
{
    public PlayerMenuOptionKeyConfigUIManager playerMenuOptionKeyConfigUIManager;
    public PlayerMenuOptionKeyConfigContentScrollUIManager playerMenuOptionKeyConfigContentScrollUIManager;
    public DeviceInputController deviceInputController;

    public void IntializeKeyConfigView(bool isKeyConfig)
    {
        // リスト作成
        List<KeyInfo> targetKeyInfo = this.GetTargetKeyInfo(isKeyConfig);
        this.playerMenuOptionKeyConfigContentScrollUIManager.SetupKeyListUI(targetKeyInfo, isKeyConfig);
        // Viewの活性化
        playerMenuOptionKeyConfigUIManager.UpdatePlayerMenuDisplay(true);

    }

    public void ReInitializeKeyConfigView(bool isKeyConfig)
    {
        // 現在のKeyをクリアする
        playerMenuOptionKeyConfigContentScrollUIManager.ClearAllKey();
        // リスト作成
        List<KeyInfo> targetKeyInfo = this.GetTargetKeyInfo(isKeyConfig);
        this.playerMenuOptionKeyConfigContentScrollUIManager.SetupKeyListUI(targetKeyInfo, isKeyConfig);
    }

    private List<KeyInfo> GetTargetKeyInfo(bool isKeyConfig)
    {
        var actions = deviceInputController.GetInputDeviceInputList();
        List<KeyInfo> keyInfoList = new List<KeyInfo>();
        foreach (var action in actions)
        {
            var bindingList = action.bindings;
            foreach (var binding in bindingList)
            {
                string SelectTarget = isKeyConfig ? "Keyboard" : "Gamepad";

                if (binding.effectivePath.Contains(SelectTarget))
                {
                    string currentKey = binding.effectivePath.Split("/")[1];
                    if (!isKeyConfig && this.IsGamePadCrossButton(binding.action))
                    {
                        continue;
                    }
                    KeyInfo keyInfo = new KeyInfo()
                    {
                        isKeyBoard = isKeyConfig,
                        keyFunction = binding.action,
                        keyName = currentKey,
                        effectivePath = binding.effectivePath,
                        id = binding.action,
                    };
                    keyInfoList.Add(keyInfo);
                }

            }
        }
        return keyInfoList;
    }

    public bool IsGamePadCrossButton(string keyAction)
    {
        return keyAction.Contains("Up")
            || keyAction.Contains("Down")
            || keyAction.Contains("Left")
            || keyAction.Contains("Right");
    }
}
