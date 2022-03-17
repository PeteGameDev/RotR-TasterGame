using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{   
    #region Variables
    //Positions of objects to move between
    public GameObject[] positions;
    
    //Speed of moving object
    public float speed;
    

    private float PosRadius = 1;
    private int current = 0;
    
    #endregion

    //Player moves with platform
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            other.transform.parent = transform;
            Debug.Log("PlayerOn");
        }
    }

    //Player exits platform
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
    private void FixedUpdate() 
    {
        
        if(Vector3.Distance(positions[current].transform.position, transform.position) < PosRadius)
        {
            current++;
            if(current >= positions.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, positions[current].transform.position, Time.deltaTime * speed);
        
    }
    
}
