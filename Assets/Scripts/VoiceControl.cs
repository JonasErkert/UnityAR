using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.Windows.Speech;

public class VoiceControl : MonoBehaviour
{
	private KeywordRecognizer VoiceRecognition = null;
	private Dictionary<string, System.Action> ActionDictionary = new Dictionary<string, System.Action>();
	[SerializeField] private Text DebugCanvasText;
	[SerializeField] private GameObject Spotlight;

	// Use this for initialization
	void Start()
	{
		DebugCanvasText.text += this.name + " is active!";

		// Voice recognition commands
		ActionDictionary.Add("Test", () => { this.BroadcastMessage("TestSpeak"); });
		ActionDictionary.Add("Light", () => { this.SendMessage("UseLightSwitch"); });

		VoiceRecognition = new KeywordRecognizer(ActionDictionary.Keys.ToArray());
		VoiceRecognition.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
		VoiceRecognition.Start();
	}

	private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
	{
		System.Action KeywordAction;
		if (ActionDictionary.TryGetValue(args.text, out KeywordAction))
		{
			KeywordAction.Invoke();
		}
	}

	private void TestSpeak()
	{
		DebugCanvasText.text += "\n - User said TEST ";
	}
}