using UnityEngine;
using System.Collections;

public class UGUISelectionController : MonoBehaviour
{
	[Space(5)]
	public GameObject SelectionGO;
	jInputSettings SetScript;
	bool FirstSelectCheck;

	void Start()
	{
		if (SetScript == null)
			SetScript = GetComponent<jInputSettings>();
		if (SetScript == null)
			Debug.LogError("[jInput] Error!! jInputSettings script is Not Found!!");
		if (SelectionGO != null)
			SelectionGO.SetActive(false);
		else
			Debug.LogError("[jInput] Error!! Selection is Not Found!!");
		FirstSelectCheck = true;
	}

	void OnEnable()
	{
		if (SelectionGO != null)
			SelectionGO.SetActive(false);
	}

	void Update()
	{
		if (SetScript == null || SelectionGO == null || FirstSelectCheck)
			return;

		if (SetScript.PlayerNumSituation || SetScript.WindowSituation == 1 || SetScript.WindowSituation == 2)
		{
			if (SelectionGO.activeSelf != true && SetScript.CloseButtonSelect != true)
				SelectionGO.SetActive(true);
		} else
		{
			if (SelectionGO.activeSelf)
				SelectionGO.SetActive(false);
			FirstSelectCheck = true;
		}

	}

	public void SelectionSet(GameObject SetBaseGO)
	{
		if (SelectionGO == null)
			return;
		SelectionGO.transform.position = new Vector3(SetBaseGO.transform.position.x, SelectionGO.transform.position.y, SelectionGO.transform.position.z);
		//以下2行によりUpdate()内でSetActive(true)になる
		SetScript.CloseButtonSelect = false;
		FirstSelectCheck = false;
	}
	public void SelectionFalse()
	{
		if (SelectionGO == null)
			return;
		SetScript.CloseButtonSelect = true;
		if (SelectionGO.activeSelf)
			SelectionGO.SetActive(false);
	}

}
