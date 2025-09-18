using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuOptionBGMSliderChanged : MonoBehaviour
{
    public Slider slider;

    public void OnChangeBGMValue()
    {
        GameData.instance.UpdateBGMValue(((int)this.slider.value));
    }
}
