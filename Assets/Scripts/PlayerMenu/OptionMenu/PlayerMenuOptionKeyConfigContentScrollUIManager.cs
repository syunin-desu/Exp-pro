using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMenuOptionKeyConfigContentScrollUIManager : MonoBehaviour
{
    [SerializeField]
    RectTransform KeyContent;

    [SerializeField]
    RectTransform defaultContent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        KeyContent.gameObject.SetActive(false);
        defaultContent.gameObject.SetActive(false);
    }

    public void SetupKeyListUI(List<KeyInfo> targetInfo, bool isKeyboard)
    {
        foreach (KeyInfo info in targetInfo)
        {
            var keySlot = GameObject.Instantiate(KeyContent) as RectTransform;
            keySlot.SetParent(transform, false);

            keySlot.GetComponent<PlayerMenuOptionKeyConfigContentUIManager>().UpdateKeyContentTexts(
                info.keyFunction,
                info.keyName
                );
            keySlot.GetComponentInChildren<PlayerMenuOptionKeyConfigSettingButtonClicked>().actionID = info.id.ToString();
            keySlot.GetComponentInChildren<PlayerMenuOptionKeyConfigSettingButtonClicked>().isKeyboard = info.isKeyBoard;
            keySlot.gameObject.SetActive(true);
        }
        var defSlot = GameObject.Instantiate(defaultContent) as RectTransform;
        defSlot.SetParent(transform, false);
        defSlot.gameObject.GetComponentInChildren<PlayerMenuOptionKeyDefaultButtonClicked>().isKeyboard = isKeyboard;
        defSlot.gameObject.SetActive(true);

    }

    public void ClearAllKey()
    {
        var KeyButtonComponent = GameObject.FindGameObjectsWithTag("PlayerMenuKeyConfigSettingButton");
        foreach (var keyButton in KeyButtonComponent)
        {
            GameObject.Destroy(keyButton);
        }
    }
}
