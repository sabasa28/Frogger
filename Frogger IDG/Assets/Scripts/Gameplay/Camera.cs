using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Transform player;
    public float offset;
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        if (transform.position.y+ offset != player.position.y)
            transform.position = new Vector3(transform.position.x, player.position.y + offset, transform.position.z);
    }
}
