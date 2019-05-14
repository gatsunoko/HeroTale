using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class UGUIButtonPointSelect : MonoBehaviour
{

	List<RaycastResult> RaycastResultsList;

	void Start ()
	{
		RaycastResultsList = new List<RaycastResult>();
	}

	void Update ()
	{
		//ポイントされたobjectを選択状態にする
		PointerEventData pointer = new PointerEventData (EventSystem.current);
		pointer.position = Input.mousePosition;
		EventSystem.current.RaycastAll (pointer, RaycastResultsList);
		if (RaycastResultsList.Count > 0) {
			GameObject RayHitGO = RaycastResultsList [0].gameObject;
			if (RayHitGO.GetComponent<Button> ()) {
				if (RayHitGO.GetComponent<Button> ().interactable)
					EventSystem.current.SetSelectedGameObject (RayHitGO);
			} else if (RayHitGO.transform.parent.GetComponent<Button> ()) {
				if (RayHitGO.transform.parent.GetComponent<Button> ().interactable)
					EventSystem.current.SetSelectedGameObject (RayHitGO.transform.parent.gameObject);
			}
		}
		RaycastResultsList.Clear ();

	}
}
