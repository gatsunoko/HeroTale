using UnityEngine;
using System.Collections;
#if (UNITY_EDITOR)
using UnityEditor;
#endif

public class PlayerMappingNums : MonoBehaviour
{
		void Start ()
		{ }

		void Update ()
		{ }
}

#if (UNITY_EDITOR)
[CustomEditor(typeof(PlayerMappingNums))]
public class PlayerMappingNumsInspector : Editor
{
		public override void OnInspectorGUI ()
		{
				DrawDefaultInspector ();
				EditorGUILayout.HelpBox ("If you want design this Texts, changing 'PlayerMappingNumPrefab' in ProjectWindow. That is set 'PlayerNumWindow' GameObject.", MessageType.Info, true);
		}
}
#endif
