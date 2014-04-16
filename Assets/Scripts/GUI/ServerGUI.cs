using UnityEngine;
using System.Collections;

public class ServerGUI : MonoBehaviour {
	public bool inputName = true;
	string externalIP = "unknown";

	public UnityEngine.MonoBehaviour[] enableWhenGUI;
	public UnityEngine.MonoBehaviour[] disableWhenGUI;

	public int guiDepth = 0;

	private const float WIDTH = 220;

	public bool lockCursor = true;
	public bool hideCursor = true;

	IEnumerator CheckIP()
	{
		Debug.Log ("ip checking...");
		WWW www = new WWW("http://checkip.dyndns.org");

		yield return www;
		Debug.Log ("ip got!");
		string externalIP = www.text;
		externalIP=externalIP.Substring(externalIP.IndexOf(":")+1);
		externalIP=externalIP.Substring(0,externalIP.IndexOf("<"));

	}

	void Start() {
		Debug.Log ("Starting");
		CheckIP();
	}

	void OnGUI()
	{
		GUI.depth = guiDepth;
		
		if (uLink.Network.lastError == uLink.NetworkConnectionError.NoError && uLink.Network.status == uLink.NetworkStatus.Connected && uLink.NetworkView.FindByOwner(uLink.Network.player).Length != 0 && (!lockCursor || Screen.lockCursor))
		{
			EnableGUI(false);
			return;
		}
		
		EnableGUI(true);

	GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
	GUILayout.BeginHorizontal();
	GUILayout.FlexibleSpace();
	GUILayout.BeginVertical();
	GUILayout.FlexibleSpace();

	GUILayout.BeginVertical("Box", GUILayout.Width(WIDTH));

	GUILayout.BeginHorizontal();
	GUILayout.FlexibleSpace();
		GUILayout.Label("Your IP address is " + externalIP);
	GUILayout.FlexibleSpace();
	GUILayout.EndHorizontal();

	GUILayout.BeginHorizontal();
	GUILayout.FlexibleSpace();



	GUILayout.FlexibleSpace();
	GUILayout.EndHorizontal();

	GUILayout.BeginVertical();
	GUILayout.Space(2);
	GUILayout.EndVertical();
	GUILayout.EndVertical();


	GUILayout.FlexibleSpace();
	GUILayout.EndVertical();
	GUILayout.FlexibleSpace();
	GUILayout.EndHorizontal();
	GUILayout.EndArea();
	}

	
	void EnableGUI(bool enabled)
	{
		if (lockCursor) Screen.lockCursor = !enabled;
		if (hideCursor) Screen.showCursor = enabled;
		
		foreach (UnityEngine.MonoBehaviour component in enableWhenGUI)
		{
			component.enabled = enabled;
		}
		
		foreach (UnityEngine.MonoBehaviour component in disableWhenGUI)
		{
			component.enabled = !enabled;
		}
	}
}
