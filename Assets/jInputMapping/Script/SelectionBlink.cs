using UnityEngine;
using System.Collections;

public class SelectionBlink : MonoBehaviour
{

		Color SColor;
		Color BaseColor;
		float BlinkTimer;
		float BaseAlpha;

		void Start ()
		{
				SColor = GetComponent<Renderer>().material.color;
				BaseColor = GetComponent<Renderer>().material.color;
				BaseAlpha = GetComponent<Renderer>().material.color.a;
		}
	
		void Update ()
		{
				
				GetComponent<Renderer>().material.SetColor ("_Color", SColor);
				BlinkTimer += Time.deltaTime;

				if (0.0f <= BlinkTimer && BlinkTimer < 0.15f) {
			
				} else if (0.15f <= BlinkTimer && BlinkTimer <= 0.4f) {
						SColor = new Color (SColor.r, SColor.g, SColor.b, SColor.a - 1.1f * Time.deltaTime);
				} else if (0.4f < BlinkTimer && BlinkTimer < 0.45f) {

				} else if (0.45f <= BlinkTimer && BlinkTimer <= 0.95f) {
						SColor = new Color (SColor.r, SColor.g, SColor.b, Mathf.Min (BaseAlpha, SColor.a + 0.75f * Time.deltaTime));
				} else {
						BlinkTimer = 0;
				}

		}

		void OnEnable ()
		{
				SColor = BaseColor;
				BlinkTimer = 0;
		}
}
