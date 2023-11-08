using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public Animator animator;


    [Header("基本参数")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
    }
    void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        rigidbody2D.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rigidbody2D.velocity.y);
    }

}
