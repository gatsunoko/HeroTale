using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
#if (UNITY_EDITOR)
using UnityEditor;
#endif

[ExecuteInEditMode]
public class TextGenerateOperate : MonoBehaviour
{
	GameObject TextObject;
	TextMesh tm;
	[Space(5)]
	[SerializeField]
	string
		Text;
	[Space(7)]
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
			if (TextObject.GetComponent<TextMesh>() as TextMesh)
				tm = TextObject.GetComponent<TextMesh>() as TextMesh;
			else
				Debug.LogError("[jInput] Error!! TextMesh component on " + this.gameObject.name + " child gameObject is Not Found!!");
		}

		MeshRenderer rend = TextObject.GetComponent<MeshRenderer>();
		rend.material = tm.font.material;
		rend.shadowCastingMode = ShadowCastingMode.Off;
		rend.receiveShadows = false;

		if (TextObject.transform.parent != transform)
			TextObject.transform.SetParent(transform);
		TextObject.transform.localScale = new Vector3((1.0f / transform.localScale.x) * FontScaleX, (1.0f / transform.localScale.y), 1.0f);
		TextObject.transform.position = transform.position + transform.forward * -0.1f;
	}

	void Update()
	{
		tm.text = Text;
		tm.fontSize = FontSize;
		tm.fontStyle = FontStyle;
		tm.font = Font;
		TextObject.transform.localScale = new Vector3((1.0f / transform.localScale.x) * FontScaleX, (1.0f / transform.localScale.y), 1.0f);
	}

	public void TextObjectCreate()
	{
		TextObject = new GameObject();
		TextObject.name = "TextPrefab";
		tm = TextObject.AddComponent<TextMesh>() as TextMesh;
		tm.color = new Color(0, 0, 0, 0);
		tm.text = Text;
		tm.offsetZ = 0.0f;
		tm.characterSize = 0.1f;
		tm.anchor = TextAnchor.MiddleCenter;
		tm.fontSize = FontSize;
		tm.fontStyle = FontStyle;
		tm.font = Font;
	}

}
