using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using Unity.Collections;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    PhysicsCheck _physicsCheck;
    PlayerAnimation _playerAnimation;

    CapsuleCollider2D _collider;
    public Playerinputcontroll inputControll;
    public Vector2 inputDirection;
    public float speed;
    [Header("Basic")]
    public float jumpForce;
    Rigidbody2D _rigidbody2D;

    [Header("状态")]
    public bool isHurt;
    public float hurtForce;
    public bool isDead;
    public bool isAttack;
    //public int combo;

    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    void Awake()
    {
        inputControll = new Playerinputcontroll();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _physicsCheck = GetComponent<PhysicsCheck>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _collider = GetComponent<CapsuleCollider2D>();
        inputControll.Player.Jump.started += Jump;
        inputControll.Player.Attack.started += PlayAttack;
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
        if (!isHurt && !isAttack)
            Move();
        CheckState();
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
    private void PlayAttack(InputAction.CallbackContext context)
    {
        _playerAnimation.PlayAttack();
        isAttack = true;
        //combo++;
        // if (combo >= 3)
        // {
        //     combo = 0;
        // }
    }



    //UnityEvent中执行
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        _rigidbody2D.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        _rigidbody2D.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }
    public void PlayerDead()
    {
        isDead = true;
        inputControll.Player.Disable();
    }
    public void CheckState()
    {
        _collider.sharedMaterial = _physicsCheck.isGround ? normal : wall;
    }

}
