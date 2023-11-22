using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class Enemy : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    [HideInInspector] public Animator animator;
    [HideInInspector] public PhysicsCheck physicsCheck;

    [Header("基本参数")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public Transform attacker;
    public float hurtForce;
    public float waitTime;
    public float waitTimeCounter;
    public float lostTime;
    public float lostTimeCounter;
    public bool isWait;
    public bool isHurt;
    public bool isDead;

    private BaseState currentState;
    protected BaseState chaseState;
    protected BaseState patrolState;
    // Start is called before the first frame update


    [Header("检测")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;
    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;

    }
    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }
    private void Start()
    {
        waitTimeCounter = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        // if (physicsCheck.isTouchLeftWall && faceDir.x < 0 || physicsCheck.isTouchRightWall && faceDir.x > 0)
        // {
        //     isWait = true;
        //     animator.SetBool("isWalk", false);
        // }

        currentState.LogicUpdate();
        TimeCounter();
    }
    private void OnDisable()
    {
        currentState.OnExit();
    }
    void FixedUpdate()
    {
        if (!isWait && !isHurt && !isDead)
            Move();
    }

    public virtual void Move()
    {
        _rigidbody2D.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, _rigidbody2D.velocity.y);
    }
    public void TimeCounter()
    {
        if (isWait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                isWait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }
        if (!FoundPlayer() && lostTimeCounter > 0)
        {
            lostTimeCounter -= Time.deltaTime;
        }
        else
        {
            lostTimeCounter = lostTime;
        }
    }
    public bool FoundPlayer()
    {

        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, checkDistance, attackLayer);

    }
    //切换状态
    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }



    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;

        if (attackTrans.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (attackTrans.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);

        }
        //受伤被击退
        isHurt = true;
        animator.SetTrigger("hurt");
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x, 0).normalized;
        _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        StartCoroutine(OnHurt(dir));
    }
    IEnumerator OnHurt(Vector2 dir)
    {
        _rigidbody2D.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isHurt = false;

    }
    public void OnDead()
    {
        gameObject.layer = 2;
        animator.SetBool("dead", true);
        isDead = true;
    }
    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }
    //显示野兽显示范围
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(checkDistance * -transform.localScale.x, 0), 0.2f);
    }
}