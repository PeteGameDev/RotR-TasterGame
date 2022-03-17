using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    //general variables
    float m_MaxDistance;
    float f_FloorDistance;
    bool m_HitDetect;

    //handles the raycast
    Collider m_Collider;
    RaycastHit m_Hit;
    RaycastHit m_FirstHit;

    //handles the drop
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float moveTime;
    Vector3 moveBy;
    float transitionTime = 0.5f;
    private bool DropMe = false;
    private bool DropStart = false;

    //handles the shakes

	private Vector3 originPosition;
	private Quaternion originRotation;
	float shake_decay = 0.002f;
	float shake_intensity = .3f;
    private float temp_shake_intensity = 0;


    void Start()
    {
        //Choose the distance the Box can reach to
        m_MaxDistance = 300.0f;
        m_Collider = GetComponent<Collider>();


        //Grab the distance to the floor
        Physics.BoxCast(m_Collider.bounds.center, transform.localScale, Vector3.down, out m_FirstHit, transform.rotation, m_MaxDistance);
        f_FloorDistance = m_FirstHit.distance;
    }

    // Update is called once per frame
    void Update()
    {

		if (temp_shake_intensity > 0){
			transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
				originRotation.y + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
				originRotation.z + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
				originRotation.w + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f);
			temp_shake_intensity -= shake_decay;
		}
        

        if (DropStart == true){
            if (transform.position != targetPosition)
                {
                    moveTime += Time.deltaTime;

                    // Lerp from start to target position. 
                    // Note that "moveTime / transitionTime" will equal 1 when we reach the target, which
                    // is exactly what we want.
                    transform.position = Vector3.Lerp(startPosition, targetPosition, moveTime / transitionTime);
                }
            }
        
    }

    void FixedUpdate()
    {
        //Test to see if there is a hit using a BoxCast
        //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
        //Also fetch the hit data
        m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale, Vector3.down, out m_Hit, transform.rotation, m_MaxDistance);
        if (m_Hit.distance < f_FloorDistance){
            if (DropMe == false){
                StartCoroutine(CanIDrop());
            }
        }
    }

    IEnumerator CanIDrop() {
        DropMe = true;
        Shake();

        yield return new WaitForSeconds(1);

        DropStart = true;
        // Record our starting position so that we can 
        // lerp to the target position smoothly.
        startPosition = transform.position;

        // Add how much we want to move by to the start position.
        // This is where we want to end up.
        moveBy = new Vector3 (0, f_FloorDistance, 0);
        targetPosition = startPosition - moveBy;

        // This tracks how long we've been moving for, in seconds.
        moveTime = 0.0f;
       
    }

    void Shake(){
		originPosition = transform.position;
		originRotation = transform.rotation;
		temp_shake_intensity = shake_intensity;

	}


    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
/*     void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, Vector3.down * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + Vector3.down * m_Hit.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, Vector3.down * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + Vector3.down * m_MaxDistance, transform.localScale);
        }
    } */
}
