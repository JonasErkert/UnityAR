using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArStatus : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		if (UnityEngine.XR.XRDevice.isPresent)
		{
			Debug.Log("INFO: App runs on the Hololens or on a Hololens emulator!");
		}
		else
		{
			Debug.Log("INFO: App doesn't run on the Hololens!");
		}
	}
}
