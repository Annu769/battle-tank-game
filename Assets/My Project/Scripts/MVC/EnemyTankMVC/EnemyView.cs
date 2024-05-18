using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
namespace BattleTank
{
    public class EnemyView : MonoBehaviour,IDamageable
    {
        private EnemyController enemyController;
        private StateBase currState;
       
        [SerializeField]
        private NavMeshAgent agent;
        [SerializeField] Rigidbody rb;
        [SerializeField] Transform gun;
        [SerializeField] public  Idle idelSate;
        [SerializeField] public ChasingState chasingState;
        [SerializeField] public ShootState shootState;
        [SerializeField] public PatrolingState patrolingState;
        [SerializeField] private Slider enemyHealthBar;
        

        private void Start()
        {
         
            agent = GetComponent<NavMeshAgent>();
            agent.speed = GetEnemySpeed();
            agent.stoppingDistance = 1f;

            ChangeState(idelSate);
        }
        private void Update()
        {
          
            currState.Tick();
        }
        public void SetEnemyController(EnemyController _enemyController)
        {
            enemyController = _enemyController;
        }
        public Rigidbody GetRigidbody()
        {
            return rb;
        }
        public int GetEnemyStrength()
        {
            return enemyController.GetStrenth();
        }
        public void TakeDamage(int damage)
        {
            enemyController.TakeDamage(damage);
        }
        public NavMeshAgent GetAgent()
        {
            return agent;
        }
        public Transform GetGunPosition()
        {
            return gun;
        }
        public Slider GetHealthBar()
        {
            return enemyHealthBar;
        }
        public Transform GetPlayerTransform()
        {
            return enemyController.PlayerTransform();
        }
        public void ChangeState(StateBase newState)
        {
            if(currState != null)
            {
                currState.OnStateExit();
            }
            currState = newState;
            currState.OnStateEnter();

        }
        public void ShootBullet()
        {
            enemyController.Shoot(gun);
        }
        public float GetEnemyVisibilityRange()
        {
            return enemyController.enemyModel.visibilityRange;
        }

        public float GetEnemyRotationSpeed()
        {
            return enemyController.enemyModel.rotationSpeed;
        }

        public int GetEnemyBPM()
        {
            return enemyController.enemyModel.bpm;
        }
        public float GetEnemySpeed()
        {
            return enemyController.enemyModel.speed;
        }

        public float GetEnemyDetectionRange()
        {
            return enemyController.enemyModel.detectionRange;
        }
    }
}