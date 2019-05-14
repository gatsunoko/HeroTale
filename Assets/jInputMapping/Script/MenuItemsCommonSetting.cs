using UnityEngine;
using System.Collections;

public class MenuItemsCommonSetting : MonoBehaviour
{
	[Space(5)]
	public GameObject
		MenuItemPrefab;
	[HideInInspector]
	public GameObject
		AlertMarkPrefab;
	[HideInInspector]
	public Vector2
		HeadingRelativePosi;
#if (UNITY_EDITOR)
	[HideInInspector]
	public float
		AlignInterval;
#endif

	void Start()
	{ }
	void Update()
	{ }

}
