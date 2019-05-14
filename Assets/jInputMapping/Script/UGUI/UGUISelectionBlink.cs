using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UGUISelectionBlink : MonoBehaviour
{

	Color SColor;
	Color BaseColor;
	float BlinkTimer;
	float BaseAlpha;

	void Start()
	{
		BaseColor = GetComponent<Image>().color;
		BaseAlpha = GetComponent<Image>().color.a;
		OnEnable();
      }

	void Update()
	{
		GetComponent<Image>().color = SColor;
		BlinkTimer += Time.deltaTime;

		if (0.0f <= BlinkTimer && BlinkTimer < 0.15f)
		{

		} else if (0.15f <= BlinkTimer && BlinkTimer <= 0.4f)
		{
			SColor = new Color(SColor.r, SColor.g, SColor.b, SColor.a - 1.1f * Time.deltaTime);
		} else if (0.4f < BlinkTimer && BlinkTimer < 0.45f)
		{

		} else if (0.45f <= BlinkTimer && BlinkTimer <= 0.95f)
		{
			SColor = new Color(SColor.r, SColor.g, SColor.b, Mathf.Min(BaseAlpha, SColor.a + 0.75f * Time.deltaTime));
		} else
		{
			BlinkTimer = 0;
		}

	}

	void OnEnable()
	{
		SColor = BaseColor;
		BlinkTimer = 0;
	}
}
