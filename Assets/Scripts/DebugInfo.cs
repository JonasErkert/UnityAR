using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugInfo : MonoBehaviour
{
	[SerializeField] private GameObject debugCanvas;
	[SerializeField] private Text debugCanvasText;
	
	// Use this for initialization
	void Start ()
	{
		debugCanvas.SetActive(true);
		debugCanvasText.text = name + " is active!";
	}
}
