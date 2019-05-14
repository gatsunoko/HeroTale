using UnityEngine;
using System.Collections;

public class SelectOrnamentSaveButton : MonoBehaviour
{
	jInputSettings SetScript;
	TextMesh TextComponent;
	[Space(5)]
	[SerializeField]
	Color
		FontColor = new Color(0.3f, 0.2f, 0.25f, 0.9f);
	Color SelectPlusColor = new Color(0.15f, 0.1f, 0.15f, 1.0f);

	void Start()
	{
		if (SetScript == null)
			SetScript = GetComponentInParent<jInputSettings>();
		if (SetScript == null)
			Debug.LogError("[jInput] jInputSettings script is Not Found!!");
		if (TextComponent == null)
			TextComponent = transform.Find("TextPrefab").gameObject.GetComponent<TextMesh>();
	}

	void Update()
	{
		if (SetScript == null)
		{
			TextComponent.color = FontColor;
			return;
		}
		if (SetScript.SaveNoSelectPosition && gameObject.name == "NoButton" ||
			SetScript.SaveNoSelectPosition != true && gameObject.name == "YesButton" ||
			SetScript.ExitSelectPosition == 0 && gameObject.name == "SaveButton" ||
			SetScript.ExitSelectPosition == 1 && gameObject.name == "NoSaveButton" ||
			SetScript.ExitSelectPosition == 2 && gameObject.name == "ReturnButton")
		{
			TextComponent.color = FontColor + SelectPlusColor;
		} else
		{
			TextComponent.color = FontColor;
		}
	}
}
