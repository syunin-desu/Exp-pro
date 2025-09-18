using TMPro;
using UnityEngine;

public class SaveMenuSaveContentUIManager : MonoBehaviour
{
    public RectTransform emptySaveContent;
    public RectTransform savedContent;

    public TextMeshProUGUI saveNoText;

    public TextMeshProUGUI currentFloorNo;
    public TextMeshProUGUI currentFloorName;
    public TextMeshProUGUI charName;
    public TextMeshProUGUI charClass;
    public TextMeshProUGUI time;

    public void UpdateIsEmptySave(bool isEmptySaveContent)
    {
        if (isEmptySaveContent)
        {
            this.emptySaveContent.gameObject.SetActive(true);
            this.savedContent.gameObject.SetActive(false);
        }
        else
        {

            this.emptySaveContent.gameObject.SetActive(false);
            this.savedContent.gameObject.SetActive(true);
        }
    }

    public void UpdateSaveSlotNo(int saveSlotNo)
    {
        this.saveNoText.text = saveSlotNo.ToString();
    }

    public void UpdateSaveContentUI(SaveDataSummary saveDataSummary)
    {
        currentFloorNo.text = saveDataSummary.currentFloorNo.ToString();
        currentFloorName.text = saveDataSummary.currentFloorName.ToString();
        charName.text = saveDataSummary.charName;
        charClass.text = saveDataSummary.charClass.ToString();
        time.text = saveDataSummary.savedTime;
    }
}
