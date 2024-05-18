using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BattleTank
{
    public class ShootState : StateBase
    {
        private Transform playerTransform;
        private Rigidbody rb;
        private NavMeshAgent agent;
        private Vector3 tankLookDirection;

        private float distanceToPlayer;
        private float timeSinceShot;
        private float rotationSpeed;

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            agent = enemyView.GetAgent();
            rb = enemyView.GetRigidbody();
            playerTransform = enemyView.GetPlayerTransform();
            rotationSpeed = enemyView.GetEnemyRotationSpeed();

            agent.ResetPath();
            rb.velocity = Vector3.zero;
            timeSinceShot = 0f;
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

            Attack();
        }

        private void Attack()
        {
           
            distanceToPlayer = Vector3.Distance(rb.transform.position, playerTransform.position);
            tankLookDirection = (playerTransform.position - rb.transform.position).normalized;

            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, Quaternion.LookRotation(tankLookDirection), rotationSpeed);

            if (distanceToPlayer < enemyView.GetEnemyVisibilityRange())
            {
                Debug.Log("Shoot");
                EnemyShoot();
            }
            else
                enemyView.ChangeState(enemyView.chasingState);
        }

        private void EnemyShoot()
        {
           
            timeSinceShot += Time.deltaTime;
            if (timeSinceShot > (20/ enemyView.GetEnemyBPM()))
            {
                Debug.Log("Shoot two");
                enemyView.ShootBullet();
                timeSinceShot = 0;
            }
        }
    }
}
