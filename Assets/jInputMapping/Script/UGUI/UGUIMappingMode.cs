using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UGUIMappingMode : MonoBehaviour
{

	jInputSettings SetScript;
	Button ThisButton;
	Color BaseHighlightedThisButtonColor;
	Navigation.Mode PrevNavMode;
	[Space(5)]
	public Color MappingModeButtonColor = new Color(0.95f, 0.35f, 0.3f, 1.0f);

	void Start()
	{
		if (SetScript == null)
			SetScript = GetComponentInParent<jInputSettings>();
		if (SetScript == null)
			Debug.LogError("[jInput] jInputSettings script is Not Found!!");

		if (ThisButton == null)
			ThisButton = GetComponent<Button>();
		if (ThisButton != null)
		{
			BaseHighlightedThisButtonColor = ThisButton.colors.highlightedColor;
			PrevNavMode = ThisButton.navigation.mode;
		} else
		{
			Debug.LogError("[jInput] Error!! Button component is Not Found!!");
		}

	}

	void Update()
	{
		if (SetScript != null)
		{
			if (SetScript.MappingMode)
			{
				//上下左右の移動を一時的になくす
				if (ThisButton.navigation.mode != Navigation.Mode.None)
					BlockMoveDir();
				if (EventSystem.current.currentSelectedGameObject == this.gameObject
					|| EventSystem.current.currentSelectedGameObject == transform.parent.gameObject)
				{
					Mapper.MoveDelayCall();
					if (ThisButton.transition == Selectable.Transition.ColorTint)
					{
						ColorBlock CustomClrB = ThisButton.colors;
						CustomClrB.highlightedColor = MappingModeButtonColor;
						ThisButton.colors = CustomClrB;
					}
					ThisButton.interactable = true;
				} else
				{
					if (ThisButton.transition == Selectable.Transition.ColorTint)
					{
						ColorBlock CustomClrB = ThisButton.colors;
						CustomClrB.highlightedColor = BaseHighlightedThisButtonColor;
						ThisButton.colors = CustomClrB;
					}
					ThisButton.interactable = false;
				}
			} else //SetScript.MappingMode == false;
			{
				if (SetScript.FirstSet)
					Mapper.MoveDelayCall();

				if (PrevNavMode != Navigation.Mode.None || PrevNavMode != ThisButton.navigation.mode)
					UsualMoveDir();
				if (ThisButton.transition == Selectable.Transition.ColorTint)
				{
					ColorBlock CustomClrB = ThisButton.colors;
					CustomClrB.highlightedColor = BaseHighlightedThisButtonColor;
					ThisButton.colors = CustomClrB;
				}
				if (SetScript.WindowSituation != 0)
					ThisButton.interactable = false;
				else
					ThisButton.interactable = true;
			}

		}

		if (ThisButton.interactable != true)
		{
			if (EventSystem.current.currentSelectedGameObject == this.gameObject
			|| EventSystem.current.currentSelectedGameObject == transform.parent.gameObject)
				EventSystem.current.SetSelectedGameObject(null);
		}

	}

	void BlockMoveDir()
	{
		PrevNavMode = ThisButton.navigation.mode;
		Navigation TempNav = ThisButton.navigation;
		TempNav.mode = Navigation.Mode.None;
		ThisButton.navigation = TempNav;
	}
	void UsualMoveDir()
	{
		Navigation TempNav = ThisButton.navigation;
		TempNav.mode = PrevNavMode;
		ThisButton.navigation = TempNav;
	}

}
