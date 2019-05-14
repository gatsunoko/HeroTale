using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(jInputSettings))]
public class jInputSettingsInspector : Editor
{
    /*
		public override void OnInspectorGUI() {
			serializedObject.Update();
			SerializedProperty DIA = serializedObject.FindProperty ("DefaultInputArray");
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(DIA, true); //通常のInspectorの形と同じに描画
			if (EditorGUI.EndChangeCheck ()) {
					serializedObject.ApplyModifiedProperties ();
			}
			// ...
		}
	*/

    //Foldoutを標準のInspectorと同じ様に描画、public static にすればどのスクリプトからでも呼んで使える
    bool Foldout(bool foldout, GUIContent content, bool toggleOnLabelClick, GUIStyle style)
    {
        Rect position = GUILayoutUtility.GetRect(40f, 40f, 16f, 16f, style);
        // EditorGUI.kNumberW == 40f but is internal
        return EditorGUI.Foldout(position, foldout, content, toggleOnLabelClick, style);
    }

    bool Foldout(bool foldout, string content, bool toggleOnLabelClick, GUIStyle style)
    {
        return Foldout(foldout, new GUIContent(content), toggleOnLabelClick, style);
    }


    GUIStyle jInputGUIStyle = new GUIStyle();
    bool InspectorDisabled;
    bool DefaKeySetModeButtonCheck;
    bool SetModeStartLogCheck;
    bool ChangeDefaKey1pCheck;
    bool ChangeDefaKey2pCheck;
    bool ChangeDefaKey3pCheck;
    bool ChangeDefaKey4pCheck;
    MenuItemsCommonSetting MenuItemsCommonScript;
    string DefaKeyInconsistencyErrorText = "Default key name is inconsistency!\nThere is a need to Fix and Play again in order to apply.\n\n";
    readonly string[] DisplayedPlayerNum = { "1Player", "2Players", "3Players", "4Players" };
    readonly int[] OptionPlayerNum = { 1, 2, 3, 4 };
    readonly string[] DisplayedExcludeDevice = { "Not Exclude", "Exclude KeyBoard", "Exclude Joystick" };
    readonly int[] ExcludeDeviceNum = { 0, 1, 2 };

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        jInputSettings SetScript = target as jInputSettings;
        GUI.changed = false;

        jInputGUIStyle.padding.top = 4;
        jInputGUIStyle.padding.bottom = 4;
        jInputGUIStyle.padding.left = 10;
        jInputGUIStyle.padding.right = 2;
        jInputGUIStyle.normal.textColor = EditorStyles.label.normal.textColor;//背景Dark/Lightでの文字色
        //jInputGUIStyle.fontSize = 12; //fontを大きくする場合


        /*
		SerializedProperty MIH = serializedObject.FindProperty ("MenuItemHeadings");
		SerializedProperty DIA = serializedObject.FindProperty ("DefaultInputArray");

		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(MIH, true);
		if (GUI.changed) {
				Debug.Log ("ch1");
		}
		EditorGUI.EndChangeCheck ();

		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(DIA, true);
		if (GUI.changed) {
			Debug.Log ("ch2");
		}
		EditorGUI.EndChangeCheck ();
		*/

        if (!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused)
        {
            InspectorDisabled = false;
            if (SetScript.CompareThroughCheck)
                SetScript.CompareThroughCheck = false;
        }
        else
        {
            InspectorDisabled = true;
        }

        EditorGUI.BeginDisabledGroup(InspectorDisabled);
        //EditorGUI.BeginChangeCheck ();
        DrawDefaultInspector();
        //if (GUI.changed) {
        //	SetScript.HeadingGiveNumber ();
        //}
        //EditorGUI.EndChangeCheck ();
        EditorGUI.EndDisabledGroup();

        Undo.RecordObject(SetScript, "Inspector");
        if (Event.current.type == EventType.ValidateCommand)
        {
            if (Event.current.commandName == "UndoRedoPerformed")
            {
                SetScript.PlayerNumToSOData();
                SetScript.ArrayCopyToSOData();
                SetScript.ArrayCopyToSOData2();
                SetScript.ArrayCopyToSOData3();
                SetScript.ArrayCopyToSOData4();
                SetScript.UIOperationDropdownToList();
                SetScript.UINumListsAdvanceToSOData();
                SetScript.ExcludeDropdownToList();
                SetScript.AxesAdvanceToSOData();
                SetScript.jInputSOData.AxesSetApply();
            }
        }


        EditorGUI.BeginDisabledGroup(InspectorDisabled);
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Max Players in Same Place");
        EditorGUI.BeginChangeCheck();
        SetScript.PlayerNum = EditorGUILayout.IntPopup(" ", SetScript.PlayerNum, DisplayedPlayerNum, OptionPlayerNum);
        if (GUI.changed)
        {
            SetScript.PlayerNumToSOData();
            SetScript.ArrayCopyToSOData2();
            SetScript.ArrayCopyToSOData3();
            SetScript.ArrayCopyToSOData4();
        }
        EditorGUI.EndChangeCheck();
        EditorGUILayout.EndVertical();
        EditorGUI.EndDisabledGroup();


        EditorGUILayout.BeginVertical(GUI.skin.box);
        //SetScript.FoldoutBool = GUILayout.Toggle( SetScript.FoldoutBool, "Click to "+(SetScript.FoldoutBool ? "collapse":"expand"), "Foldout", GUILayout.ExpandWidth(false));
        //SetScript.FoldoutBool = EditorGUILayout.Foldout(SetScript.FoldoutBool, "Default Input Mapping");
        /*
		SetScript.FoldoutBool = GUILayout.Toggle( SetScript.FoldoutBool, "Default Input Mapping", "Foldout", GUILayout.ExpandWidth(false));
		if (SetScript.FoldoutBool) {
			for(int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
				SetScript.DefaultInputArray[i] = EditorGUILayout.TextField("    "+SetScript.MenuItemHeadings[i], SetScript.DefaultInputArray[i]);
			}
		}
		*/
        /*
		EditorGUILayout.GetControlRect (true, EditorGUIUtility.singleLineHeight, EditorStyles.foldout);
		Rect DefaKeyFoldRect = GUILayoutUtility.GetLastRect ();
		if (Event.current.type == EventType.MouseUp && DefaKeyFoldRect.Contains (Event.current.mousePosition)) {
				SetScript.DefaKeyFoldoutBool = !SetScript.DefaKeyFoldoutBool;
				//Event.current.Use(); //以降のクリック判定を抑制,重なっている下のものなどの判定をしない時に使う,これを入れると矢印部分にクリック判定が届かず色が変わらない
		}
		SetScript.DefaKeyFoldoutBool = EditorGUI.Foldout (DefaKeyFoldRect, SetScript.DefaKeyFoldoutBool, "Default Input Mapping");
		if (SetScript.DefaKeyFoldoutBool) {
				EditorGUI.indentLevel++;
				for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
						SetScript.DefaultInputArray [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray [i]);
				}
		*/


        EditorGUI.indentLevel++;
        EditorGUILayout.Space();
        SetScript.jInputSOData.DefaKeyFoldoutBool = Foldout(SetScript.jInputSOData.DefaKeyFoldoutBool, "Default Input Mapping", true, EditorStyles.foldout);
        EditorGUILayout.Space();
        if (SetScript.jInputSOData.DefaKeyFoldoutBool)
        {
            if (SetScript.PlayerNum == 1)
            {
                EditorGUI.indentLevel++;
                EditorGUI.BeginDisabledGroup(InspectorDisabled);
                EditorGUILayout.BeginVertical(GUI.skin.box);
                EditorGUI.BeginChangeCheck();
                try
                {
                    //for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
                    //		SetScript.DefaultInputArray [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray [i]);
                    //}
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(false));
                    GUILayout.Box("", GUIStyle.none, GUILayout.ExpandWidth(false), GUILayout.Height(12));
                    for (var i = 0; i < SetScript.MenuItemHeadings.Length; i++)
                    {
                        GUILayout.Label("E" + i + ": " + SetScript.MenuItemHeadings[i], jInputGUIStyle, GUILayout.ExpandWidth(false));
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
                    m_list.DoLayoutList();
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                    serializedObject.ApplyModifiedProperties();
                    EditorGUI.indentLevel++;
                    EditorGUI.indentLevel++;
                }
                catch
                {
                    SetScript.DefferentLengthArrayRenew();
                    //for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
                    //		SetScript.DefaultInputArray [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray [i]);
                    //}
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(false));
                    GUILayout.Box("", GUIStyle.none, GUILayout.ExpandWidth(false), GUILayout.Height(12));
                    for (var i = 0; i < SetScript.MenuItemHeadings.Length; i++)
                    {
                        GUILayout.Label("E" + i + ": " + SetScript.MenuItemHeadings[i], jInputGUIStyle, GUILayout.ExpandWidth(false));
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
                    m_list.DoLayoutList();
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                    serializedObject.ApplyModifiedProperties();
                    EditorGUI.indentLevel++;
                    EditorGUI.indentLevel++;
                }
                if (GUI.changed || ChangeDefaKey1pCheck)
                {
                    SetScript.ArrayCopyToSOData();
                }
                EditorGUI.EndChangeCheck();
                EditorGUI.EndDisabledGroup();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical(GUILayout.MinWidth(40), GUILayout.MaxWidth(180));
                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginVertical();
                if (!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused)
                { //Playしてない時
                    if (DefaKeySetModeButtonCheck != true && SetScript.DefaKeySetModeCheck && SetScript.PlayerSelectNum == 1)
                    {
                        //PlayStop時, このscript内のprivate boolであるDefaKeySetModeButtonCheckは自動でoffになりここにくる
                        SetScript.DefaKeySetModeCheck = false;
                        if (SetScript.jInputSOData.DefaKeyButtonOffCheck)
                        {
                            SetScript.jInputSOData.DefaKeyButtonOffCheck = false;
                            Debug.Log("Default Keys have been Set.");
                            SetScript.ApplyDefaInputMode();
                            EditorUtility.SetDirty(target);
                            // Repaint();
                        }
                        else
                        {
                            if (Event.current.commandName != "UndoRedoPerformed")
                                Debug.Log("Cancel Default Key Set Mode.");
                        }
                    }
                    if (SetScript.DefaKeySetModeCheck)
                    {
                        EditorGUI.BeginDisabledGroup(InspectorDisabled);
                        GUILayout.Button(" ", GUILayout.MaxWidth(220));
                        EditorGUI.EndDisabledGroup();
                    }
                    else
                    {
                        if (GUILayout.Button("Default Key Set Mode", GUILayout.MaxWidth(220)))
                        {
                            EditorApplication.isPlaying = true;
                            DefaKeySetModeButtonCheck = true;
                            SetScript.CompareThroughCheck = true;
                            SetScript.DefaKeySetModeCheck = true;
                            SetScript.PlayerSelectNum = 1;
                            GUI.FocusControl("");
                        }
                    }
                }
                else
                { //Play中
                    if (SetScript.DefaKeySetModeCheck != true || SetScript.PlayerSelectNum != 1)
                    {
                        EditorGUI.BeginDisabledGroup(InspectorDisabled);
                        GUILayout.Button(" ", GUILayout.MaxWidth(220));
                        EditorGUI.EndDisabledGroup();
                    }
                    else
                    {
                        if (SetModeStartLogCheck != true)
                        {
                            SetModeStartLogCheck = true;
                            Debug.Log("Default Key Set Mode Start.");
                        }
                        if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused)
                        { //Edit時からシーンPlayが始まるまでの間
                            GUILayout.Button("Default Key Set Mode [ON]", GUILayout.MaxWidth(220));
                        }
                        else
                        {
                            if (SetScript.CurrentryRestore)
                            {
                                if (GUILayout.Button("Default Key Set Mode [ON]", GUILayout.MaxWidth(220)))
                                {
                                    EditorApplication.isPlaying = false;
                                }
                            }
                            else
                            {
                                if (GUILayout.Button("* Default Key Set Mode [ON] *", GUILayout.MaxWidth(220)))
                                { //Playボタンを押してEditに戻った場合はデフォキーへの変更は適用されない
                                    EditorApplication.isPlaying = false;
                                    SetScript.jInputSOData.DefaKeyButtonOffCheck = true;
                                    SetScript.SaveDefaInputMode(); //デフォキー変更を適用させる
                                }
                            }
                        }
                    }
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();
                EditorGUI.indentLevel--;
            }
            else
            { //SetScript.PlayerNum!=1
                EditorGUI.indentLevel++;
                if (SetScript.PlayerNum >= 2)
                {
                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    SetScript.jInputSOData.DefaKey1pFoldoutBool = Foldout(SetScript.jInputSOData.DefaKey1pFoldoutBool, "Player1 Default Input", true, EditorStyles.foldout);
                    if (SetScript.jInputSOData.DefaKey1pFoldoutBool)
                    {
                        EditorGUI.BeginDisabledGroup(InspectorDisabled);
                        EditorGUI.BeginChangeCheck();
                        try
                        {
                            //for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
                            //		SetScript.DefaultInputArray [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray [i]);
                            //}
                            EditorGUI.indentLevel--;
                            EditorGUI.indentLevel--;
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(false));
                            GUILayout.Box("", GUIStyle.none, GUILayout.ExpandWidth(false), GUILayout.Height(12));
                            for (var i = 0; i < SetScript.MenuItemHeadings.Length; i++)
                            {
                                GUILayout.Label("E" + i + ": " + SetScript.MenuItemHeadings[i], jInputGUIStyle, GUILayout.ExpandWidth(false));
                            }
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
                            m_list.DoLayoutList();
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                            serializedObject.ApplyModifiedProperties();
                            EditorGUI.indentLevel++;
                            EditorGUI.indentLevel++;
                        }
                        catch
                        {
                            SetScript.DefferentLengthArrayRenew();
                            //for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
                            //		SetScript.DefaultInputArray [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray [i]);
                            //}
                            EditorGUI.indentLevel--;
                            EditorGUI.indentLevel--;
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(false));
                            GUILayout.Box("", GUIStyle.none, GUILayout.ExpandWidth(false), GUILayout.Height(12));
                            for (var i = 0; i < SetScript.MenuItemHeadings.Length; i++)
                            {
                                GUILayout.Label("E" + i + ": " + SetScript.MenuItemHeadings[i], jInputGUIStyle, GUILayout.ExpandWidth(false));
                            }
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
                            m_list.DoLayoutList();
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                            serializedObject.ApplyModifiedProperties();
                            EditorGUI.indentLevel++;
                            EditorGUI.indentLevel++;
                        }
                        if (GUI.changed || ChangeDefaKey1pCheck)
                        {
                            SetScript.ArrayCopyToSOData();
                        }
                        EditorGUI.EndChangeCheck();
                        EditorGUI.EndDisabledGroup();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.BeginVertical(GUILayout.MinWidth(40), GUILayout.MaxWidth(180));
                        EditorGUILayout.Space();
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.BeginVertical();
                        if (!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused)
                        { //Playしてない時
                            if (DefaKeySetModeButtonCheck != true && SetScript.DefaKeySetModeCheck && SetScript.PlayerSelectNum == 1)
                            {
                                //PlayStop時, このscript内のprivate boolであるDefaKeySetModeButtonCheckは自動でoffになりここにくる
                                SetScript.DefaKeySetModeCheck = false;
                                if (SetScript.jInputSOData.DefaKeyButtonOffCheck)
                                {
                                    SetScript.jInputSOData.DefaKeyButtonOffCheck = false;
                                    Debug.Log("1P Default Keys have been Set.");
                                    SetScript.ApplyDefaInputMode();
                                    EditorUtility.SetDirty(target);
                                    // Repaint();
                                }
                                else
                                {
                                    if (Event.current.commandName != "UndoRedoPerformed")
                                        Debug.Log("Cancel Default Key Set Mode.");
                                }
                            }
                            if (SetScript.DefaKeySetModeCheck)
                            {
                                EditorGUI.BeginDisabledGroup(InspectorDisabled);
                                GUILayout.Button(" ", GUILayout.MaxWidth(220));
                                EditorGUI.EndDisabledGroup();
                            }
                            else
                            {
                                if (GUILayout.Button("1P Default Key Set Mode", GUILayout.MaxWidth(220)))
                                {
                                    EditorApplication.isPlaying = true;
                                    DefaKeySetModeButtonCheck = true;
                                    SetScript.CompareThroughCheck = true;
                                    SetScript.DefaKeySetModeCheck = true;
                                    SetScript.PlayerSelectNum = 1;
                                    GUI.FocusControl("");
                                }
                            }
                        }
                        else
                        { //Play中
                            if (SetScript.DefaKeySetModeCheck != true || SetScript.PlayerSelectNum != 1)
                            {
                                EditorGUI.BeginDisabledGroup(InspectorDisabled);
                                GUILayout.Button(" ", GUILayout.MaxWidth(220));
                                EditorGUI.EndDisabledGroup();
                            }
                            else
                            {
                                if (SetModeStartLogCheck != true)
                                {
                                    SetModeStartLogCheck = true;
                                    Debug.Log("1P Default Key Set Mode Start.");
                                }
                                if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused)
                                { //Edit時からシーンPlayが始まるまでの間
                                    GUILayout.Button("1P Default Key Set Mode [ON]", GUILayout.MaxWidth(220));
                                }
                                else
                                {
                                    if (SetScript.CurrentryRestore)
                                    {
                                        if (GUILayout.Button("1P Default Key Set Mode [ON]", GUILayout.MaxWidth(220)))
                                        {
                                            EditorApplication.isPlaying = false;
                                        }
                                    }
                                    else
                                    {
                                        if (GUILayout.Button("* 1P Default Key Set Mode [ON] *", GUILayout.MaxWidth(220)))
                                        { //Playボタンを押してEditに戻った場合はデフォキーへの変更は適用されない
                                            EditorApplication.isPlaying = false;
                                            SetScript.jInputSOData.DefaKeyButtonOffCheck = true;
                                            SetScript.SaveDefaInputMode(); //デフォキー変更を適用させる
                                        }
                                    }
                                }
                            }
                        }
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                    }
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    SetScript.jInputSOData.DefaKey2pFoldoutBool = Foldout(SetScript.jInputSOData.DefaKey2pFoldoutBool, "Player2 Default Input", true, EditorStyles.foldout);
                    if (SetScript.jInputSOData.DefaKey2pFoldoutBool)
                    {
                        EditorGUI.BeginDisabledGroup(InspectorDisabled);
                        EditorGUI.BeginChangeCheck();
                        try
                        {
                            //for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
                            //		SetScript.DefaultInputArray2p [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray2p [i]);
                            //}
                            EditorGUI.indentLevel--;
                            EditorGUI.indentLevel--;
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(false));
                            GUILayout.Box("", GUIStyle.none, GUILayout.ExpandWidth(false), GUILayout.Height(12));
                            for (var i = 0; i < SetScript.MenuItemHeadings.Length; i++)
                            {
                                GUILayout.Label("E" + i + ": " + SetScript.MenuItemHeadings[i], jInputGUIStyle, GUILayout.ExpandWidth(false));
                            }
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
                            m_list2p.DoLayoutList();
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                            serializedObject.ApplyModifiedProperties();
                            EditorGUI.indentLevel++;
                            EditorGUI.indentLevel++;
                        }
                        catch
                        {
                            SetScript.DefferentLengthArrayRenew2();
                            //for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
                            //		SetScript.DefaultInputArray2p [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray2p [i]);
                            //}
                            EditorGUI.indentLevel--;
                            EditorGUI.indentLevel--;
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(false));
                            GUILayout.Box("", GUIStyle.none, GUILayout.ExpandWidth(false), GUILayout.Height(12));
                            for (var i = 0; i < SetScript.MenuItemHeadings.Length; i++)
                            {
                                GUILayout.Label("E" + i + ": " + SetScript.MenuItemHeadings[i], jInputGUIStyle, GUILayout.ExpandWidth(false));
                            }
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
                            m_list2p.DoLayoutList();
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                            serializedObject.ApplyModifiedProperties();
                            EditorGUI.indentLevel++;
                            EditorGUI.indentLevel++;
                        }
                        if (GUI.changed || ChangeDefaKey2pCheck)
                        {
                            SetScript.ArrayCopyToSOData2();
                        }
                        EditorGUI.EndChangeCheck();
                        EditorGUI.EndDisabledGroup();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.BeginVertical(GUILayout.MinWidth(40), GUILayout.MaxWidth(180));
                        EditorGUILayout.Space();
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.BeginVertical();
                        if (!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused)
                        { //Playしてない時
                            if (DefaKeySetModeButtonCheck != true && SetScript.DefaKeySetModeCheck && SetScript.PlayerSelectNum == 2)
                            {
                                //PlayStop時, このscript内のprivate boolであるDefaKeySetModeButtonCheckは自動でoffになりここにくる
                                SetScript.DefaKeySetModeCheck = false;
                                if (SetScript.jInputSOData.DefaKeyButtonOffCheck)
                                {
                                    SetScript.jInputSOData.DefaKeyButtonOffCheck = false;
                                    Debug.Log("2P Default Keys have been Set.");
                                    SetScript.ApplyDefaInputMode2p();
                                    EditorUtility.SetDirty(target);
                                    // Repaint();
                                }
                                else
                                {
                                    if (Event.current.commandName != "UndoRedoPerformed")
                                        Debug.Log("Cancel Default Key Set Mode.");
                                }
                            }
                            if (SetScript.DefaKeySetModeCheck)
                            {
                                EditorGUI.BeginDisabledGroup(InspectorDisabled);
                                GUILayout.Button(" ", GUILayout.MaxWidth(220));
                                EditorGUI.EndDisabledGroup();
                            }
                            else
                            {
                                if (GUILayout.Button("2P Default Key Set Mode", GUILayout.MaxWidth(220)))
                                {
                                    EditorApplication.isPlaying = true;
                                    DefaKeySetModeButtonCheck = true;
                                    SetScript.CompareThroughCheck = true;
                                    SetScript.DefaKeySetModeCheck = true;
                                    SetScript.PlayerSelectNum = 2;
                                    GUI.FocusControl("");
                                }
                            }
                        }
                        else
                        { //Play中
                            if (SetScript.DefaKeySetModeCheck != true || SetScript.PlayerSelectNum != 2)
                            {
                                EditorGUI.BeginDisabledGroup(InspectorDisabled);
                                GUILayout.Button(" ", GUILayout.MaxWidth(220));
                                EditorGUI.EndDisabledGroup();
                            }
                            else
                            {
                                if (SetModeStartLogCheck != true)
                                {
                                    SetModeStartLogCheck = true;
                                    Debug.Log("2P Default Key Set Mode Start.");
                                }
                                if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused)
                                { //Edit時からシーンPlayが始まるまでの間
                                    GUILayout.Button("2P Default Key Set Mode [ON]", GUILayout.MaxWidth(220));
                                }
                                else
                                {
                                    if (SetScript.CurrentryRestore)
                                    {
                                        if (GUILayout.Button("2P Default Key Set Mode [ON]", GUILayout.MaxWidth(220)))
                                        {
                                            EditorApplication.isPlaying = false;
                                        }
                                    }
                                    else
                                    {
                                        if (GUILayout.Button("* 2P Default Key Set Mode [ON] *", GUILayout.MaxWidth(220)))
                                        { //Playボタンを押してEditに戻った場合はデフォキーへの変更は適用されない
                                            EditorApplication.isPlaying = false;
                                            SetScript.jInputSOData.DefaKeyButtonOffCheck = true;
                                            SetScript.SaveDefaInputMode(); //デフォキー変更を適用させる
                                        }
                                    }
                                }
                            }
                        }
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                    }
                    EditorGUILayout.EndVertical();
                }

                if (SetScript.PlayerNum >= 3)
                {
                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    SetScript.jInputSOData.DefaKey3pFoldoutBool = Foldout(SetScript.jInputSOData.DefaKey3pFoldoutBool, "Player3 Default Input", true, EditorStyles.foldout);
                    if (SetScript.jInputSOData.DefaKey3pFoldoutBool)
                    {
                        EditorGUI.BeginDisabledGroup(InspectorDisabled);
                        EditorGUI.BeginChangeCheck();
                        try
                        {
                            //for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
                            //		SetScript.DefaultInputArray3p [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray3p [i]);
                            //}
                            EditorGUI.indentLevel--;
                            EditorGUI.indentLevel--;
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(false));
                            GUILayout.Box("", GUIStyle.none, GUILayout.ExpandWidth(false), GUILayout.Height(12));
                            for (var i = 0; i < SetScript.MenuItemHeadings.Length; i++)
                            {
                                GUILayout.Label("E" + i + ": " + SetScript.MenuItemHeadings[i], jInputGUIStyle, GUILayout.ExpandWidth(false));
                            }
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
                            m_list3p.DoLayoutList();
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                            serializedObject.ApplyModifiedProperties();
                            EditorGUI.indentLevel++;
                            EditorGUI.indentLevel++;
                        }
                        catch
                        {
                            SetScript.DefferentLengthArrayRenew3();
                            //for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
                            //		SetScript.DefaultInputArray3p [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray3p [i]);
                            //}
                            EditorGUI.indentLevel--;
                            EditorGUI.indentLevel--;
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(false));
                            GUILayout.Box("", GUIStyle.none, GUILayout.ExpandWidth(false), GUILayout.Height(12));
                            for (var i = 0; i < SetScript.MenuItemHeadings.Length; i++)
                            {
                                GUILayout.Label("E" + i + ": " + SetScript.MenuItemHeadings[i], jInputGUIStyle, GUILayout.ExpandWidth(false));
                            }
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
                            m_list3p.DoLayoutList();
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                            serializedObject.ApplyModifiedProperties();
                            EditorGUI.indentLevel++;
                            EditorGUI.indentLevel++;
                        }
                        if (GUI.changed || ChangeDefaKey3pCheck)
                        {
                            SetScript.ArrayCopyToSOData3();
                        }
                        EditorGUI.EndChangeCheck();
                        EditorGUI.EndDisabledGroup();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.BeginVertical(GUILayout.MinWidth(40), GUILayout.MaxWidth(180));
                        EditorGUILayout.Space();
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.BeginVertical();
                        if (!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused)
                        { //Playしてない時
                            if (DefaKeySetModeButtonCheck != true && SetScript.DefaKeySetModeCheck && SetScript.PlayerSelectNum == 3)
                            {
                                //PlayStop時, このscript内のprivate boolであるDefaKeySetModeButtonCheckは自動でoffになりここにくる
                                SetScript.DefaKeySetModeCheck = false;
                                if (SetScript.jInputSOData.DefaKeyButtonOffCheck)
                                {
                                    SetScript.jInputSOData.DefaKeyButtonOffCheck = false;
                                    Debug.Log("3P Default Keys have been Set.");
                                    SetScript.ApplyDefaInputMode3p();
                                    EditorUtility.SetDirty(target);
                                    // Repaint();
                                }
                                else
                                {
                                    if (Event.current.commandName != "UndoRedoPerformed")
                                        Debug.Log("Cancel Default Key Set Mode.");
                                }
                            }
                            if (SetScript.DefaKeySetModeCheck)
                            {
                                EditorGUI.BeginDisabledGroup(InspectorDisabled);
                                GUILayout.Button(" ", GUILayout.MaxWidth(220));
                                EditorGUI.EndDisabledGroup();
                            }
                            else
                            {
                                if (GUILayout.Button("3P Default Key Set Mode", GUILayout.MaxWidth(220)))
                                {
                                    EditorApplication.isPlaying = true;
                                    DefaKeySetModeButtonCheck = true;
                                    SetScript.CompareThroughCheck = true;
                                    SetScript.DefaKeySetModeCheck = true;
                                    SetScript.PlayerSelectNum = 3;
                                    GUI.FocusControl("");
                                }
                            }
                        }
                        else
                        { //Play中
                            if (SetScript.DefaKeySetModeCheck != true || SetScript.PlayerSelectNum != 3)
                            {
                                EditorGUI.BeginDisabledGroup(InspectorDisabled);
                                GUILayout.Button(" ", GUILayout.MaxWidth(220));
                                EditorGUI.EndDisabledGroup();
                            }
                            else
                            {
                                if (SetModeStartLogCheck != true)
                                {
                                    SetModeStartLogCheck = true;
                                    Debug.Log("3P Default Key Set Mode Start.");
                                }
                                if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused)
                                { //Edit時からシーンPlayが始まるまでの間
                                    GUILayout.Button("3P Default Key Set Mode [ON]", GUILayout.MaxWidth(220));
                                }
                                else
                                {
                                    if (SetScript.CurrentryRestore)
                                    {
                                        if (GUILayout.Button("3P Default Key Set Mode [ON]", GUILayout.MaxWidth(220)))
                                        {
                                            EditorApplication.isPlaying = false;
                                        }
                                    }
                                    else
                                    {
                                        if (GUILayout.Button("* 3P Default Key Set Mode [ON] *", GUILayout.MaxWidth(220)))
                                        { //Playボタンを押してEditに戻った場合はデフォキーへの変更は適用されない
                                            EditorApplication.isPlaying = false;
                                            SetScript.jInputSOData.DefaKeyButtonOffCheck = true;
                                            SetScript.SaveDefaInputMode(); //デフォキー変更を適用させる
                                        }
                                    }
                                }
                            }
                        }
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                    }
                    EditorGUILayout.EndVertical();
                }

                if (SetScript.PlayerNum >= 4)
                {
                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    SetScript.jInputSOData.DefaKey4pFoldoutBool = Foldout(SetScript.jInputSOData.DefaKey4pFoldoutBool, "Player4 Default Input", true, EditorStyles.foldout);
                    if (SetScript.jInputSOData.DefaKey4pFoldoutBool)
                    {
                        EditorGUI.BeginDisabledGroup(InspectorDisabled);
                        EditorGUI.BeginChangeCheck();
                        try
                        {
                            //for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
                            //		SetScript.DefaultInputArray4p [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray4p [i]);
                            //}
                            EditorGUI.indentLevel--;
                            EditorGUI.indentLevel--;
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(false));
                            GUILayout.Box("", GUIStyle.none, GUILayout.ExpandWidth(false), GUILayout.Height(12));
                            for (var i = 0; i < SetScript.MenuItemHeadings.Length; i++)
                            {
                                GUILayout.Label("E" + i + ": " + SetScript.MenuItemHeadings[i], jInputGUIStyle, GUILayout.ExpandWidth(false));
                            }
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
                            m_list4p.DoLayoutList();
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                            serializedObject.ApplyModifiedProperties();
                            EditorGUI.indentLevel++;
                            EditorGUI.indentLevel++;
                        }
                        catch
                        {
                            SetScript.DefferentLengthArrayRenew4();
                            //for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
                            //		SetScript.DefaultInputArray4p [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray4p [i]);
                            //}
                            EditorGUI.indentLevel--;
                            EditorGUI.indentLevel--;
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandWidth(false));
                            GUILayout.Box("", GUIStyle.none, GUILayout.ExpandWidth(false), GUILayout.Height(12));
                            for (var i = 0; i < SetScript.MenuItemHeadings.Length; i++)
                            {
                                GUILayout.Label("E" + i + ": " + SetScript.MenuItemHeadings[i], jInputGUIStyle, GUILayout.ExpandWidth(false));
                            }
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
                            m_list4p.DoLayoutList();
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                            serializedObject.ApplyModifiedProperties();
                            EditorGUI.indentLevel++;
                            EditorGUI.indentLevel++;
                        }
                        if (GUI.changed || ChangeDefaKey4pCheck)
                        {
                            SetScript.ArrayCopyToSOData4();
                        }
                        EditorGUI.EndChangeCheck();
                        EditorGUI.EndDisabledGroup();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.BeginVertical(GUILayout.MinWidth(40), GUILayout.MaxWidth(180));
                        EditorGUILayout.Space();
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.BeginVertical();
                        if (!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused)
                        { //Playしてない時
                            if (DefaKeySetModeButtonCheck != true && SetScript.DefaKeySetModeCheck && SetScript.PlayerSelectNum == 4)
                            {
                                //PlayStop時, このscript内のprivate boolであるDefaKeySetModeButtonCheckは自動でoffになりここにくる
                                SetScript.DefaKeySetModeCheck = false;
                                if (SetScript.jInputSOData.DefaKeyButtonOffCheck)
                                {
                                    SetScript.jInputSOData.DefaKeyButtonOffCheck = false;
                                    Debug.Log("4P Default Keys have been Set.");
                                    SetScript.ApplyDefaInputMode4p();
                                    EditorUtility.SetDirty(target);
                                    // Repaint();
                                }
                                else
                                {
                                    if (Event.current.commandName != "UndoRedoPerformed")
                                        Debug.Log("Cancel Default Key Set Mode.");
                                }
                            }
                            if (SetScript.DefaKeySetModeCheck)
                            {
                                EditorGUI.BeginDisabledGroup(InspectorDisabled);
                                GUILayout.Button(" ", GUILayout.MaxWidth(220));
                                EditorGUI.EndDisabledGroup();
                            }
                            else
                            {
                                if (GUILayout.Button("4P Default Key Set Mode", GUILayout.MaxWidth(220)))
                                {
                                    EditorApplication.isPlaying = true;
                                    DefaKeySetModeButtonCheck = true;
                                    SetScript.CompareThroughCheck = true;
                                    SetScript.DefaKeySetModeCheck = true;
                                    SetScript.PlayerSelectNum = 4;
                                    GUI.FocusControl("");
                                }
                            }
                        }
                        else
                        { //Play中
                            if (SetScript.DefaKeySetModeCheck != true || SetScript.PlayerSelectNum != 4)
                            {
                                EditorGUI.BeginDisabledGroup(InspectorDisabled);
                                GUILayout.Button(" ", GUILayout.MaxWidth(220));
                                EditorGUI.EndDisabledGroup();
                            }
                            else
                            {
                                if (SetModeStartLogCheck != true)
                                {
                                    SetModeStartLogCheck = true;
                                    Debug.Log("4P Default Key Set Mode Start.");
                                }
                                if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused)
                                { //Edit時からシーンPlayが始まるまでの間
                                    GUILayout.Button("4P Default Key Set Mode [ON]", GUILayout.MaxWidth(220));
                                }
                                else
                                {
                                    if (SetScript.CurrentryRestore)
                                    {
                                        if (GUILayout.Button("4P Default Key Set Mode [ON]", GUILayout.MaxWidth(220)))
                                        {
                                            EditorApplication.isPlaying = false;
                                        }
                                    }
                                    else
                                    {
                                        if (GUILayout.Button("* 4P Default Key Set Mode [ON] *", GUILayout.MaxWidth(220)))
                                        { //Playボタンを押してEditに戻った場合はデフォキーへの変更は適用されない
                                            EditorApplication.isPlaying = false;
                                            SetScript.jInputSOData.DefaKeyButtonOffCheck = true;
                                            SetScript.SaveDefaInputMode(); //デフォキー変更を適用させる
                                        }
                                    }
                                }
                            }
                        }
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space();
                        EditorGUILayout.Space();
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUI.indentLevel--;
            }
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.Space();
        if (SetScript.jInputSOData.DefaultInputNameInconsistencyCheck)
        {
            EditorGUILayout.HelpBox(DefaKeyInconsistencyErrorText + SetScript.jInputSOData.DefaKeyInconsistencyListText, MessageType.Error, true);
        }
        EditorGUILayout.EndVertical();


        EditorGUI.BeginDisabledGroup(InspectorDisabled);
        if (SetScript.jInputNonUGUICheck)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            SetScript.PrecludeSameMappingCheck = EditorGUILayout.Toggle("Preclude Same Mapping", SetScript.PrecludeSameMappingCheck);
            EditorGUILayout.EndVertical();
        }
        else
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;
            SetScript.jInputSOData.SameMappingFoldoutBool = Foldout(SetScript.jInputSOData.SameMappingFoldoutBool, "Deal with Same Key", true, EditorStyles.foldout);
            if (SetScript.jInputSOData.SameMappingFoldoutBool)
            {
                SetScript.PrecludeSameMappingCheck = EditorGUILayout.Toggle("Preclude Same Mapping", SetScript.PrecludeSameMappingCheck);
                EditorGUILayout.BeginVertical(GUILayout.Height(20));
                //微妙な縦スペース調整
                EditorGUILayout.BeginVertical();
                EditorGUILayout.EndVertical();
                EditorGUI.BeginDisabledGroup(SetScript.PrecludeSameMappingCheck);
                if (SetScript.PrecludeSameMappingCheck)
                {
                    EditorGUILayout.ColorField("Same KeyName Color", Color.clear);
                }
                else
                {
                    SetScript.SameKeyOutlineColor = EditorGUILayout.ColorField("Same KeyName Color", SetScript.SameKeyOutlineColor);
                }
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.EndVertical();
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }


        if (SetScript.UnusableKeySize < 0)
            SetScript.UnusableKeySize = 0;
        else if (SetScript.UnusableKeySize > 300)
            SetScript.UnusableKeySize = 300;
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;
        SetScript.jInputSOData.UnusableKeyFoldoutBool = Foldout(SetScript.jInputSOData.UnusableKeyFoldoutBool, "Unusable Mapping", true, EditorStyles.foldout);
        if (SetScript.jInputSOData.UnusableKeyFoldoutBool)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(GUILayout.MinWidth(100));
            EditorGUILayout.LabelField("Size", GUILayout.MinWidth(100));
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            EditorGUI.BeginChangeCheck();
            SetScript.UnusableKeySize = EditorGUILayout.IntField("", SetScript.UnusableKeySize, GUILayout.MinWidth(100));
            if (GUI.changed)
            {
                SetScript.UnusableIntsToList();
            }
            EditorGUI.EndChangeCheck();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            for (int i = 0; i < SetScript.UnusableKeyInts.Length; i++)
            {
                if (i < SetScript.UnusableKeySize)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    EditorGUILayout.LabelField("Unusable" + i, GUILayout.Width(94));
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    EditorGUI.BeginChangeCheck();
                    string tempKeyName = jInput.AllInputNames[SetScript.UnusableKeyInts[i]];
                    if (tempKeyName == " ")
                        tempKeyName = string.Empty;
                    tempKeyName = EditorGUILayout.TextField("", tempKeyName, GUILayout.MinWidth(80), GUILayout.ExpandWidth(true));
                    if (tempKeyName.Length > 0)
                    {
                        /* 頭文字を大文字にする場合(jInput.AllInputNamesからstringを写すようにしたので今は必要ない)
                        if (tempKeyName.Length == 1)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(tempKeyName, "[a-z]"))
                                tempKeyName = tempKeyName.ToUpper();
                        }
                        else
                        {
                            tempKeyName = System.Char.ToUpper(tempKeyName[0]) + tempKeyName.Substring(1);
                        }
                        */
                        //入力したstringが配列の何番目にあるか,大文字小文字の違いを無視して探す(なければ-1)
                        int tempIndexOf = System.Array.FindIndex(jInput.AllInputNames, s => s.Equals(tempKeyName, System.StringComparison.OrdinalIgnoreCase));
                        if (tempIndexOf < 0)
                        { //存在しないインプット名の場合
                            SetScript.UnusableKeyInts[i] = 0;
                        }
                        else
                        {
                            SetScript.UnusableKeyInts[i] = tempIndexOf;
                        }
                    }
                    else
                    { //空欄
                        SetScript.UnusableKeyInts[i] = 0;
                    }
                    if (GUI.changed)
                    {
                        SetScript.UnusableIntsToList();
                    }
                    EditorGUI.EndChangeCheck();
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginVertical();
                    EditorGUI.BeginChangeCheck();
                    SetScript.UnusableKeyInts[i] = EditorGUILayout.Popup("", SetScript.UnusableKeyInts[i], jInput.AllInputNames, GUILayout.MinWidth(48), GUILayout.MaxWidth(150));
                    if (GUI.changed)
                    {
                        SetScript.UnusableIntsToList();
                    }
                    EditorGUI.EndChangeCheck();
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    SetScript.UnusableKeyInts[i] = 0;
                }
            }
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();


        if (SetScript.PlayerNum == 1)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            SetScript.ExcludeDeviceAry[1] = EditorGUILayout.IntPopup("Exclude Device", SetScript.ExcludeDeviceAry[1], DisplayedExcludeDevice, ExcludeDeviceNum);
            EditorGUILayout.EndVertical();
        }
        else
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;
            SetScript.jInputSOData.ExcludeDeviceFoldoutBool = Foldout(SetScript.jInputSOData.ExcludeDeviceFoldoutBool, "Exclude Device", true, EditorStyles.foldout);
            if (SetScript.jInputSOData.ExcludeDeviceFoldoutBool)
            {

                if (SetScript.PlayerNum >= 2)
                {
                    SetScript.ExcludeDeviceAry[1] = EditorGUILayout.IntPopup("From 1P", SetScript.ExcludeDeviceAry[1], DisplayedExcludeDevice, ExcludeDeviceNum);
                    SetScript.ExcludeDeviceAry[2] = EditorGUILayout.IntPopup("From 2P", SetScript.ExcludeDeviceAry[2], DisplayedExcludeDevice, ExcludeDeviceNum);
                }
                if (SetScript.PlayerNum >= 3)
                    SetScript.ExcludeDeviceAry[3] = EditorGUILayout.IntPopup("From 3P", SetScript.ExcludeDeviceAry[3], DisplayedExcludeDevice, ExcludeDeviceNum);
                if (SetScript.PlayerNum >= 4)
                    SetScript.ExcludeDeviceAry[4] = EditorGUILayout.IntPopup("From 4P", SetScript.ExcludeDeviceAry[4], DisplayedExcludeDevice, ExcludeDeviceNum);
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }


        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;
        SetScript.jInputSOData.UGUINumListsFoldoutBool = Foldout(SetScript.jInputSOData.UGUINumListsFoldoutBool, "UI Operation Settings", true, EditorStyles.foldout);
        if (SetScript.jInputSOData.UGUINumListsFoldoutBool)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginVertical();
            SetScript.UGUIUpFlags = EditorGUILayout.MaskField("Up Move", SetScript.UGUIUpFlags, SetScript.MenuItemHeadingsCopy, GUILayout.Height(19));
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            SetScript.UGUIDownFlags = EditorGUILayout.MaskField("Down Move", SetScript.UGUIDownFlags, SetScript.MenuItemHeadingsCopy, GUILayout.Height(19));
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            SetScript.UGUIRightFlags = EditorGUILayout.MaskField("Right Move", SetScript.UGUIRightFlags, SetScript.MenuItemHeadingsCopy, GUILayout.Height(19));
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            SetScript.UGUILeftFlags = EditorGUILayout.MaskField("Left Move", SetScript.UGUILeftFlags, SetScript.MenuItemHeadingsCopy, GUILayout.Height(24));
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            SetScript.UGUISubmitFlags = EditorGUILayout.MaskField("UGUI Submit", SetScript.UGUISubmitFlags, SetScript.MenuItemHeadingsCopy, GUILayout.Height(19));
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            SetScript.UGUICancelFlags = EditorGUILayout.MaskField("UGUI Cancel", SetScript.UGUICancelFlags, SetScript.MenuItemHeadingsCopy, GUILayout.Height(19));
            EditorGUILayout.EndVertical();
            if (GUI.changed)
            {
                SetScript.UIOperationDropdownToList();
                SetScript.UINumListsAdvanceToSOData();
            }
            EditorGUILayout.Space();
            SetScript.ExcludeDecisionFlags = EditorGUILayout.MaskField("ExcludeDecisionFnc", SetScript.ExcludeDecisionFlags, SetScript.MenuItemHeadingsCopy, GUILayout.Height(24));
            if (GUI.changed)
                SetScript.ExcludeDropdownToList();
            EditorGUI.EndChangeCheck();
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        SetScript.UseEscDefinitedBehavior = EditorGUILayout.Toggle("Use Esc Definited Behavior", SetScript.UseEscDefinitedBehavior);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;
        SetScript.jInputSOData.OpenCloseSetFoldoutBool = Foldout(SetScript.jInputSOData.OpenCloseSetFoldoutBool, "jInput Open/Close Settings", true, EditorStyles.foldout);
        if (SetScript.jInputSOData.OpenCloseSetFoldoutBool)
        {
            EditorGUILayout.Space();
            SetScript.SetActiveToOpenClose = EditorGUILayout.Toggle("SetActive to OpenClose", SetScript.SetActiveToOpenClose, GUILayout.Height(20));
            /*2重Toggleにして,最初のToggleがfalseの時は2つめのtoggleを薄く表示したままで機能しないようにするには
				EditorGUILayout.BeginVertical (GUI.skin.box);
				SetScript.AlwaysOpenToggle = EditorGUILayout.Toggle ("Always OpenWindow", SetScript.AlwaysOpenToggle);
					EditorGUI.indentLevel++;
					EditorGUI.BeginDisabledGroup (SetScript.AlwaysOpenToggle);
					SetScript.MappingWindowDestroyToggle = EditorGUILayout.Toggle ("Destroy to Close", SetScript.MappingWindowDestroyToggle);
					EditorGUI.EndDisabledGroup ();
					EditorGUI.indentLevel--;
			 */

            serializedObject.Update();
            SerializedProperty EventProp = serializedObject.FindProperty("ClosingEvent");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(EventProp, true); //通常のInspectorの形と同じに描画
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }

            EditorGUILayout.BeginVertical(GUILayout.Height(10));
            EditorGUILayout.EndVertical();
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
        EditorGUI.EndDisabledGroup();
        //EditorGUILayout.Separator ();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;
        SetScript.jInputSOData.AxesFoldoutBool = Foldout(SetScript.jInputSOData.AxesFoldoutBool, "Axes Advance Settings", true, EditorStyles.foldout);
        if (SetScript.jInputSOData.AxesFoldoutBool)
        {
            EditorGUI.BeginChangeCheck();
            SetScript.DeadZone = EditorGUILayout.Slider("AxisDeadZone", SetScript.DeadZone, 0.01f, 1.0f);
            SetScript.Gravity = EditorGUILayout.Slider("AxisGravity", SetScript.Gravity, 0.1f, 50.0f);
            SetScript.Sensitivity = EditorGUILayout.Slider("AxisSensitivity", SetScript.Sensitivity, 0.1f, 50.0f);
            if (GUI.changed)
            {
                SetScript.AxesAdvanceToSOData();
                SetScript.jInputSOData.AxesSetApply();
            }
            EditorGUI.EndChangeCheck();
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
            SetScript.jInputSOData.SOValueDetermine();
        }
        //Repaint ();

        //元の標準のInspectorを表示
        //DrawDefaultInspector ();

    }

    ReorderableList m_list;
    ReorderableList m_list2p;
    ReorderableList m_list3p;
    ReorderableList m_list4p;
    SerializedProperty ReoListElement;

    void OnEnable()
    { //ドラッグ可能なInspectorでのListの設定はOnEnableで行わないとドラッグが効かない場合有り

        jInputSettings SetScript = target as jInputSettings;

        //new UnityEditorInternal.ReorderableList(List<SomeType>, typeof(SomeType), dragable, displayHeader, displayAddButton, displayRemoveButton);
        m_list = new ReorderableList(serializedObject, serializedObject.FindProperty("DefaultInputArray"), true, true, false, false);
        m_list.drawHeaderCallback = (rect) =>
        {
            EditorGUI.LabelField(rect, "1P");
        };

        m_list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            ReoListElement = m_list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            //入力欄の背景に薄く文字を入れる
            //EditorGUI.LabelField (rect, string.Format ("{0}:{1}", index, "stringValue"));

            /*ラベルと入力欄が一緒にドラッグできるようにしたい場合
			EditorGUI.LabelField (rect, "E" + index + ": " + SetScript.MenuItemHeadings [index]);
			EditorGUI.PropertyField (new Rect (rect.x + (rect.width * 1 / 2), rect.y, (rect.width * 1 / 2), EditorGUIUtility.singleLineHeight), ReoListElement, GUIContent.none);
			*/

            //大文字小文字関係なく文字列を比較
            int tempIndexOf = System.Array.FindIndex(jInput.AllInputNames, s => s.Equals(ReoListElement.stringValue, System.StringComparison.OrdinalIgnoreCase));
            if (tempIndexOf >= 0) //存在するInput名であれば
                ReoListElement.stringValue = jInput.AllInputNames[tempIndexOf];
            EditorGUI.BeginChangeCheck();
            tempIndexOf = EditorGUI.Popup(new Rect(rect.x + (rect.width * 0.55f) + 3, rect.y, rect.width - (rect.width * 0.55f) - 3, EditorGUIUtility.singleLineHeight), "", tempIndexOf, jInput.AllInputNames);
            if (index < SetScript.DefaultInputArray.Length)
            {
                if (tempIndexOf > 0)
                    SetScript.DefaultInputArray[index] = jInput.AllInputNames[tempIndexOf];
                else if (tempIndexOf == 0)
                    SetScript.DefaultInputArray[index] = string.Empty;
            }
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width * 0.55f, EditorGUIUtility.singleLineHeight), ReoListElement, GUIContent.none);
            if (GUI.changed)
            {
                ChangeDefaKey1pCheck = true;
                SetScript.ArrayCopyToSOData();
            }
            EditorGUI.EndChangeCheck();
        };

        m_list2p = new ReorderableList(serializedObject, serializedObject.FindProperty("DefaultInputArray2p"), true, true, false, false);
        m_list2p.drawHeaderCallback = (rect) =>
        {
            EditorGUI.LabelField(rect, "2P");
        };

        m_list2p.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            ReoListElement = m_list2p.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            //大文字小文字関係なく文字列を比較
            int tempIndexOf = System.Array.FindIndex(jInput.AllInputNames, s => s.Equals(ReoListElement.stringValue, System.StringComparison.OrdinalIgnoreCase));
            if (tempIndexOf >= 0) //存在するInput名であれば
                ReoListElement.stringValue = jInput.AllInputNames[tempIndexOf];
            EditorGUI.BeginChangeCheck();
            tempIndexOf = EditorGUI.Popup(new Rect(rect.x + (rect.width * 0.55f) + 3, rect.y, rect.width - (rect.width * 0.55f) - 3, EditorGUIUtility.singleLineHeight), "", tempIndexOf, jInput.AllInputNames);
            if (index < SetScript.DefaultInputArray2p.Length)
            {
                if (tempIndexOf > 0)
                    SetScript.DefaultInputArray2p[index] = jInput.AllInputNames[tempIndexOf];
                else if (tempIndexOf == 0)
                    SetScript.DefaultInputArray2p[index] = string.Empty;
            }
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width * 0.55f, EditorGUIUtility.singleLineHeight), ReoListElement, GUIContent.none);
            if (GUI.changed)
            {
                ChangeDefaKey2pCheck = true;
                SetScript.ArrayCopyToSOData2();
            }
            EditorGUI.EndChangeCheck();
        };

        m_list3p = new ReorderableList(serializedObject, serializedObject.FindProperty("DefaultInputArray3p"), true, true, false, false);
        m_list3p.drawHeaderCallback = (rect) =>
        {
            EditorGUI.LabelField(rect, "3P");
        };
        m_list3p.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            ReoListElement = m_list3p.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            //大文字小文字関係なく文字列を比較
            int tempIndexOf = System.Array.FindIndex(jInput.AllInputNames, s => s.Equals(ReoListElement.stringValue, System.StringComparison.OrdinalIgnoreCase));
            if (tempIndexOf >= 0) //存在するInput名であれば
                ReoListElement.stringValue = jInput.AllInputNames[tempIndexOf];
            EditorGUI.BeginChangeCheck();
            tempIndexOf = EditorGUI.Popup(new Rect(rect.x + (rect.width * 0.55f) + 3, rect.y, rect.width - (rect.width * 0.55f) - 3, EditorGUIUtility.singleLineHeight), "", tempIndexOf, jInput.AllInputNames);
            if (index < SetScript.DefaultInputArray3p.Length)
            {
                if (tempIndexOf > 0)
                    SetScript.DefaultInputArray3p[index] = jInput.AllInputNames[tempIndexOf];
                else if (tempIndexOf == 0)
                    SetScript.DefaultInputArray3p[index] = string.Empty;
            }
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width * 0.55f, EditorGUIUtility.singleLineHeight), ReoListElement, GUIContent.none);
            if (GUI.changed)
            {
                ChangeDefaKey3pCheck = true;
                SetScript.ArrayCopyToSOData3();
            }
            EditorGUI.EndChangeCheck();
        };

        m_list4p = new ReorderableList(serializedObject, serializedObject.FindProperty("DefaultInputArray4p"), true, true, false, false);
        m_list4p.drawHeaderCallback = (rect) =>
        {
            EditorGUI.LabelField(rect, "4P");
        };
        m_list4p.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            ReoListElement = m_list4p.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            //大文字小文字関係なく文字列を比較
            int tempIndexOf = System.Array.FindIndex(jInput.AllInputNames, s => s.Equals(ReoListElement.stringValue, System.StringComparison.OrdinalIgnoreCase));
            if (tempIndexOf >= 0) //存在するInput名であれば
                ReoListElement.stringValue = jInput.AllInputNames[tempIndexOf];
            EditorGUI.BeginChangeCheck();
            tempIndexOf = EditorGUI.Popup(new Rect(rect.x + (rect.width * 0.55f) + 3, rect.y, rect.width - (rect.width * 0.55f) - 3, EditorGUIUtility.singleLineHeight), "", tempIndexOf, jInput.AllInputNames);
            if (index < SetScript.DefaultInputArray4p.Length)
            {
                if (tempIndexOf > 0)
                    SetScript.DefaultInputArray4p[index] = jInput.AllInputNames[tempIndexOf];
                else if (tempIndexOf == 0)
                    SetScript.DefaultInputArray4p[index] = string.Empty;
            }
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width * 0.55f, EditorGUIUtility.singleLineHeight), ReoListElement, GUIContent.none);
            if (GUI.changed)
            {
                ChangeDefaKey4pCheck = true;
                SetScript.ArrayCopyToSOData4();
            }
            EditorGUI.EndChangeCheck();
        };

        /*
		m_list = new ReorderableList (serializedObject, serializedObject.FindProperty ("DefaultInputArray"), true, true, false, false);
		m_list.drawHeaderCallback = (rect) => {
				EditorGUI.LabelField (rect, DefaInputPlayerString);
		};

		m_list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
				ReoListElement = m_list.serializedProperty.GetArrayElementAtIndex (index);
				rect.y += 2;

				EditorGUI.LabelField (rect, "E" + index + ": " + SetScript.MenuItemHeadings [index]);
				EditorGUI.PropertyField (new Rect (rect.x + (rect.width * 1 / 2), rect.y, (rect.width * 1 / 2), EditorGUIUtility.singleLineHeight), ReoListElement, GUIContent.none);
		};
		*/

    }


}
