using UnityEngine;
using System.Collections;

public class jInputMouseClick : MonoBehaviour
{

	public GameObject jCameraGO;
	Camera jCamera;
	jInputSettings SetScript;
	GameObject SaveConfirmWindow;
	int PlayerMappingNumber;

	void Start()
	{
		if (SetScript == null)
		{
			if (GetComponent<jInputSettings>())
				SetScript = GetComponent<jInputSettings>();
		}
		if (SetScript == null)
			Debug.LogError("[jInput] Error!! jInputSettings script is Not Found!!");
		if (SaveConfirmWindow == null)
		{
			if (transform.Find("ConfirmWindow"))
				SaveConfirmWindow = transform.Find("ConfirmWindow").gameObject;
		}
		if (SaveConfirmWindow == null)
			Debug.LogError("[jInput] Error!! ConfirmWindow gameObject is Not Found!!");
		if (jCamera == null && jCameraGO != null)
		{
			if (jCameraGO.GetComponent<Camera>())
				jCamera = jCameraGO.GetComponent<Camera>();
		}
		if (jCamera == null)
			Debug.LogError("[jInput] Error!! jInputCamera is Not Found!!");
	}

	void Update()
	{

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = jCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (SetScript.PlayerNumSituation && gameObject.activeSelf != false)
				{
					if (hit.collider.name == "Player1MappingNum" || hit.collider.name == "Player2MappingNum" || hit.collider.name == "Player3MappingNum" || hit.collider.name == "Player4MappingNum")
					{
						PlayerMappingNumHold PlayerMappingNumScript;
						if (PlayerMappingNumScript = hit.collider.gameObject.GetComponent<PlayerMappingNumHold>())
						{
							PlayerMappingNumber = PlayerMappingNumScript.PlayerMappingNumber;
							switch (PlayerMappingNumber)
							{
								case 1:
									SetScript.PlayerSelectNum = 1;
									break;
								case 2:
									SetScript.PlayerSelectNum = 2;
									break;
								case 3:
									SetScript.PlayerSelectNum = 3;
									break;
								case 4:
									SetScript.PlayerSelectNum = 4;
									break;
							}
						}
						SetScript.PlayerSelectingProcess();
					}
					if (hit.collider.name == "CloseButton")
					{
						if (SetScript.SetActiveToOpenClose)
						{
							gameObject.SetActive(false);
							SetScript.PlayerSelectNum = 1;
							SetScript.CloseButtonSelect = false;
						}
						if (SetScript.ClosingEvent != null)
							SetScript.ClosingEvent.Invoke();
					}
				} else
				{
					if (SetScript.WindowSituation == 1)
					{
						if (hit.collider.name == "YesButton")
						{
							SetScript.SaveProcess();
						}
						if (hit.collider.name == "NoButton")
						{
							SaveConfirmWindow.SetActive(false);
							SetScript.WindowSituation = 0;
						}
					} else if (SetScript.WindowSituation == 2)
					{
						if (hit.collider.name == "SaveButton")
						{
							SetScript.SaveProcess();
							SetScript.ExitWindowProcess();
						}
						if (hit.collider.name == "NoSaveButton")
						{
							SetScript.ExitWindowProcess();
						}
						if (hit.collider.name == "ReturnButton")
						{
							SetScript.ExitWindowReturnProcess();
						}
					} else
					{
						if (hit.collider.name.IndexOf("MapperMenuItem") == 0 && hit.collider.name.Length == 16)
						{ //PreparedSelectPositionは,KeyDeley中かここから感知できないので中間記憶を一度挟んでKeyDeley中にクリックでSelectPositionが移らないように
							SetScript.PreparedSelectPosition = hit.collider.gameObject.GetComponent<SelectOrnament>().MenuNum;
							SetScript.MenuItemsClickProcess();
						}
						if (SetScript.CurrentryRestore)
						{
							if (hit.collider.name == "MapperOperateItem02")
							{
								SetScript.PreparedOperateSelectPos = 2;
								SetScript.CurrentryRestoreOperateProcess();
							}
							if (hit.collider.name == "MapperOperateItem03")
							{
								SetScript.PreparedOperateSelectPos = 3;
								SetScript.CurrentryRestoreOperateProcess();
							}
						} else
						{
							if (hit.collider.name.IndexOf("MapperOperateItem") == 0 && hit.collider.name.Length == 19)
							{
								SetScript.PreparedOperateSelectPos = hit.collider.gameObject.GetComponent<SelectOrnament2>().MenuNum;
								SetScript.OperateItemsProcess();
							}
						}
					}
				}
			}
		}

	}

}
