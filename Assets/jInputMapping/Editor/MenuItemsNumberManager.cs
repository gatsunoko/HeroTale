using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

[InitializeOnLoad]
public class MenuItemsNumberManager : MonoBehaviour
{
	static double NextItemCheckTime = 5.0;
	static int MappingSetListNum;
	static GameObject MappingSet;
	static Transform InMenuItemsTrns;
	static jInputSettings SetScript;
	static GameObject MenuItemPrefab;
	static int ItemsNum;
	static string ComparisonName;
	static GameObject TemporaryItem;
	static string PreviousComparisonName;
	static int ItemListIndex;
	static GameObject MappingManagerPrefab;
	static GameObject OneBeforeItem;
	static Button FirstMenuItemButton;
	static Button SelectedDeliverButton;
	static Button TempOneBeforeButton;
	static bool FirstLastItemRectifyCheck;
	static bool BeforeItemInstCheck;
	static bool ExistMappingManagerCheck = false;

	static MenuItemsNumberManager()
	{

		EditorApplication.update += MyUpdate; //EditorApplication.update デリゲートに処理を入れると毎フレーム呼び出される
						      //EditorApplication.hierarchyWindowChanged += MyUpdate;
						      //EditorApplication.projectWindowChanged += MyUpdate;
						      //EditorApplication.playmodeStateChanged += MyUpdate;

	}

	static void MyUpdate()
	{
		if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isPlaying || EditorApplication.isPaused)
		{

			if (ExistMappingManagerCheck != true)
			{
				if (GameObject.Find("jInputMappingManager") == null)
				{
					if (MappingManagerPrefab == null)
					{
						//Resources.Loadを使っても良い
						string[] Pathes = AssetDatabase.FindAssets("jInputMappingManager t:GameObject");
						if (Pathes.Length >= 1)
						{
							string MappingManagerPath = AssetDatabase.GUIDToAssetPath(Pathes[0]);
							MappingManagerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(MappingManagerPath);
						}
						if (Pathes.Length > 1)
						{
							Debug.LogError("[jInput] There are some GameObjects included in the name 'jInputMappingManager' in Project window!! There is a possibility that it may not operate normally.");
						}
					}
					if (MappingManagerPrefab != null)
					{
						GameObject Ist = Instantiate(MappingManagerPrefab) as GameObject;
						Ist.name = "jInputMappingManager";
						Ist.hideFlags = HideFlags.HideInHierarchy;
						Ist.GetComponent<Mapper>().IstCheck = true;
						ExistMappingManagerCheck = true;
					} else
					{
						Debug.LogError("[jInput] Error! jInputMappingManager Prefab is Not Found in Project window!!");
						EditorPlaymodeStop();
						return;
					}
				}
			}
		} else
		{
			ExistMappingManagerCheck = false;
			GameObject FindMappingManager;
			if (FindMappingManager = GameObject.Find("jInputMappingManager"))
			{
				if (FindMappingManager.GetComponent<Mapper>().IstCheck)
					DestroyImmediate(GameObject.Find("jInputMappingManager"));
			}
		}


		if (Mapper.EditorPlaymodeStop != false)
		{
			EditorPlaymodeStop();
		}

		//PlayするとjInputMappingSetが非表示だと取れずにエラーになる部分があるのでエディット中だけ動作
		if (!EditorApplication.isPlaying && !EditorApplication.isPaused)
		{

			if (jInputSettings.MappingSetList != null && jInputSettings.MappingSetList.Count > 0)
			{
				if (jInputSettings.MappingSetList.Count > 1 && jInputSettings.MappingSetList.Count != MappingSetListNum)
				{
					Debug.LogError("[jInput] There are some jInputMappingSet gameObject in the scene! It can works only one!");
				}
				MappingSetListNum = jInputSettings.MappingSetList.Count;
				if (jInputSettings.MappingSetList[0] != null)
				{
					if (MappingSet == null || MappingSet != jInputSettings.MappingSetList[0])
						MappingSet = jInputSettings.MappingSetList[0];
				}
			}
			if (MappingSet != null)
			{
				if (SetScript == null)
					SetScript = MappingSet.GetComponent<jInputSettings>();
				if (SetScript != null)
				{
					if (SetScript.jInputSOData == null)
					{
						//Resources.Loadを使っても良い
						string[] Pathes = AssetDatabase.FindAssets("jInputData t:jInput");
						if (Pathes.Length >= 1)
						{
							string MappingManagerPath = AssetDatabase.GUIDToAssetPath(Pathes[0]);
							SetScript.jInputSOData = AssetDatabase.LoadAssetAtPath<jInput>(MappingManagerPath);
						}
						if (Pathes.Length > 1)
							Debug.LogWarning("[jInput] There are some jInputData in this project!");
					}

					if (MappingSet != null && SetScript.jInputSOData != null && SetScript.jInputSOData.DefaultInputNameInconsistencyCheck)
					{
						if (!EditorApplication.isPlaying && !EditorApplication.isPaused && EditorApplication.isPlayingOrWillChangePlaymode)
							Selection.activeGameObject = MappingSet;
					}
					if (SetScript.transform.Find("MainWindow/InMapperMenuItems") != null)
						InMenuItemsTrns = SetScript.transform.Find("MainWindow/InMapperMenuItems");
					else
						Debug.LogError("[jInput] Error!! gameObject with MenuItemsCommonSetting script is Not Found!!");
					if (InMenuItemsTrns != null)
					{
						if (InMenuItemsTrns.GetComponent<MenuItemsCommonSetting>().MenuItemPrefab != null)
							MenuItemPrefab = InMenuItemsTrns.GetComponent<MenuItemsCommonSetting>().MenuItemPrefab;
						else
							Debug.LogError("[jInput] Error!! MenuItemPrefab in InMapperMenuItems gameObject is Not Found!!");
						if (SetScript.jInputNonUGUICheck != true && InMenuItemsTrns != null)
						{
							if (InMenuItemsTrns.parent.Find("SelectedDeliver") != null)
								SelectedDeliverButton = InMenuItemsTrns.parent.Find("SelectedDeliver").GetComponent<Button>();
							else
								Debug.LogError("[jInput] Error!! SelectedDeliver in MainWindow is Not Found!!");
						}
					}
				}

				if (EditorApplication.timeSinceStartup > NextItemCheckTime)
				{
					if (InMenuItemsTrns != null)
					{
						ItemsNum = InMenuItemsTrns.childCount;
						ItemsNumMatch();
					}
					NextItemCheckTime = EditorApplication.timeSinceStartup + 0.25;
				}
			}
		}
	}

	static void ItemsNumMatch()
	{
		ItemListIndex = -1;

		List<string> MenuItemsList = new List<string>();
		for (int i = 0; i < ItemsNum; i++)
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
			if (FirstMenuItemButton == null)
			{
				if (InMenuItemsTrns.transform.Find("MapperMenuItem00/Button") != null && InMenuItemsTrns.transform.Find("MapperMenuItem00/Button").GetComponent<Button>())
					FirstMenuItemButton = InMenuItemsTrns.transform.Find("MapperMenuItem00/Button").GetComponent<Button>();
			}

			if (0 <= i && i < SetScript.MenuItemHeadings.Length)
			{
				if (ItemListIndex == -1)
				{ //Itemが無い場合

					if (MenuItemPrefab == null)
						return;
					BeforeItemInstCheck = true;
					TemporaryItem = PrefabUtility.InstantiatePrefab(MenuItemPrefab) as GameObject;
					Undo.RegisterCreatedObjectUndo(TemporaryItem, "Inspector");
					if (i == 0)
					{
						OneBeforeItem = null;
						FirstLastItemRectifyCheck = true;
						TemporaryItem.transform.SetParent(InMenuItemsTrns);
						TemporaryItem.transform.position = InMenuItemsTrns.position;
						TemporaryItem.transform.rotation = InMenuItemsTrns.rotation;
						TemporaryItem.transform.localScale = new Vector3(1, 1, 1);
					} else if (0 < i && i <= 30)
					{ //ひとつ前の番号の少し下にtransformを変える
						if (InMenuItemsTrns.Find(PreviousComparisonName))
						{
							OneBeforeItem = InMenuItemsTrns.Find(PreviousComparisonName).gameObject;
							TemporaryItem.transform.SetParent(InMenuItemsTrns);
							if (SetScript != null && SetScript.jInputNonUGUICheck != true)
								TemporaryItem.transform.position = OneBeforeItem.transform.position + new Vector3(0, -18, 0);
							else
								TemporaryItem.transform.position = OneBeforeItem.transform.position + new Vector3(0, -0.6f, 0);
							TemporaryItem.transform.rotation = OneBeforeItem.transform.rotation;
							TemporaryItem.transform.localScale = OneBeforeItem.transform.localScale;
						}
					}
					if (SetScript.jInputNonUGUICheck != true && TemporaryItem.transform.Find("Button") != null)
					{
						Button TempItemButton;
						if (TempItemButton = TemporaryItem.transform.Find("Button").GetComponent<Button>())
						{
							if (TempItemButton.navigation.mode == Navigation.Mode.Explicit)
							{
								if (OneBeforeItem != null && OneBeforeItem.transform.Find("Button") != null && OneBeforeItem.transform.Find("Button").GetComponent<Button>() != null)
									TempOneBeforeButton = OneBeforeItem.transform.Find("Button").GetComponent<Button>();
								Navigation TempNav;
								if (i == (SetScript.MenuItemHeadings.Length - 1))
								{
									FirstLastItemRectifyCheck = false;
									TempNav = TempItemButton.navigation;
									TempNav.selectOnRight = SelectedDeliverButton;
									if (TempOneBeforeButton != null)
										TempNav.selectOnUp = TempOneBeforeButton;
									if (FirstMenuItemButton != null)
										TempNav.selectOnDown = FirstMenuItemButton;
									TempItemButton.navigation = TempNav;
									EditorUtility.SetDirty(TempItemButton);
									if (FirstMenuItemButton != null)
									{
										TempNav = FirstMenuItemButton.navigation;
										TempNav.selectOnUp = TempItemButton;
										FirstMenuItemButton.navigation = TempNav;
										EditorUtility.SetDirty(FirstMenuItemButton);
									}
								} else
								{
									TempNav = TempItemButton.navigation;
									if (TempOneBeforeButton != null)
										TempNav.selectOnUp = TempOneBeforeButton;
									TempNav.selectOnRight = SelectedDeliverButton;
									TempItemButton.navigation = TempNav;
									EditorUtility.SetDirty(TempItemButton);
								}
								if (TempOneBeforeButton != null)
								{
									TempNav = TempOneBeforeButton.navigation;
									TempNav.selectOnDown = TempItemButton;
									TempOneBeforeButton.navigation = TempNav;
									EditorUtility.SetDirty(TempOneBeforeButton);
								}
							}
						}
					}
					TemporaryItem.name = ComparisonName;
					TemporaryItem.transform.SetSiblingIndex(i);

				} else
				{ //Itemがある場合

					MenuItemsList.RemoveAt(ItemListIndex);
					if (TemporaryItem = InMenuItemsTrns.Find(ComparisonName).gameObject)
					{
						if (ItemListIndex != i && ItemListIndex != -1)
							TemporaryItem.transform.SetSiblingIndex(i);
						if (SetScript.jInputNonUGUICheck != true && TemporaryItem.transform.Find("Button") != null)
						{
							Button TempItemButton;
							if (TempItemButton = TemporaryItem.transform.Find("Button").GetComponent<Button>())
							{
								if (FirstLastItemRectifyCheck && i == (SetScript.MenuItemHeadings.Length - 1))
								{
									FirstLastItemRectifyCheck = false;
									if (TempItemButton.navigation.mode == Navigation.Mode.Explicit)
									{
										Navigation TempNav;
										TempNav = TempItemButton.navigation;
										if (FirstMenuItemButton != null)
											TempNav.selectOnDown = FirstMenuItemButton;
										TempItemButton.navigation = TempNav;
										EditorUtility.SetDirty(TempItemButton);
									}
									if (FirstMenuItemButton != null && FirstMenuItemButton.navigation.mode == Navigation.Mode.Explicit)
									{
										Navigation TempFirstNav;
										TempFirstNav = FirstMenuItemButton.navigation;
										TempFirstNav.selectOnUp = TempItemButton;
										FirstMenuItemButton.navigation = TempFirstNav;
										EditorUtility.SetDirty(FirstMenuItemButton);
									}
								}
								if (BeforeItemInstCheck && i != 0)
								{
									BeforeItemInstCheck = false;
									if (TempItemButton.navigation.mode == Navigation.Mode.Explicit)
									{
										if (InMenuItemsTrns.Find(PreviousComparisonName))
											OneBeforeItem = InMenuItemsTrns.Find(PreviousComparisonName).gameObject;
										if (OneBeforeItem != null && OneBeforeItem.transform.Find("Button") != null && OneBeforeItem.transform.Find("Button").GetComponent<Button>() != null)
											TempOneBeforeButton = OneBeforeItem.transform.Find("Button").GetComponent<Button>();
										Navigation TempNav;
										TempNav = TempItemButton.navigation;
										if (TempOneBeforeButton != null)
											TempNav.selectOnUp = TempOneBeforeButton;
										else
											TempNav.selectOnUp = TempItemButton.navigation.selectOnUp;
										TempItemButton.navigation = TempNav;
										EditorUtility.SetDirty(TempItemButton);
										if (TempOneBeforeButton != null)
										{
											TempNav = TempOneBeforeButton.navigation;
											TempNav.selectOnDown = TempItemButton;
											TempOneBeforeButton.navigation = TempNav;
											EditorUtility.SetDirty(TempOneBeforeButton);
										}
									}
								}
							}
						}
					}

				}
			} else
			{ //ナンバリングされたオブジェクトが配列の数より多い場合は削除
				if (ItemListIndex != -1)
				{
					if (TemporaryItem = InMenuItemsTrns.Find(ComparisonName).gameObject)
					{
						Undo.DestroyObjectImmediate(TemporaryItem);
						DestroyImmediate(TemporaryItem);
					}
					MenuItemsList.RemoveAt(ItemListIndex);
				}
				FirstLastItemRectifyCheck = true;
			}
			PreviousComparisonName = ComparisonName;
			OneBeforeItem = null;
			TempOneBeforeButton = null;
		}
		if (MenuItemsList.Count > 0)
		{
			Debug.LogError("[jInput] There is otiose object or same name object in jInputMappingSet/InMapperMenuItems.");
		}
	}

	static void EditorPlaymodeStop()
	{
		Mapper.EditorPlaymodeStop = false;
		EditorApplication.isPaused = false;
		EditorApplication.isPlaying = false;
	}

}
