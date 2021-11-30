using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject pickup;
    public int numberOfObstacles;
    public int numberOfPickups;
    public int zDensity;
    public int generationRange;
    public int maxXZScale;
    public int maxYScale;
    public int minXZScale;
    public int minYScale;

    private float lastZPosition;
    private float zDistance;

    private void Start() {
        Restart();
    }

    private void Update()
    {
        zDistance += transform.position.z - lastZPosition;

        if (zDistance > zDensity) {
            zDistance = zDistance % zDensity;

            generate();
        }

        lastZPosition = transform.position.z;
    }

    private void generate () {
        for (int i = 0; i < numberOfObstacles; i++) {
            float randScaleXZ = Random.Range(minXZScale, maxXZScale);
            float randScaleY = Random.Range(minYScale, maxYScale);

            float randX = Random.Range(-generationRange, generationRange);
            GameObject newObstacle = Instantiate(obstacle, new Vector3(
                gameObject.transform.position.x + randX,
                -randScaleY / 2 - 0.5f,
                gameObject.transform.position.z
            ), Quaternion.identity);

            newObstacle.transform.localScale = new Vector3(
                randScaleXZ,
                randScaleY,
                randScaleXZ
            );
        }

        for (int i = 0; i < numberOfPickups; i++) {
            float randX = Random.Range(-generationRange, generationRange);
            GameObject newObstacle = Instantiate(pickup, new Vector3(
                gameObject.transform.position.x + randX,
                0,
                gameObject.transform.position.z
            ), Quaternion.identity);
        }
    }

    public void Restart () {
        enabled = true;

        lastZPosition = 0;
        zDistance = 0;

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles) {
            Destroy(obstacle);
        }

        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

        foreach (GameObject pickup in pickups) {
            Destroy(pickup);
        }
    }
}
