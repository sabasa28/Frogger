using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObj : MonoBehaviour
{
    public enum Direction
    {
        left,
        right
    };
    public Direction direction;
    public float speed;
    [HideInInspector]
    public float minX;
    [HideInInspector]
    public float maxX;
    float offsetForTranslation = 10;
    void Start()
    {
        if (direction == Direction.right) transform.rotation = Quaternion.Euler(0, 0, 180);
    }

    void FixedUpdate()
    {
        transform.position += -transform.right * Time.deltaTime * speed;
        CheckLevelLimits();
    }
    void CheckLevelLimits()
    {
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(maxX - offsetForTranslation, transform.position.y, transform.position.z);
        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(minX + offsetForTranslation, transform.position.y, transform.position.z);
        }
    }
}
