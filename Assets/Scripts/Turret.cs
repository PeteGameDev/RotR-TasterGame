using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject Player;
    public float RotationSpeed = 0.5f;
    public float Range;
    private GameObject FirePoint;
    bool PlayerHit;
    public GameObject projectile;
    private bool CanFire = true;
    public float FireRate;
    private bool HackFix = false;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        FirePoint = transform.Find("FirePoint").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        // Determine which direction to rotate towards
        Vector3 targetDirection = Player.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = RotationSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);



    }

    void FixedUpdate()
    {
        RaycastHit hit;
        PlayerHit = Physics.Raycast(FirePoint.transform.position, FirePoint.transform.TransformDirection(Vector3.forward), out hit, Range);
        if (PlayerHit && hit.collider.tag == "Player"){
            if (!HackFix){
                HackFix = true;
                if (CanFire == true){
                    StartCoroutine(FireTurret());
                }
            }
        }
    }


  IEnumerator FireTurret() {
        CanFire = false;
        GameObject bullet = Instantiate(projectile, FirePoint.transform.position, FirePoint.transform.rotation) as GameObject;
        yield return new WaitForSeconds(FireRate);
        CanFire = true;
        HackFix = false;
    }

}
