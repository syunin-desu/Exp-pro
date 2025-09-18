using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneWindow : EditorWindow
{
    // FIXME: フォルダに登録されているシーンの数だけメニューを表示するように修正する
    [MenuItem("Launcher/Title", priority = 0)]
    public static void OpenTitleScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Title.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/Town", priority = 0)]
    public static void OpenTownScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Town.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/Quest", priority = 0)]
    public static void OpenQuestScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Quest.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/Battle", priority = 0)]
    public static void OpenBattleScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Battle.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/MasterDataCreateor", priority = 0)]
    public static void OpenMasterDataCreateorScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/MasterDataCreateor.unity", OpenSceneMode.Single);
    }

    [MenuItem("Launcher/Conversation", priority = 0)]
    public static void OpenConversationScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Conversation.unity", OpenSceneMode.Single);
    }
}
