using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UGUICancelEventAdder : MonoBehaviour
{

	UGUITargetSelecter TargetSelectScript;

	void Start()
	{
		if (TargetSelectScript == null)
		{
			if (GetComponentInParent<jInputSettings>() != null
			&& GetComponentInParent<jInputSettings>().transform.Find("MainWindow") != null
			&& GetComponentInParent<jInputSettings>().transform.Find("MainWindow").GetComponentInChildren<UGUITargetSelecter>() != null)
				TargetSelectScript = GetComponentInParent<jInputSettings>().transform.Find("MainWindow").GetComponentInChildren<UGUITargetSelecter>();
		}

		if (GetComponent<EventTrigger>() && TargetSelectScript != null)
		{
			EventTrigger.Entry ThisEntry = new EventTrigger.Entry();
			ThisEntry.eventID = EventTriggerType.Cancel;
			//ThisEntry.callback = new EventTrigger.TriggerEvent();
			ThisEntry.callback.AddListener((x) => { TargetSelectScript.TargetSelectProcess(); });
			GetComponent<EventTrigger>().triggers.Add(ThisEntry);
		}
	}

	void Update()
	{ }

}
