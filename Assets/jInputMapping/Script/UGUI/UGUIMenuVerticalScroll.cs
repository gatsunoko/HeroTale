using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if (UNITY_EDITOR)
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class UGUIMenuVerticalScroll : MonoBehaviour
{
	[HideInInspector]
	public bool UseVerticalScroll;
	[HideInInspector]
	public float ScrollIntoRatio = 0.2f;
	[HideInInspector]
	public float ScrollIntoInertia = 0.1f;
	jInputSettings SetScript;
	[HideInInspector]
	public bool UseScrollChangeOnceCheck;
	bool ScrollingCheck;

	void Start()
	{
		if (SetScript == null)
			SetScript = GetComponentInParent<jInputSettings>();
		if (SetScript == null)
		{ Debug.LogError("[jInput] jInputSettings script is Not Found!!"); }
	}

	void Update()
	{
#if (UNITY_EDITOR)
		if (!EditorApplication.isPlaying && !EditorApplication.isPaused)
			ScrollSetting();
#endif
		if (SetScript == null)
			return;
		if (ScrollingCheck)
		{
			if (SetScript.MappingMode)
				SetScript.EscBehavior();
			SetScript.InhibitMappingModeCheck = true;
		} else
		{
			SetScript.InhibitMappingModeCheck = false;
		}

	}

	public void Scrolling()
	{
		ScrollingCheck = true;
	}
	//ScrollingCheckのOffはUGUIMainSelectedDeliver.csから選択状態のitemがScrollRect範囲内になったらOffにしている
	public void ScrollingCheckFalse()
	{
		ScrollingCheck = false;
	}
	public bool ScrollingNotify()
	{
		return ScrollingCheck;
	}

	void ScrollSetting()
	{
		if (UseVerticalScroll)
		{
			if (UseScrollChangeOnceCheck != true)
			{
				UseScrollChangeOnceCheck = true;
				if (GetComponent<Image>())
					GetComponent<Image>().enabled = true;
				if (GetComponent<ScrollRect>())
					GetComponent<ScrollRect>().enabled = true;
				if (transform.Find("Scrollbar") != null)
					transform.Find("Scrollbar").gameObject.SetActive(true);
			}
		} else
		{
			if (UseScrollChangeOnceCheck)
			{
				UseScrollChangeOnceCheck = false;
				if (GetComponent<Image>())
					GetComponent<Image>().enabled = false;
				if (GetComponent<ScrollRect>())
					GetComponent<ScrollRect>().enabled = false;
				if (transform.Find("Scrollbar") != null)
					transform.Find("Scrollbar").gameObject.SetActive(false);
			}
		}
	}

}
