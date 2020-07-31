using UnityEngine;
using System;

/// <summary>
/// Player Controller class to control Player prefab
/// Holds keycodes of all keys that are used in player movement and control
/// Allows to change movement and rotation speed of a player in Inspector
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public KeyCode keyMoveForward;
    public KeyCode keyMoveReverse;
    public KeyCode keyRotateRight;
    public KeyCode keyRotateLeft;
    public KeyCode keyFire;
    public KeyCode keyPrevWeapon;
    public KeyCode keyNextWeapon;
    private Rigidbody2D rb;
    private Vector2 velocity;
    private bool moveForward = false;
    private bool moveReverse = false;
    private float moveSpeed = 0f;
    private float moveSpeedReverse = 0f;
    private float moveAcceleration = 0.1f;
    private float moveDeceleration = 0.20f;
    public float moveSpeedMax = 2.5f;

    private bool rotateRight = false;
    private bool rotateLeft = false;
    private float rotateSpeedRight = 0f;
    private float rotateSpeedLeft = 0f;
    private float rotateAcceleration = 4f;
    private float rotateDeceleration = 10f;
    public float rotateSpeedMax = 130f;

    private bool firePressed;
    private bool nextWeaponPressed;
    private bool prevWeaponPressed;

    public Action onMoving;
    public Action onStopMoving;
    public Action onAttack;
    public Action onNextWeapon;
    public Action onPrevWeapon;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rotateLeft = (Input.GetKey(keyRotateLeft));
        if (rotateLeft)
        {
            rotateSpeedLeft = (rotateSpeedLeft < rotateSpeedMax) ? rotateSpeedLeft + rotateAcceleration : rotateSpeedMax; 
        } 
        else 
        { 
            rotateSpeedLeft = (rotateSpeedLeft > 0) ? rotateSpeedLeft - rotateDeceleration : 0;
        }

        //rotate right
        rotateRight = (Input.GetKey(keyRotateRight));
        if (rotateRight)
        {
            rotateSpeedRight = (rotateSpeedRight < rotateSpeedMax) ? rotateSpeedRight + rotateAcceleration : rotateSpeedMax; 
        } 
        else 
        {
            rotateSpeedRight = (rotateSpeedRight > 0) ? rotateSpeedRight - rotateDeceleration : 0;
        }

        //translate forward
        moveForward = (Input.GetKey(keyMoveForward));
        if (moveForward)
        {
            moveSpeed = (moveSpeed < moveSpeedMax) ? moveSpeed + moveAcceleration : moveSpeedMax; 
        } 
        else 
        { 
            moveSpeed = (moveSpeed > 0) ? moveSpeed - moveDeceleration : 0;
        }

        //translate backward
        moveReverse = (Input.GetKey(keyMoveReverse));
        if (moveReverse)
        {
            moveSpeedReverse = (moveSpeedReverse < moveSpeedMax) ? moveSpeedReverse + moveAcceleration : moveSpeedMax; 
        } 
        else 
        { 
            moveSpeedReverse = (moveSpeedReverse > 0) ? moveSpeedReverse - moveDeceleration : 0;
        }

        prevWeaponPressed = (Input.GetKeyDown(keyPrevWeapon));
        nextWeaponPressed = (Input.GetKeyDown(keyNextWeapon));

        if (moveForward | moveReverse | rotateRight | rotateLeft)
        {
            onMoving();
        }
        else
        {
            onStopMoving();
        }

        //fire
        firePressed = (Input.GetKey(keyFire));
        if(firePressed)
        {
            onAttack();
        }

        if(nextWeaponPressed)
        {
            onNextWeapon();
        }
        if(prevWeaponPressed)
        {
            onPrevWeapon();
        }
    }

    void FixedUpdate()
    {
        if(rotateLeft)
            transform.Rotate(0f, 0f, rotateSpeedLeft * Time.fixedDeltaTime);
        else if(rotateRight)
            transform.Rotate(0f, 0f, rotateSpeedRight * Time.fixedDeltaTime * -1f);

        if(moveReverse)
            rb.MovePosition((Vector2)transform.position + (Vector2)(transform.rotation * (new Vector2(0, -moveSpeedReverse))) * Time.fixedDeltaTime);
        else if(moveForward)
            rb.MovePosition((Vector2)transform.position + (Vector2)(transform.rotation * (new Vector2(0, moveSpeed))) * Time.fixedDeltaTime);
    }
}
