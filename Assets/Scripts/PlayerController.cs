using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 0.01f;
	public float strafeSpeed = 0.1f;
    public float deacceleration = 0.001f;
    public float maxPlayerTilt;
    public float maxCameraTilt;
    public GameObject playerModel;
    public GameManager gameManager;
    public Text scoreText;
    public float scoreTimer;
    public float acceleration;
    public bool collisions;

    public float speed;

    private int score;
    private int highscore = 0;
    private float elapsed;
    private bool accelerating;

    private void Start() {
        Restart();
    }

    void Update()
    {
        float strafe = Input.GetAxis("Horizontal") * strafeSpeed;

        transform.position = new Vector3(transform.position.x + strafe, transform.position.y, transform.position.z + speed);

        playerModel.transform.rotation = Quaternion.Euler(0, 0, -maxPlayerTilt * Input.GetAxis("Horizontal"));
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, -maxCameraTilt * Input.GetAxis("Horizontal"));

        if (speed > 0) {
            if (accelerating) {
                speed += acceleration;

                if (speed > maxSpeed) accelerating = false;
            } else {
                speed -= deacceleration;
            }
        } else {
            speed = 0;
            gameManager.Disable();
        }

        elapsed += Time.deltaTime;

        if (elapsed >= scoreTimer) {
            elapsed = elapsed % scoreTimer;
            score++;
            updateScoreText();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (collisions && other.tag == "Obstacle") {
            speed = 0;
        } else if (other.tag == "Pickup") {
            accelerating = true;
            Destroy(other.gameObject);
        }
    }

    public void Restart () {
        if (score > highscore) {
            PlayerPrefs.SetInt("highscore", score);
        }

        elapsed = 0;
        score = 0;
        highscore = PlayerPrefs.GetInt("highscore");
        updateScoreText();
        enabled = true;
        speed = maxSpeed;
    }

    private void updateScoreText () {
        scoreText.text = "Score: " + score + "\nBest: " + highscore;
    }
}
