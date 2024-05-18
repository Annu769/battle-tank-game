using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.MPE;
using UnityEngine;
namespace BattleTank
{
    public class TankService : GenericSingleTon<TankService>
    {
        private int bulletCount;
        private TankController tankController;
        private List<int> distanceCheckpointsAchieved = new();
        [SerializeField] private TankTypeScriptableObjectList tanklist;
        [SerializeField] private ParticleSystem tankExplosion;
        [SerializeField] private CameraController mainCamera;
        [Header("Distance achievement")]
        [SerializeField] private int[] distanceCheckpoints;
        public Transform GetbulletTransform() => tankController.GetBulletSpwanTransfrom();
        private void Start()
        {
            bulletCount = 0;
            CreateTank();
        }
        private void CreateTank()
        {
            int index = UnityEngine.Random.Range(0, tanklist.list.Count);
            TankTypeScriptableObject tankTypeScriptableObject = tanklist.list[index];
            tankController = new TankController(tankTypeScriptableObject,mainCamera);
        }
        public void ShootBullet( Transform tankTransform)
        {
            bulletCount++;
            EventServices.Instance.InvokePlayerFiredBullet(bulletCount);
            BulletService.Instance.BulletShootByTank(tankTransform);
        }
        public Transform GetPlayerTansform()
        {
            return tankController.PlayerTransform();
        }
        public void DestoryTank(TankView tankView)
        {
            GameManager.Instance.isGameOver = true;
            Vector3 pos = tankView.transform.position;
            mainCamera.SetTankTransform(null);
            StartCoroutine(TankExplosion(pos));
            Destroy(tankView.gameObject);
            GameManager.Instance.GameOver();
            
        }
        public IEnumerator TankExplosion(Vector3 tankPos)
        {
            ParticleSystem newTankExplosion = GameObject.Instantiate<ParticleSystem>(tankExplosion, tankPos, Quaternion.identity);
            newTankExplosion.Play();
            yield return new WaitForSeconds(2f);
            Destroy(newTankExplosion.gameObject);
        }
        public void distanceTravelled(float distance)
        {
            for (int i = distanceCheckpoints.Length - 1; i >= 0; i--)
            {
                if (distance > distanceCheckpoints[i] && !distanceCheckpointsAchieved.Contains(distanceCheckpoints[i]))
                {
                    EventServices.Instance.InvokeDistanceTravelled(distanceCheckpoints[i]);
                    distanceCheckpointsAchieved.Add(distanceCheckpoints[i]);
                    return;
                }
            }
        }
    }
}