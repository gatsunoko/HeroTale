using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UGUIPlayerSelectedDeliver : MonoBehaviour
{

	jInputSettings SetScript;
	GameObject FirstSelectedPlayer;
	GameObject PrevSelectedPlayer;
	GameObject PrevSelectedGO;
	bool FirstSetCheck;

	void Start()
	{
		FirstSetCheck = true;
		EventSystem.current.SetSelectedGameObject(null);
		if (SetScript == null)
			SetScript = GetComponentInParent<jInputSettings>();
		if (SetScript == null)
			Debug.LogError("[jInput] Error!! jInputSettings script is Not Found!!");
		if (FirstSelectedPlayer == null)
		{
			if (transform.parent.Find("PlayerMappingNums/Player1MappingNum").gameObject)
				FirstSelectedPlayer = transform.parent.Find("PlayerMappingNums/Player1MappingNum").gameObject;
			else
				Debug.LogError("[jInput] Error!! Player1MappingNum is Not Found!!");
		}
		if (PrevSelectedPlayer == null && FirstSelectedPlayer != null)
			PrevSelectedPlayer = FirstSelectedPlayer;
	}

	void OnEnable()
	{
		if (SetScript != null && SetScript.EscPlayerNumWindowCheck)
		{
			PrevSelectedPlayer = null;
			SetScript.EscPlayerNumWindowCheck = false;
		}
		Start();
	}

	public void SelectPlayerReset()
	{
		PrevSelectedPlayer = null;
	}

	void Update()
	{

		if (FirstSetCheck)
		{
			EventSystem.current.SetSelectedGameObject(PrevSelectedPlayer);
			FirstSetCheck = false;
		}

		//無関係な場所をクリックしても非選択状態にならないように
		if (EventSystem.current.currentSelectedGameObject != null)
		{
			if (EventSystem.current.currentSelectedGameObject != this.gameObject)
				PrevSelectedGO = EventSystem.current.currentSelectedGameObject;
		} else
		{
			if (PrevSelectedGO != null)
				EventSystem.current.SetSelectedGameObject(PrevSelectedGO);
		}

		if (EventSystem.current.currentSelectedGameObject != this.gameObject)
		{
			//PrevSelectedPlayer の更新
			if (PrevSelectedGO != null)
			{
				if (PrevSelectedGO.transform.parent.name == "PlayerMappingNums"
					&& PrevSelectedGO.name.IndexOf("Player") == 0
					&& PrevSelectedGO.name.IndexOf("MappingNum") == 7)
					PrevSelectedPlayer = PrevSelectedGO;
			}
		} else
		{
			if (PrevSelectedPlayer != null)
			{
				EventSystem.current.SetSelectedGameObject(PrevSelectedPlayer);
			} else
			{
				if (FirstSelectedPlayer != null)
					EventSystem.current.SetSelectedGameObject(FirstSelectedPlayer);
				else if (PrevSelectedGO != null)
					EventSystem.current.SetSelectedGameObject(PrevSelectedGO);
			}
		}
	}

}
