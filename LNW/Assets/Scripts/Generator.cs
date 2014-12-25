﻿using UnityEngine;
using System.Collections;

public class Generator : Singleton<Generator>
{
		private const float chunkOfMapToLoad = 128.0f;
		private const float reloadGranularity = chunkOfMapToLoad / 4.0f;

		private const float maxCubeSize = 64.0f;
		private const float minCubeSize = 8.0f;

		private Vector3 lastLoadLocation;

		private GameObject player;
	
		void Start ()
		{

				DebugUtils.Assert (IsPowerOfTwo ((int)chunkOfMapToLoad));

				if (NetworkManager.ThisPlayerSpawned != null)
						NetworkManager.ThisPlayerSpawned += ThisPlayerSpawned;

				/*		for (float x = (boxSize + gapSize)/2.0f; x < max; x += (boxSize + gapSize)) {
						for (float y = boxSize/2.0f; y < max; y += boxSize) {
								for (float z = (boxSize + gapSize)/2.0f; z < max; z += (boxSize + gapSize)) {

										//GameObject instance = Instantiate (Resources.Load ("enemy", typeof(GameObject)));

										GameObject cube0 = GameObject.CreatePrimitive (PrimitiveType.Cube);
										cube0.transform.localScale = new Vector3 (boxSize, boxSize, boxSize);
										cube0.transform.position = new Vector3 (x, y, z);

										GameObject cube1 = GameObject.CreatePrimitive (PrimitiveType.Cube);
										cube1.transform.localScale = new Vector3 (boxSize, boxSize, boxSize);
										cube1.transform.position = new Vector3 (-x, y, z);

										GameObject cube2 = GameObject.CreatePrimitive (PrimitiveType.Cube);
										cube2.transform.localScale = new Vector3 (boxSize, boxSize, boxSize);
										cube2.transform.position = new Vector3 (x, y, -z);

										GameObject cube3 = GameObject.CreatePrimitive (PrimitiveType.Cube);
										cube3.transform.localScale = new Vector3 (boxSize, boxSize, boxSize);
										cube3.transform.position = new Vector3 (-x, y, -z);
								}	
						}
				} */
		}

		void Update ()
		{
		
		}
	
		void AttemptToLoadObjectAtLocationWithSize (Vector3 location, float size)
		{
				float depth = maxCubeSize / minCubeSize - size / minCubeSize;

				Object resource = Resources.Load ("UserPrefabs/1-1", typeof(GameObject));
				GameObject instance;
		
				if (resource) {
						instance = Instantiate (resource) as GameObject;
						Debug.Log ("SHOCKED FACE");
				}
		}
	
		void UpdateMap ()
		{
				Vector3 loadLocation = player.transform.position;
				int roundNumber = (int)(chunkOfMapToLoad / 2.0f);
				loadLocation.x = (int)loadLocation.x & ~roundNumber;
				loadLocation.y = 0;
				loadLocation.z = (int)loadLocation.z & ~roundNumber;

				AttemptToLoadObjectAtLocationWithSize (new Vector3 ((loadLocation.x - maxCubeSize / 2.0f), 0.0f, (loadLocation.z - maxCubeSize / 2.0f)), maxCubeSize);
				AttemptToLoadObjectAtLocationWithSize (new Vector3 ((loadLocation.x - maxCubeSize / 2.0f), 0.0f, (loadLocation.z + maxCubeSize / 2.0f)), maxCubeSize);
				AttemptToLoadObjectAtLocationWithSize (new Vector3 ((loadLocation.x + maxCubeSize / 2.0f), 0.0f, (loadLocation.z - maxCubeSize / 2.0f)), maxCubeSize);
				AttemptToLoadObjectAtLocationWithSize (new Vector3 ((loadLocation.x + maxCubeSize / 2.0f), 0.0f, (loadLocation.z + maxCubeSize / 2.0f)), maxCubeSize);

				lastLoadLocation = player.transform.position;
		}

		void ThisPlayerSpawned (GameObject spawnedPlayer)
		{
				player = spawnedPlayer;
				UpdateMap ();
		}

		bool IsPowerOfTwo (int x)
		{
				return (x != 0) && ((x & (x - 1)) == 0);
		}

		void OnDestroy ()
		{
				if (NetworkManager.ThisPlayerSpawned != null)
						NetworkManager.ThisPlayerSpawned -= ThisPlayerSpawned;
		}
	
}
