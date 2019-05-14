using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class jInputUGUIOperationSender : MonoBehaviour
{

	public bool UGUIOperationInvalid;
	public bool UGUIOperationInvalid2p;
	public bool UGUIOperationInvalid3p;
	public bool UGUIOperationInvalid4p;
	AxisEventData m_AxisEventData;

	void Start()
	{ }

	void Update()
	{
		if (EventSystem.current != null)
		{
			m_AxisEventData = new AxisEventData(EventSystem.current);

			if (Mapper.jInputOnUp && UGUIOperationInvalid != true
			|| Mapper.jInputOnUp2p && UGUIOperationInvalid2p != true
			|| Mapper.jInputOnUp3p && UGUIOperationInvalid3p != true
			|| Mapper.jInputOnUp4p && UGUIOperationInvalid4p != true)
				UGUIUpMove();
			if (Mapper.jInputOnDown && UGUIOperationInvalid != true
			|| Mapper.jInputOnDown2p && UGUIOperationInvalid2p != true
			|| Mapper.jInputOnDown3p && UGUIOperationInvalid3p != true
			|| Mapper.jInputOnDown4p && UGUIOperationInvalid4p != true)
				UGUIDownMove();
			if (Mapper.jInputOnRight && UGUIOperationInvalid != true
			|| Mapper.jInputOnRight2p && UGUIOperationInvalid2p != true
			|| Mapper.jInputOnRight3p && UGUIOperationInvalid3p != true
			|| Mapper.jInputOnRight4p && UGUIOperationInvalid4p != true)
				UGUIRightMove();
			if (Mapper.jInputOnLeft && UGUIOperationInvalid != true
			|| Mapper.jInputOnLeft2p && UGUIOperationInvalid2p != true
			|| Mapper.jInputOnLeft3p && UGUIOperationInvalid3p != true
			|| Mapper.jInputOnLeft4p && UGUIOperationInvalid4p != true)
				UGUILeftMove();
			if (Mapper.UGUIOnSubmit && UGUIOperationInvalid != true
			|| Mapper.UGUIOnSubmit2p && UGUIOperationInvalid2p != true
			|| Mapper.UGUIOnSubmit3p && UGUIOperationInvalid3p != true
			|| Mapper.UGUIOnSubmit4p && UGUIOperationInvalid4p != true)
				UGUISubmit();
			if (Mapper.UGUIOnCancel && UGUIOperationInvalid != true
			|| Mapper.UGUIOnCancel2p && UGUIOperationInvalid2p != true
			|| Mapper.UGUIOnCancel3p && UGUIOperationInvalid3p != true
			|| Mapper.UGUIOnCancel4p && UGUIOperationInvalid4p != true)
				UGUICancel();
		}

	}

	void UGUIUpMove()
	{
		m_AxisEventData.moveDir = MoveDirection.Up;
		ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, m_AxisEventData, ExecuteEvents.moveHandler);
	}
	void UGUIDownMove()
	{
		m_AxisEventData.moveDir = MoveDirection.Down;
		ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, m_AxisEventData, ExecuteEvents.moveHandler);
	}
	void UGUIRightMove()
	{
		m_AxisEventData.moveDir = MoveDirection.Right;
		ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, m_AxisEventData, ExecuteEvents.moveHandler);
	}
	void UGUILeftMove()
	{
		m_AxisEventData.moveDir = MoveDirection.Left;
		ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, m_AxisEventData, ExecuteEvents.moveHandler);
	}
	void UGUISubmit()
	{
		ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
	}
	void UGUICancel()
	{
		ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new BaseEventData(EventSystem.current), ExecuteEvents.cancelHandler);
	}

}
