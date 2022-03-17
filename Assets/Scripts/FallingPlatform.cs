using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    //handles the drop
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float moveTime;
    Vector3 moveBy;
    float transitionTime = 0.5f;
    private bool DropStart = false;

    //handles the shakes

	private Vector3 originPosition;
	private Quaternion originRotation;
	float shake_decay = 0.001f;
	float shake_intensity = .1f;
    private float temp_shake_intensity = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            StartCoroutine(DropThePlatform());
        }
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

            if (transform.position == targetPosition) {
                Destroy(this.gameObject);
            }

        }

        

    }

    IEnumerator DropThePlatform() {
        Shake();

        yield return new WaitForSeconds(1);

        DropStart = true;
        // Record our starting position so that we can 
        // lerp to the target position smoothly.
        startPosition = transform.position;

        // Add how much we want to move by to the start position.
        // This is where we want to end up.
        moveBy = new Vector3 (0, 10f, 0);
        targetPosition = startPosition - moveBy;

        // This tracks how long we've been moving for, in seconds.
        moveTime = 0.0f;
    }

    void Shake(){
		originPosition = transform.position;
		originRotation = transform.rotation;
		temp_shake_intensity = shake_intensity;

	}



}
