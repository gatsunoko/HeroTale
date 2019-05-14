using UnityEngine;
using System.Collections;
#if (UNITY_EDITOR)
using UnityEditor;
#endif

[ExecuteInEditMode]
public class PlayerNumWindow : MonoBehaviour
{
	GameObject TextObject;
	GameObject MappingNumObjects;
	jInputSettings SetScript;
	int PlayerNum;
	int PlayerSelectNum;
	TextMesh[] PreyerNumTextMeshes;
	[Space(7)]
	[SerializeField]
	Color
		Player1_Color = new Color(0.85f, 0.4f, 0.4f, 1.0f);
	[SerializeField]
	Color
		Player2_Color = new Color(0.4f, 0.4f, 0.8f, 1.0f);
	[SerializeField]
	Color
		Player3_Color = new Color(0.92f, 0.9f, 0.38f, 1.0f);
	[SerializeField]
	Color
		Player4_Color = new Color(0.43f, 0.83f, 0.4f, 1.0f);
	Color SelectPlusColor = new Color(0.4f, 0.4f, 0.4f, 1.0f);
	[Space(3)]
	public GameObject
		NumPrefab;

	void Awake()
	{
		if (SetScript == null)
			SetScript = GetComponentInParent<jInputSettings>();
		if (SetScript == null)
			Debug.LogError("[jInput] jInputSettings script is Not Found!!");
		else
		{
			if (1 <= SetScript.PlayerNum && SetScript.PlayerNum <= 4)
			{
				PlayerNum = SetScript.PlayerNum;
			} else
			{
				PlayerNum = 4;
			}
		}

		PreyerNumTextMeshes = new TextMesh[PlayerNum + 1]; //[0] is empty all time
		bool[] MappingNumChecks = new bool[PlayerNum + 1]; //[0] is empty all time
		if (PlayerNum != 4)
		{
			if (MappingNumObjects == null && transform.Find("PlayerMappingNums"))
				MappingNumObjects = transform.Find("PlayerMappingNums").gameObject;
			if (MappingNumObjects != null)
				DestroyImmediate(MappingNumObjects);
		}
		if (PlayerNum != 1)
		{
			if (transform.Find("PlayerMappingNums"))
			{
				MappingNumObjects = transform.Find("PlayerMappingNums").gameObject;
			} else
			{
				MappingNumObjects = new GameObject();
				MappingNumObjects.name = "PlayerMappingNums";
				MappingNumObjects.AddComponent<PlayerMappingNums>();
				MappingNumObjects.transform.SetParent(transform);
			}
			for (int i = 1; i <= PlayerNum; i++)
			{
				for (int j = 0; j < MappingNumObjects.transform.childCount; j++)
				{
					GameObject TempChild = MappingNumObjects.transform.GetChild(j).gameObject;
					if (TempChild.name == "Player" + i + "MappingNum")
					{
						if (MappingNumChecks[i])
						{
							DestroyImmediate(TempChild);
						} else
						{
							MappingNumChecks[i] = true;
							TextObject = TempChild;
						}
					}
				}
				if (MappingNumChecks[i] != true)
				{
					TextObject = Instantiate(NumPrefab) as GameObject;
					TextObject.name = "Player" + i + "MappingNum";
					TextObject.transform.SetParent(MappingNumObjects.transform);
#if (UNITY_EDITOR)
					TextObject.hideFlags = HideFlags.HideInHierarchy;//.NotEditable;
#endif
				}
				TextObject.GetComponent<PlayerMappingNumHold>().PlayerMappingNumber = i;
				PreyerNumTextMeshes[i] = TextObject.transform.Find("Number").GetComponent<TextMesh>();
				PreyerNumTextMeshes[i].text = i.ToString();

				switch (PlayerNum)
				{
					case 1:
						break;
					case 2:
						if (i == 1)
							TextObject.transform.position = transform.position + transform.right * -2.5f + transform.up * -0.4f + transform.forward * -0.2f;
						if (i == 2)
							TextObject.transform.position = transform.position + transform.right * 2.5f + transform.up * -0.4f + transform.forward * -0.2f;
						break;
					case 3:
						if (i == 1)
							TextObject.transform.position = transform.position + transform.right * -3.2f + transform.up * -0.4f + transform.forward * -0.2f;
						if (i == 2)
							TextObject.transform.position = transform.position + transform.right * 0.0f + transform.up * -0.4f + transform.forward * -0.2f;
						if (i == 3)
							TextObject.transform.position = transform.position + transform.right * 3.2f + transform.up * -0.4f + transform.forward * -0.2f;
						break;
					case 4:
						if (i == 1)
							TextObject.transform.position = transform.position + transform.right * -4.2f + transform.up * -0.4f + transform.forward * -0.2f;
						if (i == 2)
							TextObject.transform.position = transform.position + transform.right * -1.4f + transform.up * -0.4f + transform.forward * -0.2f;
						if (i == 3)
							TextObject.transform.position = transform.position + transform.right * 1.4f + transform.up * -0.4f + transform.forward * -0.2f;
						if (i == 4)
							TextObject.transform.position = transform.position + transform.right * 4.2f + transform.up * -0.4f + transform.forward * -0.2f;
						break;
				}

			}
			PlayerColorSetting();
		}
	}

	void Update()
	{
#if (UNITY_EDITOR)
		if (!EditorApplication.isPlaying && !EditorApplication.isPaused)
		{
			Awake();
			return;
		}
#endif
		PlayerSelectNum = SetScript.PlayerSelectNum;
		if (SetScript.PlayerNum != 1)
		{
			switch (PlayerSelectNum)
			{
				case 1:
					PlayerColorSetting();
					if(SetScript.CloseButtonSelect!=true)
					PreyerNumTextMeshes[1].color = Player1_Color + SelectPlusColor;
					break;
				case 2:
					PlayerColorSetting();
					if (SetScript.CloseButtonSelect != true)
						PreyerNumTextMeshes[2].color = Player2_Color + SelectPlusColor;
					break;
				case 3:
					PlayerColorSetting();
					if (SetScript.CloseButtonSelect != true)
						PreyerNumTextMeshes[3].color = Player3_Color + SelectPlusColor;
					break;
				case 4:
					PlayerColorSetting();
					if (SetScript.CloseButtonSelect != true)
						PreyerNumTextMeshes[4].color = Player4_Color + SelectPlusColor;
					break;
			}
		}

	}

	void PlayerColorSetting()
	{
		if (PlayerNum >= 2)
		{
			PreyerNumTextMeshes[1].color = Player1_Color;
			PreyerNumTextMeshes[2].color = Player2_Color;
		}
		if (PlayerNum >= 3)
			PreyerNumTextMeshes[3].color = Player3_Color;
		if (PlayerNum >= 4)
			PreyerNumTextMeshes[4].color = Player4_Color;
	}

}
