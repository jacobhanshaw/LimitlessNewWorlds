using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour
{
		private const float max = 25.0f;
		private const float boxSize = 8.0f;
		private const float gapSize = 8.0f;


		// Use this for initialization
		void Start ()
		{

				for (float x = (boxSize + gapSize)/2.0f; x < max; x += (boxSize + gapSize)) {
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
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
