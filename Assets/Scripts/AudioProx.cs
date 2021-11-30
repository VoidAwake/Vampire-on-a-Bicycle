using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioProx : MonoBehaviour
{
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Obstacle") {
            source.Play();
        }
    }
}
