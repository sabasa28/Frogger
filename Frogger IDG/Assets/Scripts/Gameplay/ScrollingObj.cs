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
    void Start()
    {
        if (direction == Direction.right) transform.rotation = Quaternion.Euler(0, 0, 180);
    }

    void Update()
    {
        transform.position += -transform.right * Time.deltaTime * speed;
    }
}
