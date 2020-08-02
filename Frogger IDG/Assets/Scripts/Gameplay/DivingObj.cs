using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivingObj : MonoBehaviour
{
    public Sprite Floating;
    public Sprite Diving;
    public Sprite UnderWater;
    int minTimeToDive = 3;
    int maxTimeToDive = 6;
    int timeDiving = 1;
    int timeUnderWater = 2;
    bool playerOnTop = false;
    SpriteRenderer sr;
    BoxCollider2D coll;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        StartCoroutine(Dive());
    }
    IEnumerator Dive()
    {
        while (true)
        {
            do
            {
                int rand = Random.Range(minTimeToDive, maxTimeToDive);
                yield return new WaitForSeconds(rand);
            } while (playerOnTop);
            sr.sprite = Diving;
            yield return new WaitForSeconds(timeDiving);
            if (!playerOnTop)
            { 
                coll.enabled = false;
                sr.sprite = UnderWater;
                yield return new WaitForSeconds(timeUnderWater);
                coll.enabled = true;
            }
            sr.sprite = Floating;
        }
    }
    private void FixedUpdate()
    {
        playerOnTop = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnTop = true;
        }
    }
}
