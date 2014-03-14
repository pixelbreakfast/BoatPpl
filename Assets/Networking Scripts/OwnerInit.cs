using uLink;
using UnityEngine;

public class OwnerInit : uLink.MonoBehaviour 
{

	void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo msg) 
	{
		Camera.main.SendMessage("SetTarget", transform);
	}
	
}
