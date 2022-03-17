using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{

    [Header("Collectable to collect")]
    public int TotalCollectables;
    public int CurrentCollectables;


    [Header("DO NOT CHANGE THE STUFF BELOW")]
    public GameObject CollectablesCount;
    public GameObject StartMessage;
    public GameObject EndMessage;
    public Text CurrentTotal;
    public Text EndTotal;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(StartMessage, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTotal.text = "Collected : " + CurrentCollectables;
    }



    public void EndLevel(){
        EndMessage.SetActive (true);
        EndTotal.text = CurrentCollectables + " items out of " + TotalCollectables;
    }

    public void EndButton () {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void AddCollectable() {
        CurrentCollectables++;
    }
}
