using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UGUIKeepSelected : MonoBehaviour
{
	[Space(5)]
	public GameObject
		FirstSelectedGO;
	GameObject PrevSelectedGO;
	bool FirstSelectCheck;

	void Start()
	{
		PrevSelectedGO = FirstSelectedGO;
		FirstSelectCheck = true;
	}
	void OnEnable()
	{
		//ウインドウを開くたびカーソルを初期位置にする
		Start();
	}

	void Update()
	{
		if (FirstSelectCheck)
		{
			EventSystem.current.SetSelectedGameObject(FirstSelectedGO);
			FirstSelectCheck = false;
		}

		//無関係な場所をクリックしても非選択状態にならないように
		if (EventSystem.current.currentSelectedGameObject != null)
		{
			PrevSelectedGO = EventSystem.current.currentSelectedGameObject;
		} else
		{
			if (PrevSelectedGO != null)
				EventSystem.current.SetSelectedGameObject(PrevSelectedGO);
		}

	}
}
