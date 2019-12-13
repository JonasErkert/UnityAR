using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Persistence;

public class SaveRestoreAnchors : MonoBehaviour
{
	public WorldAnchorStore SpatialAnchorStore;
	[SerializeField] private Text DebugCanvasText;
	[SerializeField] private GameObject BoxPrefab;

	private void Awake()
	{
		WorldAnchorStore.GetAsync(StoreLoaded);
	}

	// Use this for initialization
	void Start()
	{
	}

	private void StoreLoaded(WorldAnchorStore store)
	{
		SpatialAnchorStore = store;
		string[] ids = SpatialAnchorStore.GetAllIds();
		for (int index = 0; index < ids.Length; index++)
		{
			GameObject BoxGo = Instantiate(BoxPrefab);
			BoxGo.tag = "Enemy";
			SpatialAnchorStore.Load(ids[index], BoxGo);
		}

		Debug.Log("LOADED " + ids.Length.ToString() + " Anchors " + DebugCanvasText.text);
		// Clear the store on every restart
		SpatialAnchorStore.Clear();
	}

	private void OnDestroy()
	{
		var FoundWorldAnchorObjects = FindObjectsOfType<WorldAnchor>();
		int CountEnemiesWithAnchor = 0;

		for (int i = 0; i < FoundWorldAnchorObjects.Length; i++)
		{
			// Save only enemies attached to an anchor
			if (FoundWorldAnchorObjects[i].gameObject.CompareTag("Enemy"))
			{
				CountEnemiesWithAnchor++;
				bool isOk = SpatialAnchorStore.Save(i.ToString(), FoundWorldAnchorObjects[i]);
				if (isOk)
				{
					DebugCanvasText.text = "\n- STORED: AnchorID " + i.ToString() + " of AnchorGO " +
					                       FoundWorldAnchorObjects[i].gameObject.name + DebugCanvasText.text;
				}
			}

			DebugCanvasText.text = "\n- SAVED: " + CountEnemiesWithAnchor.ToString() + " Anchors " + DebugCanvasText.text;
		}
	}
}