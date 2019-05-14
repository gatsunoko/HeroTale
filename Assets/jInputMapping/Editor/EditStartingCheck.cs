using UnityEngine;
using System.Collections;
using UnityEditor;

[InitializeOnLoad]
public class EditStartingChecker : AssetPostprocessor
{
    static jInputSettings SetScript;
    static bool StartingOnceCheck;

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromPath)
    {
        //このスクリプト自体がインポートされるときには指定のファイルが同時にインポートされても反応しない
        //SaveScene,SaveProject時にも指定のファイルが更新されていた場合は実行
        if (importedAssets.Length > 0)
        {
            for (int i = 0; i < importedAssets.Length; i++)
            {
                if (importedAssets[i] == "ProjectSettings/InputManager.asset"
                || importedAssets[i] == "ProjectSettings/InputManager(original).asset"
                || importedAssets[i] == "ProjectSettings/InputManager(original)Copy.asset")
                {
                    SyncValuesSOData();
                }
            }
        }
    }

    static EditStartingChecker()
    {
        EditorApplication.hierarchyWindowChanged += MyhierarchyChanged;
        EditorApplication.playmodeStateChanged += MyPlaymodeChanged;
    }

    static void MyPlaymodeChanged()
    { //Play開始時と停止時に実行

        //MyPlaymodeChanged()のほうがMyhierarchyChanged()より実効が早い
        //以下はisPlayingOrWillChangePlaymodeによりPlay停止時のみ
        if (!EditorApplication.isPlaying && !EditorApplication.isPaused && !EditorApplication.isPlayingOrWillChangePlaymode)
        {
            if (jInputSettings.MappingSetList != null && jInputSettings.MappingSetList.Count > 0 && jInputSettings.MappingSetList[0] != null)
            {
                if (SetScript = jInputSettings.MappingSetList[0].GetComponent<jInputSettings>())
                {
                    SetScript.jInputSOData.AxesSetApply();
                    SetScript.AxesReadFromSOData();
                }
            }
        }
        StartingOnceCheck = true;
    }

    static void MyhierarchyChanged()
    { //アセットのインポート時,シーン起動時,Inspector変更時,Play停止時に実行

        if (!EditorApplication.isPlaying && !EditorApplication.isPaused && !EditorApplication.isPlayingOrWillChangePlaymode)
        {
            if (StartingOnceCheck == false)
            {
                if (jInputSettings.MappingSetList != null && jInputSettings.MappingSetList.Count > 0 && jInputSettings.MappingSetList[0] != null)
                {
                    if (SetScript = jInputSettings.MappingSetList[0].GetComponent<jInputSettings>())
                    {
                        SetScript.DefaultArrayCopyReset(); //シーン起動時のみ実行
                        StartingOnceCheck = true;
                    }
                }
            }
        }
    }

    static void SyncValuesSOData()
    {
        if (jInputSettings.MappingSetList != null && jInputSettings.MappingSetList.Count > 0 && jInputSettings.MappingSetList[0] != null)
        {
            if (SetScript = jInputSettings.MappingSetList[0].GetComponent<jInputSettings>())
                SetScript.SODataRenew();
        }
    }

}
