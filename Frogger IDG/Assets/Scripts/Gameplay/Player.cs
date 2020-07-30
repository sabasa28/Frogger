using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpTime;
    public float minTimeBetweenJumps;
    enum State
    { 
        jumping,
        idle,
        dead
    }
    State frogState = State.idle;
    SpriteRenderer sr;
    public Sprite idleSprite;
    public Sprite jumpingSprite1;
    public Sprite jumpingSprite2;
    Vector3 spawnPosition;
    Quaternion facingUp = Quaternion.Euler(new Vector3(0, 0, 0));
    Quaternion facingDown = Quaternion.Euler(new Vector3(0, 0, 180));
    Quaternion facingRight = Quaternion.Euler(new Vector3(0, 0, -90));
    Quaternion facingLeft = Quaternion.Euler(new Vector3(0, 0, 90));
    Coroutine JumpCoroutine;
    void Start()
    {
        spawnPosition = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        int movementH = (int)Input.GetAxisRaw("Horizontal");
        int movementV = (int)Input.GetAxisRaw("Vertical");
        Vector2 direction = Vector2.zero;
        if (movementH != 0) direction.x = movementH;
        else direction.y = movementV;
        if (frogState == State.idle && direction != Vector2.zero) JumpCoroutine = StartCoroutine(Jump(direction));
    }
    IEnumerator Jump(Vector3 direction)
    {
        sr.sprite = jumpingSprite1;
        frogState = State.jumping;

        if (direction == Vector3.up) transform.rotation = facingUp;
        else if (direction == Vector3.right) transform.rotation = facingRight;
        else if (direction == Vector3.left) transform.rotation = facingLeft;
        else if (direction == Vector3.down) transform.rotation = facingDown;

        Vector3 targetPosition = transform.position + direction;
        Vector3 origPos = transform.position;
        float t = 0;
        while (transform.position != targetPosition)
        {
            UpdateJumpSprite(t);
            t += Time.deltaTime / jumpTime;
            transform.position = Vector3.Lerp(origPos, targetPosition,t);
            
            yield return null;
        }
        sr.sprite = idleSprite;
        yield return new WaitForSeconds(minTimeBetweenJumps);
        frogState = State.idle;
    }

    void UpdateJumpSprite(float t)
    {
        if (CheckMidjump(t))
        {
            if (sr.sprite != jumpingSprite2) sr.sprite = jumpingSprite2;
        }
        else if (sr.sprite != jumpingSprite1) sr.sprite = jumpingSprite1;
    }

    bool CheckMidjump(float t)
    {
        float minForMidJump = 0.3f;
        float maxForMidJump = 0.7f;
        return (t > minForMidJump && t < maxForMidJump);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Car"))
        {
            if (frogState == State.jumping)
            {
                StopCoroutine(JumpCoroutine);
                sr.sprite = idleSprite;
                frogState = State.idle;
            }
            transform.position = spawnPosition;
        }
    }
}
