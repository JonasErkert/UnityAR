using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagAlong : MonoBehaviour
{
	[SerializeField] private float MaxDistance = 2.0f;
	[SerializeField] private float FOVPercent = 0.9f;
	[SerializeField] private float Velocity = 1.0f;

	private float FOV = 0.0f;
	private float StartTime;
	private float DistanceStartEnd = 1.0f;
	private Vector3 OldPosition;
	private Vector3 NewPosition;

	private Camera MainCamera;

	private bool MenuIsMoving = false;

	// Use this for initialization
	void Start()
	{
		MainCamera = Camera.main;
		FOV = MainCamera.fieldOfView;
		OldPosition = NewPosition = MainCamera.transform.position + MaxDistance * MainCamera.transform.forward;
		DistanceStartEnd = Vector3.Distance(OldPosition, NewPosition);
	}

	// Update is called once per frame
	void Update()
	{
		var currentPosition = MainCamera.transform.position;
		var currentViewDir = MainCamera.transform.forward;
		Vector3 HeadToMenu = (this.transform.position - currentPosition).normalized;
		float Angle = Vector3.Angle(currentViewDir, HeadToMenu);
		if (!MenuIsMoving && Angle > FOV * FOVPercent)
		{
			//Debug.Log("Angle was = " + Angle + " FOV: " + FOV);
			//Debug.Log("Menu not in view.");
			NewPosition = currentPosition + currentViewDir * MaxDistance;
			StartTime = Time.time;
			DistanceStartEnd = Vector3.Distance(OldPosition, NewPosition);
			MenuIsMoving = true;
		}

		if (MenuIsMoving)
		{
			float CoveredDistance = (Time.time - StartTime) * Velocity;
			float Movement = CoveredDistance / DistanceStartEnd;
			
			this.transform.position = Vector3.Lerp(OldPosition, NewPosition, Movement);
			this.transform.rotation = Quaternion.FromToRotation(Vector3.forward, currentViewDir);
			
			if (Vector3.Distance(this.transform.position, NewPosition) < 0.01f)
			{
				//Debug.Log("Menü aus dem Bild");
				MenuIsMoving = false;
				OldPosition = NewPosition;
			}
		}
	}
}
