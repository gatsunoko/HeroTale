using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(MenuItemsCommonSetting))]
public class MenuItemsCommonInspector : Editor
{

	Transform InMenuItemsTrns;
	Transform BaseItemTrns;
	string ComparisonName;
	bool AlignInvalidBool;
	jInputSettings SetScript;
	bool NonUGUICheck;

	public override void OnInspectorGUI()
	{
		EditorGUILayout.Space();
		MenuItemsCommonSetting ItemsCommonScript = target as MenuItemsCommonSetting;
		if (SetScript == null)
		{
			if (ItemsCommonScript.transform.GetComponentInParent<jInputSettings>())
				SetScript = ItemsCommonScript.transform.GetComponentInParent<jInputSettings>();
		}

		if (SetScript != null && SetScript.jInputNonUGUICheck)
		{
			NonUGUICheck = true;
			AlignInvalidBool = false;
			InMenuItemsTrns = ItemsCommonScript.transform;
			if (InMenuItemsTrns == null || InMenuItemsTrns.childCount <= 1)
			{
				AlignInvalidBool = true;
			} else
			{
				if (BaseItemTrns = InMenuItemsTrns.Find("MapperMenuItem00"))
				{

				} else
				{
					AlignInvalidBool = true;
				}
			}
		} else
		{
			NonUGUICheck = false;
		}

		GUI.changed = false;
		DrawDefaultInspector();
		Undo.RecordObject(ItemsCommonScript, "Inspector");

		if (NonUGUICheck)
		{
			ItemsCommonScript.AlertMarkPrefab = (GameObject)EditorGUILayout.ObjectField("AlertMarkPrefab", ItemsCommonScript.AlertMarkPrefab, typeof(GameObject), true);
			ItemsCommonScript.HeadingRelativePosi = EditorGUILayout.Vector2Field("HeadingRelativePosi", ItemsCommonScript.HeadingRelativePosi);
			EditorGUILayout.BeginVertical(GUI.skin.box);
			EditorGUILayout.LabelField("Vertical Align MenuItems");
			EditorGUI.indentLevel++;
			EditorGUI.BeginDisabledGroup(AlignInvalidBool);
			ItemsCommonScript.AlignInterval = EditorGUILayout.Slider("Interval (0.1-5.0)", ItemsCommonScript.AlignInterval, 0.1f, 5.0f);
			if (ItemsCommonScript.AlignInterval < 0.1f)
				ItemsCommonScript.AlignInterval = 0.1f;
			else if (ItemsCommonScript.AlignInterval > 5.0f)
				ItemsCommonScript.AlignInterval = 5.0f;
			EditorGUI.EndDisabledGroup();
			EditorGUI.indentLevel--;
			EditorGUILayout.BeginHorizontal();
			GUILayout.Box("", GUIStyle.none, GUILayout.ExpandWidth(true));
			EditorGUI.BeginDisabledGroup(AlignInvalidBool);
			if (GUILayout.Button("Align", GUILayout.Width(80)))
			{
				if (ItemsCommonScript.AlignInterval < 0.1f)
					ItemsCommonScript.AlignInterval = 0.1f;
				else if (ItemsCommonScript.AlignInterval > 5.0f)
					ItemsCommonScript.AlignInterval = 5.0f;
				GUI.FocusControl(""); //Inspectorのフォーカスを解除して入力欄を更新
						      //itemを整列させる
				if (InMenuItemsTrns != null)
				{
					int ItemListIndex = -1;
					List<string> MenuItemsList = new List<string>();
					for (int i = 0; i < InMenuItemsTrns.childCount; i++)
					{
						MenuItemsList.Add(InMenuItemsTrns.GetChild(i).name);
					}
					for (int i = 0; i <= 30; i++)
					{
						if (0 <= i && i <= 9)
						{
							ComparisonName = "MapperMenuItem0" + i;
						} else if (10 <= i && i <= 30)
						{
							ComparisonName = "MapperMenuItem" + i;
						}
						if (ComparisonName != null)
						{
							ItemListIndex = MenuItemsList.IndexOf(ComparisonName);
						}
						if (ItemListIndex == -1)
						{

						} else
						{
							Transform TemporaryItemTrns = InMenuItemsTrns.Find(ComparisonName);
							Undo.RecordObject(TemporaryItemTrns, "Inspectoree");
							TemporaryItemTrns.position = new Vector3(TemporaryItemTrns.position.x, BaseItemTrns.position.y - (ItemsCommonScript.AlignInterval * i), TemporaryItemTrns.position.z);
						}
					}
				}
			}
			EditorGUI.EndDisabledGroup();
			GUILayout.Box("", GUIStyle.none, GUILayout.ExpandWidth(false), GUILayout.Width(10));
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.Space();
		if (GUI.changed)
			EditorUtility.SetDirty(target);
	}
}
