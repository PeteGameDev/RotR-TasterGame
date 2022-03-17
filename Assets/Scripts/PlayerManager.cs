using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private GameObject LevelManager;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager = GameObject.Find("LevelManager");
    }

    // Update is called once per frame
    void Update()
    {

        //KILL Z
        if (transform.position.y < 0) {
            KillPlayer();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dropper") {
            KillPlayer();
        }

        if (other.tag == "Projectile") {
            KillPlayer();
        }

        if (other.tag == "Finish") {
            LevelManager.GetComponent<Level_Manager>().EndLevel();
        }

        if (other.tag == "Collectable"){
            LevelManager.GetComponent<Level_Manager>().AddCollectable();
            Destroy(other.gameObject);
        }
    }


    void KillPlayer() {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }


}
