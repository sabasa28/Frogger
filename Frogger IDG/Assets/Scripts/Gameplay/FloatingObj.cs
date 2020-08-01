using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObj : ScrollingObj
{
    Player player;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 translationVec = -transform.right * speed; 
            player.momentum = translationVec;
        }
    }
}
