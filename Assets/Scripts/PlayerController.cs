using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float movementSpeed;
    public float rotationSpeed;
    public GameObject gameManager;
    private GameObject deathMenu;
    private GameController gameController;

	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("Game Manager").GetComponent<GameController>();
        deathMenu = gameController.deathmenu;
    }
	
	// Update is called once per frame
	void Update () {
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        GetComponent<Rigidbody>().MovePosition(transform.position + (transform.forward * Time.deltaTime * movementSpeed));

        Vector3 rotation = new Vector3(0, moveHorizontal * rotationSpeed, 0);
        transform.Rotate(rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            gameController.SpawnFood(1);
            gameController.SpawnBodyPiece();
            gameController.AddPoints(1);
            GetComponent<AudioSource>().Play();
        }

        if (((other.CompareTag("PlayerBody")) && (other.gameObject != gameController.getBodyPiece(0)) && (other.gameObject != gameController.getBodyPiece(1)) && (other.gameObject != gameController.getBodyPiece(2))) || (other.CompareTag("Hazard")))
        {
            Time.timeScale = 0;
            deathMenu.SetActive(true);
        }
    }

    

}
