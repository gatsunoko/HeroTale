﻿using UnityEngine;
using System.Collections;

public class jInputSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;

	public static T Instance {
		get {
			if (instance == null) {
				instance = (T)FindObjectOfType (typeof(T));
					
				if (instance == null) {
					Debug.LogError ("[jInput] Singleton Error!" + typeof(T) + "is Not Found!!");
				}
			}
				
			return instance;
		}
	}
		
}
