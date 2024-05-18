using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
namespace BattleTank
{
    public class EnemyService : GenericSingleTon<EnemyService>
    {
      
        [SerializeField] private Transform spawnPointParent;
        [SerializeField] private EnemyScriptableObjectList enemyTankList;
        [SerializeField] private int initialEnemyCount = 3;
        [SerializeField] private int maxWaves = 3;
        [SerializeField] ParticleSystem tankExplosion;
        [SerializeField] private TextMeshProUGUI waveText;
        [SerializeField] private float timeBetweenWaves = 10f;
        [SerializeField] private GameObject waveCompleteCanvas;
        [SerializeField] private Camera myCamera;
        [SerializeField] private Pickup healItem;
        private  List<EnemyController> enemies;
        private List<Transform> spawnPoints = new List<Transform>();
        private List<Transform> pointsAlreadySpawned = new List<Transform>();
        private int maxEnemyCount = 10;
        private int currentWave;
        bool isSpawningEnemies = true;

        
        void Start()
        {
            waveCompleteCanvas.SetActive(false);
            enemies = new List<EnemyController>();
            initialEnemyCount = Mathf.Min(initialEnemyCount, maxEnemyCount);
            foreach (Transform item in spawnPointParent)
            {
                spawnPoints.Add(item);
            }
            StartWave();
        }
        private void StartWave()
        {
            if (!isSpawningEnemies)
                return;

           
            if (waveText != null)
            {
                waveText.text = "Wave: " + (currentWave + 1); 
            }

            
            int enemyCount = initialEnemyCount + (currentWave * 2); 

            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPoint();
                EnemyController enemyController = CreateEnemyTank(UnityEngine.Random.Range(0, enemyTankList.enemies.Length));
                enemies.Add(enemyController);
                enemyController.EnableEnemyTank(spawnPosition);
            }
        }
        public EnemyController CreateEnemyTank(int index)
        {
            EnemyScriptableObject enemy = enemyTankList.enemies[index];
            EnemyController enemyController = new EnemyController(enemy, 50, -10);
            return enemyController;
        }
        public Vector3 GetRandomSpawnPoint()
        {
            if(spawnPoints.Count == 0)
                return Vector3.zero;

            int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
            Transform newSpawnPoint = spawnPoints[spawnPointIndex];

            pointsAlreadySpawned.Add(newSpawnPoint);
            spawnPoints.RemoveAt(spawnPointIndex);

            return newSpawnPoint.position;
        }
        public void ShootBullet( Transform tankTransform)
        {
            BulletService.Instance.BulletShootByTank(tankTransform);
        }
        public void DestoryEnemy(EnemyController _enemyController)
        {
            Vector3 pos = _enemyController.GetPosition();
            Destroy(_enemyController.enemyView.gameObject);
            StartCoroutine(TankExplosion(pos));
            enemies.Remove(_enemyController);
            if (enemies.Count == 0)
            {
                // Start the next wave if not all waves are completed
                if (currentWave < maxWaves - 1)
                {
                    currentWave++;
                    Invoke("StartWave", timeBetweenWaves);
                }
                else
                {
                    Debug.Log("All waves completed.");
                    waveCompleteCanvas.SetActive(true);
                }
            }

        }
        public void ItemDrop(Transform enemyTransform)
        {
            GameObject.Instantiate(healItem, enemyTransform.position, Quaternion.identity);
        }
        public IEnumerator TankExplosion(Vector3 tankPos)
        {
            ParticleSystem newTankExplosion = GameObject.Instantiate<ParticleSystem>(tankExplosion, tankPos, Quaternion.identity);
            newTankExplosion.Play();
            yield return new WaitForSeconds(2f);
            Destroy(newTankExplosion.gameObject);
        }
        public IEnumerator DestroyAllEnemies()
        {
            yield return new WaitForSeconds(2f);
            List<EnemyController> enemyList = new List<EnemyController>(enemies);
            foreach (EnemyController enemy in enemyList)
            {
                DestoryEnemy(enemy);
                yield return new WaitForSeconds(2f);
            }
        }
        public Camera GetCamera()
        {
            return myCamera;
        }
    }
}