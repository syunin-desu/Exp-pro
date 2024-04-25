using UnityEngine;
using UnityEngine.UI;

//StageUIを管理(ステージ数のUI/進行ボタン/町に戻るボタン)の管理
public class StageUIManager : MonoBehaviour
{
    public Text stageText;
    public GameObject nextButton;
    public GameObject toTownButton;

    private void Start()
    {
    }

    public void UpdateUI(int currentStage)
    {
        Debug.Log($"UI:{currentStage}");
        stageText.text = string.Format("{0}階", currentStage + 1);
    }

    public void HideButtons()
    {
        nextButton.SetActive(false);
        toTownButton.SetActive(false);
    }

    public void showButtons()
    {
        nextButton.SetActive(true);
        toTownButton.SetActive(true);
    }

    public void ShowClearText()
    {
        nextButton.SetActive(false);
        toTownButton.SetActive(true);
    }
}
