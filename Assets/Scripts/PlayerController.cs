using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.Collections;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    PhysicsCheck _physicsCheck;
    public Playerinputcontroll inputControll;
    public Vector2 inputDirection;
    public float speed;
    [Header("Basic")]
    public float jumpForce;
    Rigidbody2D _rigidbody2D;
    void Awake()
    {
        inputControll = new Playerinputcontroll();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _physicsCheck = GetComponent<PhysicsCheck>();
        inputControll.Player.Jump.started += Jump;
    }
    private void OnEnable()
    {
        inputControll.Enable();
    }
    private void OnDisable()
    {

        inputControll.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        inputDirection = inputControll.Player.Move.ReadValue<Vector2>();

    }
    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        _rigidbody2D.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, _rigidbody2D.velocity.y);
        int faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
        {
            faceDir = 1;
        }
        else if (inputDirection.x < 0)
        {
            faceDir = -1;
        }
        transform.localScale = new Vector3(faceDir, 1, 1);
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (_physicsCheck.isGround)
            _rigidbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}
