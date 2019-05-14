using UnityEngine;
using System.Collections;

public class SelectOrnamentCloseButton : MonoBehaviour
{
	jInputSettings SetScript;
	Material RndMaterial;
	TextMesh TextComponent;
	GameObject Selection;
	[Space(4)]
	[SerializeField]
	Color
		BackNormalColor = new Color(0.5f, 0.4f, 0.5f, 0.9f);
	[SerializeField]
	Color
		BackSelectColor = new Color(0.75f, 0.75f, 1.0f, 0.95f);
	[Space(7)]
	[SerializeField]
	Color
		FontNormalColor = new Color(0.65f, 0.65f, 0.65f, 1.0f);
	[SerializeField]
	Color
		FontSelectColor = new Color(0.97f, 0.95f, 0.95f, 1.0f);

	void Start()
	{
		if (SetScript == null)
			SetScript = GetComponentInParent<jInputSettings>();
		if (SetScript == null)
			Debug.LogError("[jInput] jInputSettings script is Not Found!!");
		TextComponent = transform.Find("TextPrefab").gameObject.GetComponent<TextMesh>();
		RndMaterial = GetComponent<Renderer>().material;
		if (SetScript != null)
			Selection = SetScript.transform.Find("Selection").gameObject;
		if (Selection == null)
		{
			Debug.LogError("[jInput] Selection object is Not Found!!");
		}
	}

	void Update()
	{
		if (SetScript != null && SetScript.CloseButtonSelect)
		{
			RndMaterial.SetColor("_Color", BackSelectColor);
			TextComponent.color = FontSelectColor;
			if (Selection != null)
			{
				Selection.SetActive(false);
			}
		} else
		{ //normal(NonSelect)
			RndMaterial.SetColor("_Color", BackNormalColor);
			TextComponent.color = FontNormalColor;
			if (Selection != null)
			{
				Selection.SetActive(true);
			}
		}

	}
}
