using UnityEngine;
using System.Collections;

//Rotates the pickup. That is all
public class Rotator : MonoBehaviour {

	public float x,y,z;

	void Update () {
		transform.Rotate (new Vector3 (x, y, z) * Time.deltaTime);
	}
}
