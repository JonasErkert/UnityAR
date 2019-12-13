using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.WSA;
using UnityEngine.UI;
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Input;

public
	class GestureControl : MonoBehaviour
{
	public static GestureControl Instance { get; private set; }

	private GestureRecognizer GestureRecognizer;
	[SerializeField] private GameObject BulltetPrefab;
	[SerializeField] private Text DebugCanvasText;
	private Transform MainCamTransform;

	[SerializeField] private LayerMask CursorMask;
	[SerializeField] private LayerMask ArUiMask;
	private LayerMask EverythingButCursorAndArUi;


	void Awake()
	{
		GestureRecognizer = new GestureRecognizer();
		GestureRecognizer.TappedEvent += (source, tapCount, ray) =>
		{
			this.SendMessage("Shoot");
			this.ClickButton();
		};
		
		GestureRecognizer.StartCapturingGestures();
		
		EverythingButCursorAndArUi = (CursorMask ^ ArUiMask);
	}

	void Start()
	{
		DebugCanvasText.text += this.name + " is active!";
		MainCamTransform = Camera.main.transform;
		CursorMask = ~CursorMask;
	}

	void Shoot()
	{
		RaycastHit hitInfo;
		if (Physics.Raycast(MainCamTransform.position, MainCamTransform.forward, out hitInfo, 100, CursorMask))
		{
			GameObject GazedGameObject = hitInfo.collider.gameObject;
			DebugCanvasText.text += "\n - " + GazedGameObject.name;
			if (GazedGameObject.CompareTag("Enemy"))
			{
				DebugCanvasText.text += "\n - Enemy tapped!";
				DestroyImmediate(GazedGameObject.GetComponent<WorldAnchor>());
				// Warnig: Objects with WorldAnchors cannot be Rigidbodys!
				GazedGameObject.AddComponent<Rigidbody>();
			}
			else
			{
				DebugCanvasText.text += "\n - Enemy created and anchored!";
				GameObject Enemy = Instantiate(BulltetPrefab, hitInfo.point + hitInfo.normal * 0.3f,
					Quaternion.identity);
				Enemy.tag = "Enemy";
				Enemy.AddComponent<WorldAnchor>();
			}
		}
	}
	
	void ClickButton()
	{
		RaycastHit hitInfo;
		if (Physics.Raycast(MainCamTransform.position, MainCamTransform.forward, out hitInfo, 100, ArUiMask))
		{
			GameObject GazedGameObject = hitInfo.collider.gameObject;
			GazedGameObject.SendMessage("ARButtonAirTabEnter");
		}
	}
}