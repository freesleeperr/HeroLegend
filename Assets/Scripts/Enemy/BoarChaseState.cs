using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaseState
{

    public override void LogicUpdate()
    {
        if (currentEnemy.lostTimeCounter <= 0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        if ((!currentEnemy.physicsCheck.isGround) || (currentEnemy.physicsCheck.isTouchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.isTouchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 1, 1);
        }
    }

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.animator.SetBool("isRun", true);
    }

    public override void OnExit()
    {
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
        currentEnemy.animator.SetBool("isRun", false);
    }

    public override void PhysicsUpdate()
    {

    }

}
