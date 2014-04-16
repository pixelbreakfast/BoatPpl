// (c)2011 Unity Park. All Rights Reserved.

using UnityEngine;
using uLink;

[AddComponentMenu("uLink Utilities/Client GUI")]
public class ClientGUI : uLink.MonoBehaviour
{
	public bool inputName = true;
	
	string labelText = "Join Game";
	string[] hostArray = new string[4];
	string host = "120.148.2.185";
	string port = "7100";

	public bool showGameLevel = false;

	public UnityEngine.MonoBehaviour[] enableWhenGUI;
	public UnityEngine.MonoBehaviour[] disableWhenGUI;


	public bool reloadOnDisconnect = false;
	
	public int targetFrameRate = 60;
	
	public int guiDepth = 0;

	private bool showConnect = true;

	private string playerName;
	
	private const float WIDTH = 220;
	
	private bool isRedirected = false;
	
	public bool dontDestroyOnLoad = false;
	
	public bool lockCursor = true;
	public bool hideCursor = true;
	
	void Awake()
	{
		#if !UNITY_2_6 && !UNITY_2_6_1
		if (Application.webSecurityEnabled)
		{
			Security.PrefetchSocketPolicy(uLink.NetworkUtility.ResolveAddress(host).ToString(), 843);
			Security.PrefetchSocketPolicy(uLink.MasterServer.ipAddress, 843);
		}
		#endif
		
		Application.targetFrameRate = targetFrameRate;
		
		if (dontDestroyOnLoad) DontDestroyOnLoad(this);
		
		playerName = PlayerPrefs.GetString("playerName", "Guest" + Random.Range(1, 100));
		hostArray[0] = PlayerPrefs.GetString("hostArray0","");
		hostArray[1] = PlayerPrefs.GetString("hostArray1","");
		hostArray[2] = PlayerPrefs.GetString("hostArray2","");
		hostArray[3] = PlayerPrefs.GetString("hostArray3","");

	}
	
	void OnDisable()
	{
		PlayerPrefs.SetString("playerName", playerName);
		PlayerPrefs.SetString("hostArray0",hostArray[0]);
		PlayerPrefs.SetString("hostArray1",hostArray[1]);
		PlayerPrefs.SetString("hostArray2",hostArray[2]);
		PlayerPrefs.SetString("hostArray3",hostArray[3]);

	}
	
	void OnGUI()
	{
		GUI.depth = guiDepth;
	


		if(showConnect) {
			DisplayJoin();
		}
		if (uLink.Network.status == uLink.NetworkStatus.Connecting && !isRedirected)
		{
			DisplayConnecting();
		}


	}

	void DisplayJoin () {
		GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();
		
		GUILayout.BeginVertical("Box", GUILayout.Width(WIDTH));

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label("Please enter your name:");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
		GUILayout.BeginVertical();
		GUILayout.Space(5);
		GUILayout.EndVertical();
		
		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		playerName = GUILayout.TextField(playerName, GUILayout.MinWidth(80));
		GUILayout.Space(10);
		GUILayout.EndHorizontal();
		
		GUILayout.BeginVertical();
		GUILayout.Space(5);
		GUILayout.EndVertical();
		
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label("Enter server IP address:");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
		GUILayout.BeginVertical();
		GUILayout.Space(5);
		GUILayout.EndVertical();
		
		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		hostArray[0] = GUILayout.TextField(hostArray[0], GUILayout.MinWidth(40), GUILayout.MaxWidth(40));
		GUILayout.Label(".");
		hostArray[1] = GUILayout.TextField(hostArray[1], GUILayout.MinWidth(40), GUILayout.MaxWidth(40));
		GUILayout.Label(".");
		hostArray[2] = GUILayout.TextField(hostArray[2], GUILayout.MinWidth(40), GUILayout.MaxWidth(40));
		GUILayout.Label(".");
		hostArray[3] = GUILayout.TextField(hostArray[3], GUILayout.MinWidth(40), GUILayout.MaxWidth(40));
		GUILayout.Label(":");
		port = GUILayout.TextField(port, GUILayout.MinWidth(60), GUILayout.MaxWidth(60));
		GUILayout.EndHorizontal();
		
		GUILayout.BeginVertical();
		GUILayout.Space(5);
		GUILayout.EndVertical();
		
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		
		if (GUILayout.Button(labelText, GUILayout.Width(120), GUILayout.Height(25)))
		{	
			showConnect = false;
			for(int i = 0; i < hostArray.Length; i++) {
				
				if(hostArray[i] == null || hostArray[i] == "") 
				{
					hostArray[i] = "0";
				}
			}
			
			host = hostArray[0] + "." + hostArray[1] + "." + hostArray[2] + "." + hostArray[3];
			
			Connect(host, int.Parse(port));
		}
		
		
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

	void DisplayConnecting() {

		GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();
		
		GUILayout.BeginVertical("Box", GUILayout.Width(WIDTH));

		GUILayout.BeginVertical();
		GUILayout.Space(5);
		GUILayout.EndVertical();

		GUILayout.BeginVertical();
		GUILayout.Space(5);
		GUILayout.EndVertical();
		
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		
		GUILayout.Label("Connecting...");

		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
			
			if (GUILayout.Button("Cancel", GUILayout.Width(80), GUILayout.Height(25)))
			{
				showConnect = true;
				uLink.Network.DisconnectImmediate();
			}
			
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();

		
		GUILayout.BeginVertical();
		GUILayout.Space(5);
		GUILayout.EndVertical();

		
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	void BusyGUI()
	{
		GUILayout.BeginVertical();
		GUILayout.Space(5);
		GUILayout.EndVertical();
		
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		
		string busyDoingWhat = "Busy...";
		
		if (uLink.Network.lastError != uLink.NetworkConnectionError.NoError)
		{
			busyDoingWhat = "Error: " + uLink.NetworkUtility.GetErrorString(uLink.Network.lastError);
		}
		else if (uLink.Network.status == uLink.NetworkStatus.Connected)
		{
			if (uLink.NetworkView.FindByOwner(uLink.Network.player).Length != 0)
			{
				if (lockCursor)
				{
					busyDoingWhat = "Click to start playing";
					
					if (Input.GetMouseButton(0)) Screen.lockCursor = true;
				}
			}
			else
			{
				busyDoingWhat = "Instantiating..."; 
			}
		}
		else if (uLink.Network.status == uLink.NetworkStatus.Connecting)
		{
			string prefix = isRedirected ? "Redirecting to " : "Connecting to ";
			busyDoingWhat = prefix + uLink.NetworkPlayer.server.endpoint;
		}
		else if (uLink.Network.status == uLink.NetworkStatus.Disconnecting)
		{
			busyDoingWhat = "Disconnecting";
		}
		
		GUILayout.Label(busyDoingWhat);
		
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
		if (uLink.Network.status == uLink.NetworkStatus.Connecting && !isRedirected)
		{
			GUILayout.BeginVertical();
			GUILayout.Space(5);
			GUILayout.EndVertical();
			
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			
			if (GUILayout.Button("Cancel", GUILayout.Width(80), GUILayout.Height(25)))
			{
				uLink.Network.DisconnectImmediate();
			}
			
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}
		
		GUILayout.BeginVertical();
		GUILayout.Space(5);
		GUILayout.EndVertical();
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
	
	void Connect(string host, int port)
	{
		isRedirected = false;
		
		if (inputName)
		{
			uLink.Network.Connect(host, port, "", playerName);
		}
		else
		{
			uLink.Network.Connect(host, port);
		}
	}
	
	void Connect(uLink.HostData host)
	{
		isRedirected = false;
		
		if (inputName)
		{
			uLink.Network.Connect(host, "", playerName);
		}
		else
		{
			uLink.Network.Connect(host);
		}
	}
	
	void uLink_OnRedirectingToServer()
	{
		isRedirected = true;
		EnableGUI(true);
	}
	
	void uLink_OnDisconnectedFromServer(uLink.NetworkDisconnection mode)
	{

	
		if (reloadOnDisconnect && mode != uLink.NetworkDisconnection.Redirecting && Application.loadedLevel != -1)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}

