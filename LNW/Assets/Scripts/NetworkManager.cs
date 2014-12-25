using UnityEngine;
using System.Collections;

public class NetworkManager : Singleton<NetworkManager>
{

		/*	Demo C# Delegation
    public delegate void EnergyTypeClickEventHandler(EnergySelection energyType);
	public static EnergyTypeClickEventHandler EnergyChange; check if null
	EnergyChange(currentEnergyType);
	EnergyTypeClickHandler.EnergyChange += EnergyChange;
	EnergyTypeClickHandler.EnergyChange -= EnergyChange;  */

		public delegate void ThisPlayerSpawnEventHandler (GameObject player);
		public static ThisPlayerSpawnEventHandler ThisPlayerSpawned;
	
		private const string typeName = "LimitlessNewWorlds";
		private const string gameName = PersonalConstants.ROOM_NAME;

		private bool isRefreshingHostList = false;
		private HostData[] hostList;

		public GameObject playerPrefab;
		public GameObject nonplayerPrefab;

		void OnGUI ()
		{
				if (!Network.isClient && !Network.isServer) {
						if (GUI.Button (new Rect (100, 100, 250, 100), "Start Server"))
								StartServer ();

						if (GUI.Button (new Rect (100, 250, 250, 100), "Refresh Hosts"))
								RefreshHostList ();

						if (hostList != null) {
								for (int i = 0; i < hostList.Length; i++) {
										if (GUI.Button (new Rect (400, 100 + (110 * i), 300, 100), hostList [i].gameName))
												JoinServer (hostList [i]);
								}
						}
				}
		}

		private void StartServer ()
		{
				Application.runInBackground = true;
				Network.InitializeServer (5, 25000, !Network.HavePublicAddress ());
				MasterServer.RegisterHost (typeName, gameName);
		}

		void OnServerInitialized ()
		{
				SpawnMainPlayer ();
		}

		void Update ()
		{
				if (isRefreshingHostList && MasterServer.PollHostList ().Length > 0) {
						isRefreshingHostList = false;
						hostList = MasterServer.PollHostList ();
				}
		}

		private void RefreshHostList ()
		{
				if (!isRefreshingHostList) {
						isRefreshingHostList = true;
						MasterServer.RequestHostList (typeName);
				}
		}


		private void JoinServer (HostData hostData)
		{
				Network.Connect (hostData);
		}

		void OnConnectedToServer ()
		{
				SpawnPlayer ();
		}

		private void SpawnMainPlayer ()
		{
				GameObject player = Network.Instantiate (nonplayerPrefab, Vector3.up * 5, Quaternion.identity, 0) as GameObject;
				if (ThisPlayerSpawned != null)
						ThisPlayerSpawned (player);
		}

		private void SpawnPlayer ()
		{
				GameObject player = Network.Instantiate (playerPrefab, Vector3.up * 5, Quaternion.identity, 0) as GameObject;
		}
}
