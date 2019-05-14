using UnityEngine;
using System.Collections;

public class UGUIButtonBehaviors : MonoBehaviour
{

	jInputSettings SetScript;
	GameObject SaveConfirmWindow;

	void Start()
	{
		if (SetScript == null)
		{
			if (GetComponent<jInputSettings>())
				SetScript = GetComponent<jInputSettings>();
		}
		if (SetScript == null)
			Debug.LogError("[jInput] Error!! jInputSetting script is Not Found!!");
		if (SaveConfirmWindow == null)
		{
			if (transform.Find("ConfirmWindow"))
				SaveConfirmWindow = transform.Find("ConfirmWindow").gameObject;
		}
		if (SaveConfirmWindow == null)
			Debug.LogError("[jInput] Error!! ConfirmWindow gameObject is Not Found!!");
	}

	public void OnSelectConfirmYesProcess()
	{
		if (SetScript != null)
			SetScript.SaveNoSelectPosition = false;
	}
	public void OnClickConfirmYesProcess()
	{
		if (SetScript != null)
		{
			OnSelectConfirmYesProcess();
			SetScript.SaveProcess();
		}
	}

	public void OnSelectConfirmNoProcess()
	{
		if (SetScript != null)
			SetScript.SaveNoSelectPosition = true;
	}
	public void OnClickConfirmNoProcess()
	{
		if (SetScript != null)
		{
			OnSelectConfirmNoProcess();
			SaveConfirmWindow.SetActive(false);
			SetScript.WindowSituation = 0;
		}
	}

	public void OnSelectExitSaveProcess()
	{
		if (SetScript != null)
			SetScript.ExitSelectPosition = 0;
	}
	public void OnClickExitSaveProcess()
	{
		if (SetScript != null)
		{
			OnSelectExitSaveProcess();
			SetScript.SaveProcess();
			SetScript.ExitWindowProcess();
		}
	}

	public void OnSelectExitNoSaveProcess()
	{
		if (SetScript != null)
			SetScript.ExitSelectPosition = 1;
	}
	public void OnClickExitNoSaveProcess()
	{
		if (SetScript != null)
		{
			OnSelectExitNoSaveProcess();
			SetScript.ExitWindowProcess();
		}
	}

	public void OnSelectExitReturnProcess()
	{
		if (SetScript != null)
			SetScript.ExitSelectPosition = 2;
	}
	public void OnClickExitReturnProcess()
	{
		if (SetScript != null)
		{
			OnSelectExitReturnProcess();
			SetScript.ExitWindowReturnProcess();
		}
	}

	public void OnSelectPlayerNumProcess(int PlayerNum)
	{
		SetScript.PlayerSelectNum = PlayerNum;
	}
	public void OnClickPlayerNumProcess(int PlayerNum)
	{
		SetScript.PlayerSelectNum = PlayerNum;
		SetScript.PlayerSelectingProcess();
	}

	public void ClosePlayerWindowProcess()
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

	public void OnSelectOperateItemsProcess(int OperatePos)
	{
		if (SetScript != null)
		{
			SetScript.OperateItemLine = true;
			SetScript.PreparedOperateSelectPos = OperatePos;
			SetScript.OperateLineSelectPosition = OperatePos;
		}
	}
	public void OnClickOperateItemsProcess(int OperatePos)
	{
		if (SetScript != null)
		{
			SetScript.OperateItemLine = true;
			SetScript.PreparedOperateSelectPos = OperatePos;
			SetScript.OperateLineSelectPosition = OperatePos;
			if (SetScript.CurrentryRestore && OperatePos == 2
			|| SetScript.CurrentryRestore && OperatePos == 3)
				SetScript.CurrentryRestoreOperateProcess();
			else
				SetScript.OperateItemsProcess();
		}
	}

}
