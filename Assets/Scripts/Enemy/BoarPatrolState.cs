using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void LogicUpdate()
    {

        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }
        if ((!currentEnemy.physicsCheck.isGround) || (currentEnemy.physicsCheck.isTouchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.isTouchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.isWait = true;
            currentEnemy.animator.SetBool("isWalk", false);
        }
        else
        {
            currentEnemy.animator.SetBool("isWalk", true);
        }
    }
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        Debug.Log("onEnter");
    }
    public override void OnExit()
    {
        currentEnemy.animator.SetBool("isWalk", false);
        Debug.Log("Exit");
    }

    public override void PhysicsUpdate()
    {

    }
}



