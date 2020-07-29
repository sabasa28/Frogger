using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Transform player;
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        if (transform.position.y != player.position.y)
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
    }
}
