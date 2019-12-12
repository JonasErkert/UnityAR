using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCursor : MonoBehaviour
{

	private MeshRenderer meshRenderer;
	private Transform mainCamTransform;
	[SerializeField] private LayerMask layerMask;
	
	// Use this for initialization
	void Start ()
	{
		meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
		mainCamTransform = Camera.main.transform;
		
		// Invert the layer mask to detect all but the cursor;
		layerMask = ~layerMask;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 headpose = mainCamTransform.position;
		Vector3 viewdir = mainCamTransform.forward;

		RaycastHit hitInfo;

		if (Physics.Raycast(headpose, viewdir, out hitInfo, 100, layerMask))
		{
			// If a GO is hit the flattened sphere can be drawn
			meshRenderer.enabled = true;
			this.transform.position = hitInfo.point;
			this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
		}
		else
		{
			// If no GO is hit
			meshRenderer.enabled = false;
		}
	}
}
