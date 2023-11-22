using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void LogicUpdate()
    {

        if (currentEnemy.FoundPlayer() && currentEnemy.physicsCheck.isGround)
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }
        if (!currentEnemy.physicsCheck.isGround || (currentEnemy.physicsCheck.isTouchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.isTouchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.isWait = true;
            currentEnemy.animator.SetBool("isWalk", false);
        }
        else
        {
            currentEnemy.isWait = false;
            currentEnemy.animator.SetBool("isWalk", true);
        }
    }
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;

    }
    public override void OnExit()
    {
        currentEnemy.animator.SetBool("isWalk", false);

    }

    public override void PhysicsUpdate()
    {

    }
}



