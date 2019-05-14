using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class TextGenerateMenuItem : MonoBehaviour
{
	GameObject TextObject;
	GameObject HeadingTextObject;
	TextMesh tm;
	TextMesh htm;
	[HideInInspector]
	public float
		FirstHeadingScaleX;
	[Space(-3)]
	[Header("HeadingText")]
	[Space(3)]
	[SerializeField]
	int
		HeadingSize;
	[SerializeField]
	float
		HeadingScaleX = 1;
	[SerializeField]
	FontStyle
		HeadingStyle;
	[SerializeField]
	Font
		HeadingFont;
	[Space(3)]
	[SerializeField]
	Color
		HeadingColor = new Color(1.0f, 1.0f, 1.0f);
	[Space(1)]
	[Header("InputItemText")]
	[Space(2)]
	[SerializeField]
	int
		FontSize;
	[SerializeField]
	float
		FontScaleX = 1;
	[SerializeField]
	FontStyle
		FontStyle;
	[SerializeField]
	Font
		Font;

	void Awake()
	{
		if (TextObject == null)
		{
			if (transform.Find("TextPrefab") != null)
			{
				TextObject = transform.Find("TextPrefab").gameObject;
			} else
			{
				TextObjectCreate();
			}
		}
#if (UNITY_EDITOR)
		TextObject.hideFlags = HideFlags.HideInHierarchy;//.NotEditable;
#endif
		if (tm == null)
		{
			if (tm = TextObject.GetComponent<TextMesh>() as TextMesh)
			{

			} else
			{
				tm = TextObject.AddComponent<TextMesh>() as TextMesh;
			}
		}

		MeshRenderer rend = TextObject.GetComponent<MeshRenderer>();
		rend.material = tm.font.material;
		rend.shadowCastingMode = ShadowCastingMode.Off;
		rend.receiveShadows = false;

		if (TextObject.transform.parent != transform)
			TextObject.transform.SetParent(transform);
		TextObject.transform.localScale = new Vector3((1.0f / transform.localScale.x) * FontScaleX, (1.0f / transform.localScale.y), 1.0f);
		TextObject.transform.position = transform.position + transform.forward * -0.1f;


		if (HeadingTextObject == null)
		{
			if (transform.Find("HeadingTextPrefab") != null)
			{
				HeadingTextObject = transform.Find("HeadingTextPrefab").gameObject;
			} else
			{
				HeadingObjectCreate();
			}
		}
#if (UNITY_EDITOR)
		HeadingTextObject.hideFlags = HideFlags.HideInHierarchy;//.NotEditable;
#endif
		if (htm == null)
		{
			if (htm = HeadingTextObject.GetComponent<TextMesh>() as TextMesh)
			{

			} else
			{
				htm = HeadingTextObject.AddComponent<TextMesh>() as TextMesh;
			}
		}

		MeshRenderer HeadingRend = HeadingTextObject.GetComponent<MeshRenderer>();
		HeadingRend.material = htm.font.material;
		rend.shadowCastingMode = ShadowCastingMode.Off;
		HeadingRend.receiveShadows = false;

		HeadingTextObject.transform.SetParent(transform);
		if (FirstHeadingScaleX == 0)
		{
			FirstHeadingScaleX = HeadingTextObject.transform.localScale.x;
		}
		TextObject.transform.localScale = new Vector3((1.0f / transform.localScale.x) * FontScaleX, (1.0f / transform.localScale.y), 1.0f);
		HeadingTextObject.transform.localScale = new Vector3((1.0f / transform.localScale.x) * HeadingScaleX, (1.0f / transform.localScale.y), 1.0f);
		HeadingTextObject.transform.position = transform.position;
		//Headingのtransform.positionは更にSelectOrnamentのStart()から位置調節

	}

	void Update()
	{
		tm.fontSize = FontSize;
		tm.fontStyle = FontStyle;
		tm.font = Font;
		TextObject.transform.localScale = new Vector3((1.0f / transform.localScale.x) * FontScaleX, (1.0f / transform.localScale.y), 1.0f);
		htm.fontSize = HeadingSize;
		htm.fontStyle = HeadingStyle;
		htm.font = HeadingFont;
		HeadingTextObject.transform.localScale = new Vector3((1.0f / transform.localScale.x) * HeadingScaleX, (1.0f / transform.localScale.y), 1.0f);
		htm.color = HeadingColor;
	}

	void TextObjectCreate()
	{
		TextObject = new GameObject();
		TextObject.name = "TextPrefab";
		tm = TextObject.AddComponent<TextMesh>() as TextMesh;
		tm.color = new Color(0, 0, 0, 0);
		tm.offsetZ = 0.0f;
		tm.characterSize = 0.1f;
		tm.anchor = TextAnchor.MiddleCenter;
		tm.fontSize = FontSize;
		tm.fontStyle = FontStyle;
		tm.font = Font;
	}

	void HeadingObjectCreate()
	{
		HeadingTextObject = new GameObject();
		HeadingTextObject.name = "HeadingTextPrefab";
		htm = HeadingTextObject.AddComponent<TextMesh>() as TextMesh;
		htm.color = HeadingColor;
		htm.offsetZ = 0.0f;
		htm.characterSize = 0.1f;
		htm.anchor = TextAnchor.MiddleRight;
		htm.fontSize = HeadingSize;
		htm.fontStyle = HeadingStyle;
		htm.font = HeadingFont;
	}
}
