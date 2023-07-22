using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace BattleTank
{
    public class EnemyController
    {

        public EnemyView enemyView { get; }
        public EnemyModel enemyModel { get; }
        private NavMeshAgent agent;
        public Vector3 spawnPoint;
        private Rigidbody rb;
        int health;
        public EnemyController(EnemyScriptableObject enemy, float x = 0, float z = 0)
        {
            enemyView = GameObject.Instantiate<EnemyView>(enemy.enemyView, new Vector3(Random.Range(-x, x), 0, Random.Range(-z, z)), Quaternion.identity);
            enemyModel = new EnemyModel(enemy);

            enemyView.SetEnemyController(this);
            enemyModel.SetEnemyController(this);

            rb = enemyView.GetRigidbody();
            agent = enemyView.GetAgent();
            health = enemyModel.health;
        }


        public void Shoot(Transform gunTransform)
        {
            EnemyService.Instance.ShootBullet(gunTransform);
        }
        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health < 0)
                TankDeath();
        }
        void TankDeath()
        {
            EnemyService.Instance.DestoryEnemy(this);
        }
        public int GetStrength()
        {
            return enemyModel.strength;
        }
        public Vector3 GetPosition()
        {
            return enemyView.transform.position;
        }
        public NavMeshAgent GetEnemyAgent()
        {
            return agent;
        }
        public void FireShell(float Distance)
        {

        }
    }
}