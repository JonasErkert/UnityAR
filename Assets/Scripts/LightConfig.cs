using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightConfig : MonoBehaviour
{
	public void UseLightSwitch()
	{
		bool IsEnabled = this.GetComponent<Light>().enabled;
		
		this.GetComponent<Light>().enabled = !IsEnabled;
		Debug.Log("UseLightSwitch " + !IsEnabled);
	}
}
