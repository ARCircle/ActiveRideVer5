using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension {
	public static T AddUnknownComponent<T>(this GameObject gameObject) where T : Component
	{
		T result = gameObject.AddComponent<T>() as T;
		return result;
	}
}
