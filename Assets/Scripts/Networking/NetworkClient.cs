using UnityEngine;
using System.Collections;

public class NetworkClient : MonoBehaviour {


	[RPC]
	public void RebroadcastMessage(string message) {

		Messenger.Broadcast(message, MessengerMode.DONT_REQUIRE_LISTENER);

	}
}
