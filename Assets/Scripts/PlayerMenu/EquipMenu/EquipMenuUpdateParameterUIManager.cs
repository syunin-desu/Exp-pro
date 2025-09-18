using UnityEngine;
using UnityEngine.UI;

public class EquipMenuUpdateParameterUIManager : MonoBehaviour
{
    public Text UpdateArrow;
    public Text UpdateValue;
    public CONST.CHARCTOR.ParameterCategory parameterCategory;
    public Color textColor = Color.white;

    private void Start()
    {
        this.textColor.a = 0f;
        this.UpdateArrow.color = textColor;
        this.UpdateValue.color = textColor;
    }

    public void UpdateParameter(int CurrentValue, int UpdateValue)
    {
        if (CurrentValue != UpdateValue)
        {
            this.UpdateParameterEnable(true);
            this.UpdateValue.text = UpdateValue.ToString();
            this.UpdateValue.color = CurrentValue < UpdateValue ? Color.green : Color.red;
            this.UpdateArrow.color = CurrentValue < UpdateValue ? Color.green : Color.red;
        }
        else
        {
            this.UpdateParameterEnable(false);
        }

    }

    public void UpdateParameterEnable(bool isEnable)
    {
        this.textColor.a = isEnable ? 1f : 0f;
        this.UpdateArrow.color = textColor;
        this.UpdateValue.color = textColor;
    }
}
