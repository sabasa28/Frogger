using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Player : MonoBehaviour
{
    public float jumpTime;
    public float minTimeBetweenJumps;
    public enum State
    { 
        midjump,
        landing,
        idle,
        dead
    }
    public State frogState;
    SpriteRenderer sr;
    public Sprite idleSprite;
    public Sprite jumpingSprite1;
    public Sprite jumpingSprite2;
    public Vector3 momentum;
    public Action SwitchPauseState;
    public Action<int> UpdateHUD;
    public int lives;
    Vector3 spawnPosition;
    bool onWater = false;
    bool onFloatingPlatform = false;
    bool fellOnWater = false;
    [HideInInspector]
    public Vector3 jumpOrigPos;
    [HideInInspector]
    public Vector3 jumpTargetPosition;
    [HideInInspector]
    public float minX;
    [HideInInspector]
    public float maxX;
    [HideInInspector]
    public float minY;
    Quaternion facingUp = Quaternion.Euler(new Vector3(0, 0, 0));
    Quaternion facingDown = Quaternion.Euler(new Vector3(0, 0, 180));
    Quaternion facingRight = Quaternion.Euler(new Vector3(0, 0, -90));
    Quaternion facingLeft = Quaternion.Euler(new Vector3(0, 0, 90));
    Coroutine JumpCoroutine;
    void Start()
    {
        frogState = State.idle;
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
        if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.P))
        {
            SwitchPauseState();
        }
    }
    private void FixedUpdate()
    {
        onWater = false;
        onFloatingPlatform = false; //lo seteo false y por orden de lectura luego se comprueba si es true en el ontriggerstay
        if (frogState != State.midjump)
        {
            transform.position += momentum * Time.deltaTime;
        }
    }
    IEnumerator Jump(Vector3 direction)
    {

        if (direction == Vector3.up) transform.rotation = facingUp;
        else if (direction == Vector3.right) transform.rotation = facingRight;
        else if (direction == Vector3.left) transform.rotation = facingLeft;
        else if (direction == Vector3.down) transform.rotation = facingDown;

        jumpTargetPosition = transform.position + direction;
        jumpOrigPos = transform.position;

        if (jumpTargetPosition.x < minX || jumpTargetPosition.x > maxX || jumpTargetPosition.y < minY)
        {
            yield break;
        }
        sr.sprite = jumpingSprite1;
        frogState = State.midjump;

        Vector3 momentumAtTakeOff = momentum;
        float t = 0;
        while (transform.position != jumpTargetPosition)
        {
            UpdateJumpSprite(t);
            t += Time.deltaTime / jumpTime;
            if (momentum != Vector3.zero)
            {
                jumpOrigPos += momentumAtTakeOff * Time.deltaTime;
                jumpTargetPosition += momentumAtTakeOff * Time.deltaTime;
            }
            if (jumpTargetPosition.x < minX) jumpTargetPosition = new Vector3(minX, jumpTargetPosition.y, jumpTargetPosition.z);
            if (jumpTargetPosition.x > maxX) jumpTargetPosition = new Vector3(maxX, jumpTargetPosition.y, jumpTargetPosition.z);
            transform.position = Vector3.Lerp(jumpOrigPos, jumpTargetPosition,t);
            yield return null;
        }
        sr.sprite = idleSprite;
        StartCoroutine(ResetMomentum());
        frogState = State.landing;
        yield return new WaitForSeconds(minTimeBetweenJumps);
        frogState = State.idle;
        OnSinking();
    }
    IEnumerator ResetMomentum()
    {
        yield return new WaitForFixedUpdate();
        momentum = Vector3.zero;
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
            Die();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            onWater = true;
        }
        if (collision.CompareTag("FloatingPlatform"))
        {
            if (OutOfXLimits()) Die();
            onFloatingPlatform = true;
        }
    }
    void OnSinking()
    {
        if (onWater && !onFloatingPlatform) Die();
    }

    void ReturnToSpawnPoint()
    {
        if (frogState == State.midjump)
        {
            StopCoroutine(JumpCoroutine);
            sr.sprite = idleSprite;
            frogState = State.idle;
        }
        StartCoroutine(ResetMomentum());
        transform.rotation = Quaternion.identity;
        transform.position = spawnPosition;
    }

    void Die()
    {
        lives -= 1;
        UpdateHUD(lives);
        ReturnToSpawnPoint();
    }

    bool OutOfXLimits()
    {
        return (transform.position.x < minX || transform.position.x > maxX);
    }
}
