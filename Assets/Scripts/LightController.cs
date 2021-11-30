using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
    public Light dayLight;
    public float maxLightSpeed;
    public float minLightSpeed;
    public int startAngle;

    public float lightSpeed;

    private PlayerController playerController;

    private float i;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        Restart();
    }

    void Update()
    {
        lightSpeed = (maxLightSpeed - minLightSpeed) * (1 - playerController.speed / playerController.maxSpeed) + minLightSpeed;

        if (transform.rotation.eulerAngles.x < 10 ) {
            if (i < 5) {
                dayLight.intensity = i;
                i += 0.6f;
            } else {
                gameManager.Disable();
            }
        } else if (transform.rotation.eulerAngles.x >= 345 || lightSpeed > 0) {
            transform.Rotate(lightSpeed, 0, 0);
        }
    }

    public void Restart () {
        enabled = true;
        transform.rotation = Quaternion.Euler(startAngle, 0, 0);
        i = 1;
        dayLight.intensity = 0.05f;
    }
}
