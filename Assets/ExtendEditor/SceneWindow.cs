using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneWindow : EditorWindow
{
    // FIXME: フォルダに登録されているシーンの数だけメニューを表示するように修正する
    [MenuItem("Launcher/Title", priority = 0)]
    public static void OpenGameScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Title.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/Town", priority = 0)]
    public static void OpenSampleScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Town.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/Quest", priority = 0)]
    public static void OpenTitleScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Quest.unity", OpenSceneMode.Single);
    }
}
