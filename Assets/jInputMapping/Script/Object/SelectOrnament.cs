using UnityEngine;
using System.Collections;
#if (UNITY_EDITOR)
using UnityEditor;
#endif

[ExecuteInEditMode]
public class SelectOrnament : MonoBehaviour
{
	[HideInInspector]
	public int
		MenuNum;
	string MenuNumString;
	[HideInInspector]
	public string
		HoldingText;
	[HideInInspector]
	public bool
		DuplicationTextColor;
	jInputSettings SetScript;
	bool InputWaiting;
	TextMesh ThisTextMesh;
	int NameCheck;
	Material RndMaterial;

	MenuItemsCommonSetting CommonSettingScript;
	Vector2 HeadingRelativePosi;
	string HeadingText;
	GameObject HeadingObject;
	GameObject AlertMarkObject;
	[HideInInspector]
	public bool
		AlertMarkCheck;
	bool AlertMarkDisplaying;
	[Space(4)]
	[SerializeField]
	Color
		BackNormalColor = new Color(0.45f, 0.35f, 0.35f, 0.9f);
	[SerializeField]
	Color
		BackSelectColor = new Color(0.75f, 0.75f, 0.9f, 0.95f);
	[SerializeField]
	Color
		BackWaitInput = new Color(1.0f, 0.4f, 0.35f, 0.95f);
	[Space(7)]
	[SerializeField]
	Color
		FontNormalColor = new Color(0.85f, 0.85f, 0.85f, 1.0f);
	[SerializeField]
	Color
		FontSelectColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	[SerializeField]
	Color
		FontWaitInput = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	[SerializeField]
	Color
		FontOtherInput = new Color(0.6f, 0.6f, 0.6f, 0.5f);
	[Space(7)]
	[SerializeField]
	Color
		SameFontNormal = new Color(0.7f, 0.7f, 0.85f, 1.0f);
	[SerializeField]
	Color
		SameFontSelect = new Color(0.7f, 0.7f, 1.0f, 1.0f);
	[SerializeField]
	Color
		SameFontWaitInput = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	[SerializeField]
	Color
		SameFontOtherInput = new Color(0.8f, 0.8f, 1.0f, 0.4f);


	void Start()
	{
		if (SetScript == null)
			SetScript = GetComponentInParent<jInputSettings>();
		if (SetScript == null)
			Debug.LogError("[jInput] jInputSettings script is Not Found!!");
		NameCheck = gameObject.name.IndexOf("MapperMenuItem");
		if (NameCheck != 0 || gameObject.name.Length != 16)
		{
			Debug.LogError("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
		} else
		{
			MenuNumString = gameObject.name.Substring(gameObject.name.Length - 2, 2);
			MenuNum = int.Parse(MenuNumString);
		}
		if (SetScript != null)
			HeadingText = SetScript.MenuItemHeadings[MenuNum];
		if (CommonSettingScript = transform.parent.GetComponent<MenuItemsCommonSetting>())
		{
			HeadingRelativePosi = CommonSettingScript.HeadingRelativePosi;
		} else
		{
			Debug.LogError("[jInput] Error!! MenuItemsCommonSetting script is Not Found!");
		}
		if (ThisTextMesh == null)
			ThisTextMesh = transform.Find("TextPrefab").GetComponent<TextMesh>();
		if (HeadingObject == null)
			HeadingObject = transform.Find("HeadingTextPrefab").gameObject;
		HeadingObject.GetComponent<TextMesh>().text = HeadingText;
		HeadingObject.transform.position = transform.position + transform.right * HeadingRelativePosi.x + transform.up * HeadingRelativePosi.y;
#if (UNITY_EDITOR)
		if (EditorApplication.isPlaying || EditorApplication.isPaused)
		{
#endif
			RndMaterial = GetComponent<Renderer>().material;
			HeadingTextPour();
#if (UNITY_EDITOR)
		}
#endif
	}

	void DuplicationInput()
	{
		DuplicationTextColor = true;
	}
	void IndividualInput()
	{
		DuplicationTextColor = false;
	}

	void OnDisable()
	{
		if (RndMaterial != null)
			RndMaterial.SetColor("_Color", BackNormalColor);
		if (ThisTextMesh != null)
			ThisTextMesh.color = FontNormalColor;
	}

	void Update()
	{

#if (UNITY_EDITOR)
		if (!EditorApplication.isPlaying && !EditorApplication.isPaused)
		{
			if (SetScript == null)
				SetScript = GetComponentInParent<jInputSettings>();
			if (HeadingObject == null)
				HeadingObject = transform.Find("HeadingTextPrefab").gameObject;
			HeadingTextPour();
			if (ThisTextMesh == null)
				ThisTextMesh = transform.Find("TextPrefab").GetComponent<TextMesh>();
			if (ThisTextMesh != null)
				ThisTextMesh.text = "Key Name";
		} else
		{
#endif

			if (SetScript != null)
			{
				if (ThisTextMesh != null)
				{
					if (DuplicationTextColor && InputWaiting != true)
					{
						if (SetScript.SelectPosition == MenuNum && SetScript.OperateItemLine != true && SetScript.FirstSet != true)
						{
							if (SetScript.MappingMode)
							{
								ThisTextMesh.color = SameFontWaitInput; //With sameText, Color of wait Key-in
							} else
							{
								ThisTextMesh.color = SameFontSelect; //With sameText, Select Color of normal
							}
						} else
						{
							if (SetScript.MappingMode)
							{
								ThisTextMesh.color = SameFontOtherInput; //With sameText, NonSelect Color of wait Key-in
							} else
							{
								ThisTextMesh.color = SameFontNormal; //With sameText, NonSelect Color of normal
							}
						}
					} else
					{
						if (SetScript.SelectPosition == MenuNum && SetScript.OperateItemLine != true && SetScript.FirstSet != true)
						{
							if (SetScript.MappingMode)
							{
								ThisTextMesh.color = FontWaitInput; //Select TextColor of wait Key-in
							} else
							{
								ThisTextMesh.color = FontSelectColor; //Select TextColor of normal
							}
						} else
						{
							if (SetScript.MappingMode)
							{
								ThisTextMesh.color = FontOtherInput; //NonSelect TextColor of wait Key-in
							} else
							{
								ThisTextMesh.color = FontNormalColor; //NonSelect TextColor of normal
							}
						}
					}

					if (RndMaterial != null)
					{
						if (SetScript.SelectPosition == MenuNum && SetScript.OperateItemLine != true && SetScript.FirstSet != true)
						{
							SetScript.CullentTextMesh = ThisTextMesh;
							if (SetScript.FirstSet != true)
							{
								if (SetScript.MappingMode)
								{ //BackColor of wait Key-in
									RndMaterial.SetColor("_Color", BackWaitInput);
									if (InputWaiting != true)
									{
										InputWaiting = true;
										SetScript.PreviousText = HoldingText;
										ThisTextMesh.text = "Input...";
									}
								} else
								{ //BackColor of select
									RndMaterial.SetColor("_Color", BackSelectColor);
									HoldingText = ThisTextMesh.text;
									InputWaiting = false;
								}
							}
						} else
						{ //BackColor of normal
							if (SetScript.MappingMode)
							{ //Other wait Key-in
								RndMaterial.SetColor("_Color", BackNormalColor - new Color(0.1f, 0.05f, 0.05f, 0.1f));
								HoldingText = ThisTextMesh.text;
								InputWaiting = false;
							} else
							{ //normal
								RndMaterial.SetColor("_Color", BackNormalColor);
								HoldingText = ThisTextMesh.text;
								InputWaiting = false;
							}
						}
					}
				}
			}

			if (AlertMarkCheck)
			{
				if (AlertMarkDisplaying != true)
				{
					AlertMarkDisplaying = true;
					AlertMarkObject = Instantiate(CommonSettingScript.AlertMarkPrefab) as GameObject;
					AlertMarkObject.transform.position = transform.position + new Vector3(-(transform.lossyScale.x * 0.47f), (transform.lossyScale.y * 0.22f), -1);
					AlertMarkObject.transform.SetParent(transform);
				}
			} else
			{
				AlertMarkDisplaying = false;
				if (AlertMarkObject != null)
				{
					Destroy(AlertMarkObject);
				}
			}

#if (UNITY_EDITOR)
		}
#endif

		if (CommonSettingScript != null && HeadingObject != null)
		{
			HeadingRelativePosi = CommonSettingScript.HeadingRelativePosi;
			HeadingObject.transform.position = transform.position + transform.right * HeadingRelativePosi.x + transform.up * HeadingRelativePosi.y;
		}

	}

	void HeadingTextPour()
	{
		if (SetScript != null && SetScript.MenuItemHeadings.Length > MenuNum)
			HeadingText = SetScript.MenuItemHeadings[MenuNum];
		if (HeadingObject != null)
			HeadingObject.GetComponent<TextMesh>().text = HeadingText;
	}

}
