using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ImportBaseClass))]//拡張するクラスを指定
public class ButtonExtend : Editor
{
    /// <summary>
    /// InspectorのGUIを更新
    /// </summary>
    public override void OnInspectorGUI()
    {
        //元のInspector部分を表示
        base.OnInspectorGUI();

        //targetを変換して対象を取得
        ImportBaseClass exampleScript = target as ImportBaseClass;


        if (GUILayout.Button("ImportItemMasterCSV"))
        {
            //SendMessageを使って実行
            exampleScript.SendMessage("OnClickedItemImport", null, SendMessageOptions.DontRequireReceiver);
        }


        if (GUILayout.Button("ImportEquipMasterCSV"))
        {
            //SendMessageを使って実行
            exampleScript.SendMessage("OnClickedEquipImport", null, SendMessageOptions.DontRequireReceiver);
        }

        if (GUILayout.Button("ImportBuffMasterCSV"))
        {
            //SendMessageを使って実行
            exampleScript.SendMessage("OnClickedBuffImport", null, SendMessageOptions.DontRequireReceiver);
        }

        if (GUILayout.Button("ImportAbilityMasterCSV"))
        {
            //SendMessageを使って実行
            exampleScript.SendMessage("OnClickedAbilityImport", null, SendMessageOptions.DontRequireReceiver);
        }

        if (GUILayout.Button("ImportAbilityMasterSettingWhimCSV"))
        {
            //SendMessageを使って実行
            exampleScript.SendMessage("OnClickedAbilityWhimSetting", null, SendMessageOptions.DontRequireReceiver);
        }

        if (GUILayout.Button("ImportClassMasterCSV"))
        {
            //SendMessageを使って実行
            exampleScript.SendMessage("OnClickedClassImport", null, SendMessageOptions.DontRequireReceiver);
        }


        if (GUILayout.Button("ImportEnemyMasterCSV"))
        {
            //SendMessageを使って実行
            exampleScript.SendMessage("OnClickedEnemyImport", null, SendMessageOptions.DontRequireReceiver);
        }

        if (GUILayout.Button("ImportPlayerMasterCSV"))
        {
            //SendMessageを使って実行
            exampleScript.SendMessage("OnClickedPlayerImport", null, SendMessageOptions.DontRequireReceiver);
        }

        if (GUILayout.Button("ImportFloorCardMasterCSV"))
        {
            //SendMessageを使って実行
            exampleScript.SendMessage("OnClickedFloorCardImport", null, SendMessageOptions.DontRequireReceiver);
        }

        if (GUILayout.Button("ImportWhimRateMasterCSV"))
        {
            //SendMessageを使って実行
            exampleScript.SendMessage("OnClickedWhimRateImport", null, SendMessageOptions.DontRequireReceiver);
        }

        if (GUILayout.Button("ImportAllMasterCSV"))
        {
            //SendMessageを使って実行
            exampleScript.SendMessage("OnClickedAllImport", null, SendMessageOptions.DontRequireReceiver);
        }

    }
}
