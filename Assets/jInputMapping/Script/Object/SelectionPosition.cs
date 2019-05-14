using UnityEngine;
using System.Collections;

public class SelectionPosition : MonoBehaviour
{

	jInputSettings SetScript;
	public GameObject SaveConfirmWindow;
	public GameObject ExitWindow;
	public GameObject PlayerNumWindow;
	public GameObject Selection;

	void Start()
	{
		if (SetScript == null)
			SetScript=GetComponent<jInputSettings>();
		if (SetScript == null)
			Debug.LogError("[jInput] Error!! jInputSettings script is Not Found!!");
		if (Selection != null)
			Selection.SetActive(false);
		else
			Debug.LogError("[jInput] Error!! Selection gameObject is Not Found!!");
	}

	void Update()
	{

		if (SetScript.WindowSituation == 1)
		{
			if (SetScript.SaveNoSelectPosition)
			{
				Selection.transform.position = SaveConfirmWindow.transform.position + SaveConfirmWindow.transform.right * 3.13f + SaveConfirmWindow.transform.up * 1.02f + SaveConfirmWindow.transform.forward * -0.1f;
			} else
			{
				Selection.transform.position = SaveConfirmWindow.transform.position + SaveConfirmWindow.transform.right * -2.88f + SaveConfirmWindow.transform.up * 1.02f + SaveConfirmWindow.transform.forward * -0.1f;
			}
			if (Selection.activeSelf != true)
				Selection.SetActive(true);
		} else if (SetScript.WindowSituation == 2)
		{
			if (SetScript.ExitSelectPosition == 0)
			{
				Selection.transform.position = ExitWindow.transform.position + ExitWindow.transform.right * -5.1f + ExitWindow.transform.up * 1.02f + ExitWindow.transform.forward * -0.1f;
			} else if (SetScript.ExitSelectPosition == 1)
			{
				Selection.transform.position = ExitWindow.transform.position + ExitWindow.transform.right * 0.07f + ExitWindow.transform.up * 1.02f + ExitWindow.transform.forward * -0.1f;
			} else if (SetScript.ExitSelectPosition == 2)
			{
				Selection.transform.position = ExitWindow.transform.position + ExitWindow.transform.right * 5.1f + ExitWindow.transform.up * 1.02f + ExitWindow.transform.forward * -0.1f;
			} else
			{
				SetScript.ExitSelectPosition = 0;
			}
			if (Selection.activeSelf != true)
				Selection.SetActive(true);
		} else if (SetScript.WindowSituation == 0 && SetScript.PlayerNumSituation && gameObject.activeSelf != false)
		{
			switch (SetScript.PlayerNum)
			{
				case 1:

					break;
				case 2:
					if (SetScript.PlayerSelectNum == 1)
						Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * -2.5f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
					if (SetScript.PlayerSelectNum == 2)
						Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * 2.5f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
					break;
				case 3:
					if (SetScript.PlayerSelectNum == 1)
						Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * -3.15f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
					if (SetScript.PlayerSelectNum == 2)
						Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * 0f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
					if (SetScript.PlayerSelectNum == 3)
						Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * 3.15f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
					break;
				case 4:
					if (SetScript.PlayerSelectNum == 1)
						Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * -4.18f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
					if (SetScript.PlayerSelectNum == 2)
						Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * -1.39f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
					if (SetScript.PlayerSelectNum == 3)
						Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * 1.39f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
					if (SetScript.PlayerSelectNum == 4)
						Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * 4.18f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
					break;
			}
			if (SetScript.CloseButtonSelect)
				Selection.SetActive(false);
			else if (Selection.activeSelf != true && SetScript.PlayerNum != 1)
				Selection.SetActive(true);
		} else
		{
			if (Selection != null && Selection.activeSelf != false)
				Selection.SetActive(false);
		}
	}
}
