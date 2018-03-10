using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("Game Manager").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hazard"))
        {
            Destroy(gameObject);
            GameObject.Find("Game Manager").GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            gameController.SpawnHazards(1);
            gameController.AddPoints(2);
        }
    }
}
