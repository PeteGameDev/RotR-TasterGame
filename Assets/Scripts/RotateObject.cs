using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Start is called before the first frame update
	public float xDirection;
    public float yDirection;
    public float zDirection;
    public float Speed;

	void Update () {
		transform.Rotate (new Vector3 (xDirection, yDirection, zDirection) * Time.deltaTime * Speed);
	}
}
