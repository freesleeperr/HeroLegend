using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public override void Move()
    {
        base.Move();
        animator.SetBool("isWalk", true);
    }
    // Update is called once per frame
}
