using UnityEngine;

public class ExitButtonClicked : MonoBehaviour
{
    public void TitleExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
