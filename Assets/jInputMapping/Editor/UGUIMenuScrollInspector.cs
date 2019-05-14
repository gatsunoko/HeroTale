using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(UGUIMenuVerticalScroll))]
public class UGUIMenuScrollInspector : Editor
{
	bool InspectorDisabled;

	public override void OnInspectorGUI()
	{
		UGUIMenuVerticalScroll ScrollScript = target as UGUIMenuVerticalScroll;
		GUI.changed = false;

		Undo.RecordObject(ScrollScript, "Inspector");

		DrawDefaultInspector();
		EditorGUILayout.Space();

		if (!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused)
		{
			InspectorDisabled = false;
		} else
		{ InspectorDisabled = true; }

		EditorGUI.BeginChangeCheck();
		EditorGUI.BeginDisabledGroup(InspectorDisabled);
		ScrollScript.UseVerticalScroll = EditorGUILayout.Toggle("Use VerticalScroll", ScrollScript.UseVerticalScroll);
		EditorGUI.EndDisabledGroup();
		EditorGUI.BeginDisabledGroup(ScrollScript.UseVerticalScroll != true);
		ScrollScript.ScrollIntoRatio = EditorGUILayout.Slider("ScrollInto Ratio", ScrollScript.ScrollIntoRatio, 0, 1);
		ScrollScript.ScrollIntoInertia = EditorGUILayout.Slider("ScrollInto Inertia", ScrollScript.ScrollIntoInertia, 0, 1);
		EditorGUI.EndDisabledGroup();
		if (GUI.changed)
			EditorUtility.SetDirty(target);
		EditorGUI.EndChangeCheck();
	}
}
