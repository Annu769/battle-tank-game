using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BattleTank
{
    public class ChasingState : StateBase
    {
        private Transform playerTransform;
        private NavMeshAgent agent;

        private float playerDetectionRange;

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            playerTransform = enemyView.GetPlayerTransform();
            agent = enemyView.GetAgent();
            playerDetectionRange = enemyView.GetEnemyDetectionRange();

            agent.SetDestination(playerTransform.position);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void Tick()
        {
            base.Tick();

            if (playerTransform == null)
            {
                enemyView.ChangeState(enemyView.idelSate);
                return;
            }

            Chase();
        }

        public void Chase()
        {
            if (agent.remainingDistance > playerDetectionRange)
            {
                enemyView.ChangeState(enemyView.idelSate);
            }
            else if (agent.remainingDistance < enemyView.GetEnemyVisibilityRange())
            {
                enemyView.ChangeState(enemyView.shootState);
            }
            else
            {
                agent.SetDestination(playerTransform.position);
            }
        }
    }
}
