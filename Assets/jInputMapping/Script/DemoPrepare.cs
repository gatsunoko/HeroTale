using UnityEngine;
using System.Collections;

public class DemoPrepare : MonoBehaviour
{
	[SerializeField]
	GameObject
			MappingSet;
	jInputSettings jInputSetScript;
	[SerializeField]
	Camera
			DemoCamera;
	Vector3 viewPos;
	Vector3 LimitPosi;

	void Update()
	{
		if (MappingSet != null)
		{
			if (MappingSet.activeSelf)
			{
				gameObject.GetComponent<DemoCube>().enabled = false;
			} else
			{
				gameObject.GetComponent<DemoCube>().enabled = true;
			}
		} else
		{
			gameObject.GetComponent<DemoCube>().enabled = true;
		}

		if (jInputSetScript == null)
		{
			jInputSetScript = MappingSet.GetComponent<jInputSettings>();
		}
		if (jInputSetScript != null)
		{
			if (gameObject.GetComponent<DemoCube>().enabled)
			{
				if (jInputSetScript.PlayerNum > 1)
					GetComponent<DemoCube>().SomePlayersCheck = true;
				else
					GetComponent<DemoCube>().SomePlayersCheck = false;
			}
		}

		viewPos = DemoCamera.WorldToViewportPoint(transform.position);
		if (viewPos.x < 0.07f)
		{
			LimitPosi = DemoCamera.ViewportToWorldPoint(new Vector3(0.08f, viewPos.y, viewPos.z));
			transform.position = LimitPosi;
		} else if (viewPos.x > 0.93f)
		{
			LimitPosi = DemoCamera.ViewportToWorldPoint(new Vector3(0.92f, viewPos.y, viewPos.z));
			transform.position = LimitPosi;
		}
		if (viewPos.y < 0.08f)
		{
			LimitPosi = DemoCamera.ViewportToWorldPoint(new Vector3(viewPos.x, 0.1f, viewPos.z));
			transform.position = LimitPosi;
		} else if (viewPos.y > 0.92f)
		{
			LimitPosi = DemoCamera.ViewportToWorldPoint(new Vector3(viewPos.x, 0.9f, viewPos.z));
			transform.position = LimitPosi;
		}
	}
}
