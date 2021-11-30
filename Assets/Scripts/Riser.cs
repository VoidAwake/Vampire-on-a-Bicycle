using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riser : MonoBehaviour
{

    public float riseSpeed;

    void Update()
    {
        if (transform.position.y < transform.localScale.y / 2 - 0.5) {
            transform.Translate(new Vector3(0, riseSpeed, 0));
        } else {
            Destroy(this);
        }
    }
}
