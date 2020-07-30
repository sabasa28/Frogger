using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : ScrollingObj
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
            Vector3 translationVec = -transform.right * Time.deltaTime * speed; 
            player.momentum = translationVec;
        }
    }
}
