using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject resumeMenu;
    public GameObject playerPrefab;
    public int startingFood;
    public int startingHazards;
    public GameObject foodPrefab;
    public GameObject projectilePrefab;
    public GameObject hazardPrefab;
    public Vector3 foodSpawnLocation;
    public GameObject deathmenu;
    public Text scoreText;
    public Text highScoreText;
    private int score;
    private PlayerController playerController;
    private Rigidbody playerRigidBody;
    private GameObject player;
    private Camera mainCamera;
    private bool started;

    public GameObject bodyPiece;
    private List<GameObject> bodyPieceList;

	// Use this for initialization
	void Start () {
        CreatePlayer();
        SpawnFood(startingFood);
        SpawnHazards(startingHazards);
        mainCamera = GameObject.Find("Player(Clone)/Main").GetComponent<Camera>();
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highscore", 0);
        started = false;
        Time.timeScale = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update() {
        if (started == false && Input.anyKeyDown)
        {
            started = true;
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (resumeMenu.activeSelf == false)
            {
                Time.timeScale = 0;
                resumeMenu.SetActive(true);
            }
            else
            {
                ResumeGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (PlayerPrefs.GetInt("upgrade1", 0) == 1)
            {
                mainCamera.enabled = !mainCamera.enabled;
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (PlayerPrefs.GetInt("upgrade2", 0) == 1)
            {
                if (playerController.movementSpeed < 35f)
                {
                    playerController.movementSpeed += 0.02f;
                }
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            if (PlayerPrefs.GetInt("upgrade2", 0) == 1)
            {
                if (playerController.movementSpeed > 5f)
                {
                    playerController.movementSpeed -= 0.02f;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (PlayerPrefs.GetInt("upgrade3", 0) == 1)
            {
                CreateProjectile();
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.M))
            {
                if (Input.GetKey(KeyCode.U))
                {
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        AddPoints(1000);
                    }
                }
            }
        }


    }

    public void CreatePlayer()
    {
        player = Instantiate(playerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);
        bodyPieceList = new List<GameObject>();
        playerController = player.GetComponent<PlayerController>();
        playerRigidBody = player.GetComponent<Rigidbody>();
    }

    public void SpawnFood(int amount)
    {
        for (; amount > 0; amount--)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-foodSpawnLocation.x, foodSpawnLocation.x), foodSpawnLocation.y, Random.Range(-foodSpawnLocation.z, foodSpawnLocation.z));
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(foodPrefab, spawnPosition, spawnRotation);
        }
    }

    public void SpawnHazards(int amount)
    {
        for (; amount > 0; amount--)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-foodSpawnLocation.x, foodSpawnLocation.x), 0.7f, Random.Range(-foodSpawnLocation.z, foodSpawnLocation.z));
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(hazardPrefab, spawnPosition, hazardPrefab.transform.rotation);
        }
    }

    public void SpawnBodyPiece()
    {
        int totalBodyPieces = bodyPieceList.Count;
        GameObject leader;

        if (totalBodyPieces == 0)
        {
            leader = player;
        }
        else
        {
            leader = bodyPieceList[totalBodyPieces - 1];
        }
        bodyPieceList.Add(Instantiate(bodyPiece, leader.transform.position - leader.transform.forward * 0.5f, bodyPiece.transform.rotation));
        bodyPieceList[totalBodyPieces].GetComponent<BodyController>().leader = leader;
    }

    public void AddPoints(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money", 0) + amount);
        UpdateHighScore();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        resumeMenu.SetActive(false);
    }

    public GameObject getBodyPiece(int index)
    {
        if (index > bodyPieceList.Count - 1)
        {
            return null;
        }
        return bodyPieceList[index];
    }

    public void CreateProjectile()
    {
        AddPoints(-1);
        GameObject projectile = Instantiate(projectilePrefab, player.transform.position + player.transform.forward, player.transform.rotation);
        projectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1000);
        StartCoroutine(DestroyObject(projectile, 10));
    }

    IEnumerator DestroyObject(GameObject gameObject, int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Destroy(gameObject);
    }

    private void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", score);
            highScoreText.text = "High Score: " + score;
        }
    }
}
