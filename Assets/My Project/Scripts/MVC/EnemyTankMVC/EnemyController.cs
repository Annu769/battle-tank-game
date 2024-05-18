using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
namespace BattleTank
{
    public class EnemyController
    {
        public EnemyView enemyView { get; }
        public EnemyModel enemyModel { get; }
        private StateBase enemyState;
        private NavMeshAgent agent;
        public Vector3 spawnPoint;
        private Rigidbody rb;
        private float health;
        private float shootCooldown;
        private Slider _healthBar;
        private Camera playerCamera;
        public Transform GetEnemyGunPosition() => enemyView.GetGunPosition();
        public Transform GetEnemyTankTransform() => enemyView.transform;
        public EnemyController(EnemyScriptableObject enemy, float x = 0, float z = 0)
        {
            enemyView = GameObject.Instantiate<EnemyView>(enemy.enemyView, new Vector3(Random.Range(x, -x), 0, Random.Range(z, -z)), Quaternion.identity);
            enemyModel = new EnemyModel(enemy);
            enemyView.SetEnemyController(this);
            enemyModel.SetEnemyController(this);
            enemyState = new Idle();
            rb = enemyView.GetRigidbody();
            agent = enemyView.GetAgent();
            health = enemyModel.health;
            _healthBar = enemyView.GetHealthBar();
            playerCamera = EnemyService.Instance.GetCamera();
            SetupHealthBar();
        }
        public void Shoot(Transform gunTransform)
        {
            EnemyService.Instance.ShootBullet(gunTransform);
        }
        public void TakeDamage(int damage)
        {
            health -= damage;
            _healthBar.value = health;
            if (health < 0)
                TankDeath();
        }
        void TankDeath()
        {
            EnemyService.Instance.DestoryEnemy(this);
            EnemyService.Instance.ItemDrop(enemyView.transform);
        }
       
        public Vector3 GetPosition()
        {
            return enemyView.transform.position;
        }
        public NavMeshAgent GetEnemyAgent()
        {
            return agent;
        }
        public int GetStrenth()
        {
            return enemyModel.strength;
        }
       private float DistanceBetbeenTank()
        {
            if(TankService.Instance.GetPlayerTansform().position == null)
            {
                return Mathf.Infinity;
            }
            float distance = Vector3.Distance(TankService.Instance.GetPlayerTansform().position, enemyView.transform.position);
            return distance;
        }

        public void EnableEnemyTank(Vector3 _newPosition)
        {
            health = enemyModel.health; 
            _healthBar.value = enemyModel.health;
            enemyView.transform.position = _newPosition;
            enemyView.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            enemyView.gameObject.SetActive(true);
        }
        public Transform PlayerTransform()
        {
            return TankService.Instance.GetPlayerTansform();
        }
        private void SetupHealthBar()
        {
                _healthBar.maxValue = enemyModel.health; 
        }

    }
}