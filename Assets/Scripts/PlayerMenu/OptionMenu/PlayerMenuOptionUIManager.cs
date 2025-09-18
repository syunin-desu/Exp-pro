using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuOptionUIManager : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider seSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateOptionMenuisActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }

    public void InitializeOptionMenuUI()
    {
        this.InitializeSlider();
        this.UpdateOptionMenuisActive(true);
    }

    private void InitializeSlider()
    {
        this.bgmSlider.value = GameData.instance.BGM_Volume;
        this.seSlider.value = GameData.instance.SE_Volume;
    }
}
