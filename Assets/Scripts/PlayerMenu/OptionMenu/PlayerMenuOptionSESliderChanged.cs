using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuOptionSESliderChanged : MonoBehaviour
{
    public Slider slider;
    public void OnChangeSEValue()
    {
        GameData.instance.UpdateBGMValue(((int)this.slider.value));
    }
}
