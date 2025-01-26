using UnityEngine;
using UnityEngine.UI;

//StageUIを管理(ステージ数のUI/進行ボタン/町に戻るボタン)の管理
public class StageUIManager : MonoBehaviour
{
    public Text stageText;

    private void Start()
    {
    }

    public void UpdateUI(int currentStage)
    {
        Debug.Log($"UI:{currentStage}");
        stageText.text = string.Format("{0}", currentStage + 1);
    }
}
