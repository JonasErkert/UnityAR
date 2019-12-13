using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInteraction : MonoBehaviour
{
	private GameObject LastFocusedGO;
	private Transform CameraTransform;
	[SerializeField] private LayerMask ArUiMask;

	private void Start()
	{
		CameraTransform = Camera.main.transform;
	}

	void Update()
	{
		var HeadPosition = CameraTransform.position;
		var EyeDirection = CameraTransform.forward;

		RaycastHit hitInfo;
		bool HasRaycastHit = Physics.Raycast(HeadPosition, EyeDirection, out hitInfo, Mathf.Infinity, ArUiMask);
		if (HasRaycastHit)
		{
			hitInfo.collider.GetComponent<InteractionButton>().ARButtonGazeEnter();
			LastFocusedGO = hitInfo.collider.gameObject;
		}
		else if (LastFocusedGO != null)
		{
			LastFocusedGO.GetComponent<InteractionButton>().ARButtonGazeLeave();
			LastFocusedGO = null;
		}
	}
}