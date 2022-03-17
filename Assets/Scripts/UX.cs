using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UX : MonoBehaviour
{

    public float speed;

	public GameObject Player;


    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void QuitGame() {
        Application.Quit();
    }

    void FixedUpdate() {
        transform.LookAt (Player.GetComponent<Transform>().position);
		transform.Translate (Vector3.right * Time.deltaTime * speed);
    }
}
