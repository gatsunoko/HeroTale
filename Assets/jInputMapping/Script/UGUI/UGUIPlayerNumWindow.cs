using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class UGUIPlayerNumWindow : MonoBehaviour
{

	jInputSettings SetScript;
	GameObject[] PlayerNumAry;
	[Space(5)]
	public GameObject PlayerMappingNumsGO;
	public Button CloseButton;
	Button LastNumberPlayerButton;
	[SerializeField, HideInInspector]
	public int PlayerNum;
	HorizontalLayoutGroup LayoutGroupCpnt;


#if UNITY_EDITOR

	void Update()
	{
		if (!EditorApplication.isPlaying && !EditorApplication.isPaused)
		{
			if (SetScript == null)
				SetScript = GetComponentInParent<jInputSettings>();
			if (SetScript == null)
				Debug.LogError("[jInput] jInputSettings script is Not Found!!");
			if (PlayerMappingNumsGO == null || SetScript == null)
				return;
			if (LayoutGroupCpnt == null && PlayerMappingNumsGO.transform.GetComponent<HorizontalLayoutGroup>())
				LayoutGroupCpnt = PlayerMappingNumsGO.transform.GetComponent<HorizontalLayoutGroup>();

			if (PlayerNum != SetScript.PlayerNum)
			{
				PlayerNumAry = new GameObject[4];

				for (int i = 0; i < 4; i++)
				{
					for (int j = 0; j < PlayerMappingNumsGO.transform.childCount; j++)
					{
						GameObject TempChild = PlayerMappingNumsGO.transform.GetChild(j).gameObject;
						if (TempChild.name == "Player" + (i + 1) + "MappingNum")
						{
							PlayerNumAry[i] = TempChild;
							if (i < SetScript.PlayerNum)
								TempChild.SetActive(true);
							else
								TempChild.SetActive(false);
							break;
						}
					}
					if (i < SetScript.PlayerNum)
					{
						if (PlayerNumAry[i] == null)
						{
							Debug.LogError("[jInput] Error!! Player" + (i + 1) + "MappingNum gameObject is Not Found!!");
							return;
						}
					}
				}

				if (LayoutGroupCpnt != null)
				{
					switch (SetScript.PlayerNum)
					{
						case 1:
							break;
						case 2:
							//PlayerNumAry[0].transform.position = new Vector3(PlayerMappingNumsGO.transform.position.x, PlayerNumAry[0].transform.position.y, PlayerNumAry[0].transform.position.z) + transform.right * -77f; //transform.position + transform.right * -2.5f + transform.up * -0.4f + transform.forward * -0.2f;
							//PlayerNumAry[1].transform.position = new Vector3(PlayerMappingNumsGO.transform.position.x, PlayerNumAry[1].transform.position.y, PlayerNumAry[1].transform.position.z) + transform.right * 77f; //transform.position + transform.right * 2.5f + transform.up * -0.4f + transform.forward * -0.2f;
							LayoutGroupCpnt.spacing = -280;
							break;
						case 3:
							//PlayerNumAry[0].transform.position = new Vector3(PlayerMappingNumsGO.transform.position.x, PlayerNumAry[0].transform.position.y, PlayerNumAry[0].transform.position.z) + transform.right * -122f; //transform.position + transform.right * -3.2f + transform.up * -0.4f + transform.forward * -0.2f;
							//PlayerNumAry[1].transform.position = new Vector3(PlayerMappingNumsGO.transform.position.x, PlayerNumAry[1].transform.position.y, PlayerNumAry[1].transform.position.z); //transform.position + transform.right * 0.0f + transform.up * -0.4f + transform.forward * -0.2f;
							//PlayerNumAry[2].transform.position = new Vector3(PlayerMappingNumsGO.transform.position.x, PlayerNumAry[2].transform.position.y, PlayerNumAry[2].transform.position.z) + transform.right * 122; //transform.position + transform.right * 3.2f + transform.up * -0.4f + transform.forward * -0.2f;
							LayoutGroupCpnt.spacing = -140;
							break;
						case 4:
							//PlayerNumAry[0].transform.position = new Vector3(PlayerMappingNumsGO.transform.position.x, PlayerNumAry[0].transform.position.y, PlayerNumAry[0].transform.position.z) + transform.right * -158f; //transform.position + transform.right * -4.2f + transform.up * -0.4f + transform.forward * -0.2f;
							//PlayerNumAry[1].transform.position = new Vector3(PlayerMappingNumsGO.transform.position.x, PlayerNumAry[1].transform.position.y, PlayerNumAry[1].transform.position.z) + transform.right * -54f; //transform.position + transform.right * -1.4f + transform.up * -0.4f + transform.forward * -0.2f;
							//PlayerNumAry[2].transform.position = new Vector3(PlayerMappingNumsGO.transform.position.x, PlayerNumAry[2].transform.position.y, PlayerNumAry[2].transform.position.z) + transform.right * 54f; //transform.position + transform.right * 1.4f + transform.up * -0.4f + transform.forward * -0.2f;
							//PlayerNumAry[3].transform.position = new Vector3(PlayerMappingNumsGO.transform.position.x, PlayerNumAry[3].transform.position.y, PlayerNumAry[3].transform.position.z) + transform.right * 158f; //transform.position + transform.right * 4.2f + transform.up * -0.4f + transform.forward * -0.2f;
							LayoutGroupCpnt.spacing = 0;
							break;
					}
				}

				if (CloseButton != null)
				{ //Player人数が変わった際にCloseボタンからの右移動で右端のPlayerNumに選択が移動するように設定する
					if (1 <= SetScript.PlayerNum && SetScript.PlayerNum <= 4)
						LastNumberPlayerButton = PlayerNumAry[SetScript.PlayerNum - 1].GetComponent<Button>();
					else
						LastNumberPlayerButton = null;
					if (LastNumberPlayerButton != null)
					{
						if (CloseButton.navigation.mode == Navigation.Mode.Explicit)
						{
							//Navigation TempNav = new Navigation ();
							//TempNav.mode = Navigation.Mode.Explicit;
							Navigation TempNav = CloseButton.navigation;
							TempNav.selectOnRight = LastNumberPlayerButton;
							CloseButton.navigation = TempNav;
							EditorUtility.SetDirty(CloseButton);
						}
					}
				}
				PlayerNum = SetScript.PlayerNum;
			}
		}
	}
#endif

}
