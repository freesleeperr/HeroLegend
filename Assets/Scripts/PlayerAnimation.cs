using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigbody;
    private PhysicsCheck _physicsCheck;
    private PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigbody = GetComponent<Rigidbody2D>();
        _physicsCheck = GetComponent<PhysicsCheck>();
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimator();
    }
    public void SetAnimator()
    {
        _animator.SetFloat("velocityX", Math.Abs(_rigbody.velocity.x));
        _animator.SetFloat("velocityY", _rigbody.velocity.y);
        _animator.SetBool("isGround", _physicsCheck.isGround);
        _animator.SetBool("isDead", _playerController.isDead);
        _animator.SetBool("isAttack", _playerController.isAttack);
    }
    public void PlayHurt()
    {
        _animator.SetTrigger("Hurt");
    }
    public void PlayAttack()
    {
        _animator.SetTrigger("Attack");
    }

}
