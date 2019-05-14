using UnityEngine;
using System.Collections;

public class DemoCube : MonoBehaviour
{
		float speed = 5.0f;
		[HideInInspector]
		public bool
				SomePlayersCheck;

		void Update ()
		{
				//Example by Player1
				var vPositive = jInput.GetAxis (Mapper.InputArray [0]);
				var vNegative = jInput.GetAxis (Mapper.InputArray [1]);
				var v = vPositive - vNegative;

				var hPositive = jInput.GetAxis (Mapper.InputArray [2]);
				var hNegative = jInput.GetAxis (Mapper.InputArray [3]);
				var h = hPositive - hNegative;

				transform.Translate (h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0 * Time.deltaTime, Space.World);

				if (jInput.GetButton (Mapper.InputArray [4])) {
						transform.Rotate (18.0f * Time.deltaTime, 40.0f * Time.deltaTime, 7f * Time.deltaTime, Space.World);
				}
				if (jInput.GetKey (Mapper.InputArray [5])) {
						GetComponent<Renderer>().material.SetColor ("_Color", new Color (0.8f, 0.3f, 0.3f, 1.0f));
				} else {
						GetComponent<Renderer>().material.SetColor ("_Color", new Color (0.6f, 0.7f, 0.7f, 1.0f));
				}
				if (jInput.GetKey (Mapper.InputArray [6])) {
						GetComponent<ParticleSystem>().Play ();
				} else {
						GetComponent<ParticleSystem>().Pause ();
				}
				//Example by Player1 End

				if (SomePlayersCheck) {
						//Example by Player2
						var vPositive2p = jInput.GetAxis (Mapper.InputArray2p [0]);
						var vNegative2p = jInput.GetAxis (Mapper.InputArray2p [1]);
						var v2p = vPositive2p - vNegative2p;
		
						var hPositive2p = jInput.GetAxis (Mapper.InputArray2p [2]);
						var hNegative2p = jInput.GetAxis (Mapper.InputArray2p [3]);
						var h2p = hPositive2p - hNegative2p;
		
						transform.Translate (h2p * speed * Time.deltaTime, v2p * speed * Time.deltaTime, 0 * Time.deltaTime, Space.World);
		
						if (jInput.GetButton (Mapper.InputArray2p [4])) {
								transform.Rotate (18.0f * Time.deltaTime, 40.0f * Time.deltaTime, 7f * Time.deltaTime, Space.World);
						}
						if (jInput.GetKey (Mapper.InputArray [5]) != true) { //Demo forbid briefly that Player1 and player2 operate this in same time
								if (jInput.GetKey (Mapper.InputArray2p [5])) {
										GetComponent<Renderer>().material.SetColor ("_Color", new Color (0.3f, 0.4f, 0.7f, 1.0f));
								} else {
										GetComponent<Renderer>().material.SetColor ("_Color", new Color (0.6f, 0.7f, 0.7f, 1.0f));
								}
						}
						if (jInput.GetKey (Mapper.InputArray [6]) != true) { //Demo forbid briefly that Player1 and player2 operate this in same time
								if (jInput.GetKey (Mapper.InputArray2p [6])) {
										GetComponent<ParticleSystem>().Play ();
								} else {
										GetComponent<ParticleSystem>().Pause ();
								}
						}
						//Example by Player2 End
				}

		}

}
