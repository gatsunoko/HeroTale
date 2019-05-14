using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
#if (UNITY_EDITOR)
using UnityEditor;
#endif

[ExecuteInEditMode]
public class TextGeneratePlayerSelectNum : MonoBehaviour
{
	GameObject TextObject;
	TextMesh tm;
	[Space(5)]
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
	[Space(3)]
	[SerializeField]
	Color
		FontColor = new Color(1.0f, 1.0f, 1.0f, 0.37f);

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
		rend.shadowCastingMode = ShadowCastingMode.Off;
		rend.shadowCastingMode = ShadowCastingMode.Off;
		rend.receiveShadows = false;

		if (TextObject.transform.parent != transform)
			TextObject.transform.SetParent(transform);
		TextObject.transform.localScale = new Vector3((1.0f * FontScaleX), 1.0f, 1.0f);
		TextObject.transform.position = transform.position;// + transform.forward * -0.1f;
	}

	void Start()
	{ }

	void Update()
	{
		tm.fontSize = FontSize;
		tm.fontStyle = FontStyle;
		tm.font = Font;
		TextObject.transform.localScale = new Vector3((1.0f * FontScaleX), 1.0f, 1.0f);
		tm.color = FontColor;
	}

	void TextObjectCreate()
	{
		TextObject = new GameObject();
		TextObject.name = "TextPrefab";
		tm = TextObject.AddComponent<TextMesh>() as TextMesh;
		tm.color = FontColor;
		tm.offsetZ = 0.0f;
		tm.characterSize = 0.1f;
		tm.anchor = TextAnchor.LowerLeft;
		tm.fontSize = FontSize;
		tm.fontStyle = FontStyle;
		tm.font = Font;
	}
}
