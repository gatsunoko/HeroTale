using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UGUITargetSelecter : MonoBehaviour
{
	[Space(5)]
	public GameObject SelectTarget;
	jInputSettings SetScript;

	void Start()
	{
		if (SetScript == null)
			SetScript = GetComponentInParent<jInputSettings>();
		if (SetScript == null)
			Debug.LogError("[jInput] jInputSettings script is Not Found!!");
	}

	public void TargetSelectProcess()
	{
		if (SelectTarget != null)
		{
			if (SetScript != null)
			{
				//UGUIのCancelに設定されているキーをInput登録する時に同時にCancelを働かせない
				//確認系の窓が出ているときも同様
				if (SetScript.MappingMode || SetScript.WindowSituation > 0 || SetScript.KeyDelayNotify())
					return;
			}
			if (EventSystem.current.currentSelectedGameObject != SelectTarget)
				EventSystem.current.SetSelectedGameObject(SelectTarget);
		}
	}

}
