using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButton : MonoBehaviour
{
	private bool Focused = false;
	
	void Start()
	{
		this.GetComponentInChildren<Text>().text = this.gameObject.name;
	}
	
	public void ARButtonGazeEnter()
	{
		this.GetComponent<Button>().OnPointerEnter(null);
		Focused = true;
		Debug.Log("Button " + this.gameObject.name + " hit!");
	}
	
	public void ARButtonGazeLeave()
	{
		this.GetComponent<Button>().OnPointerExit(null);
		Focused = false;
		Debug.Log("Button " + this.gameObject.name + " left!");
	}
	
	public void ARButtonAirTabEnter()
	{
		Debug.Log("Button " + this.gameObject.name + " clicked!");
		if (Focused)
		{
			Text MenuText = GameObject.Find("MenuText").GetComponentInChildren<Text>();
			MenuText.text = this.gameObject.name + " clicked!";
		}
	}
}