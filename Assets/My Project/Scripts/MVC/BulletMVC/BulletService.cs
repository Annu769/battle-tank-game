using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BattleTank
{
    public class BulletService : GenericSingleTon<BulletService>
    {

        [SerializeField] private BulletSOList bulletSOList;
        [SerializeField] private ParticleSystem bulletExplosion;
        private BulletController bulletController;
        public BulletPoolService poolService;
        public Action<int> onBulletFired;
        private BulletExplosionPoolService bulletExplosionPoolService;
        private void Start()
        {
            poolService = (BulletPoolService)BulletPoolService.Instance;
            bulletExplosionPoolService = (BulletExplosionPoolService)BulletExplosionPoolService.Instance;
        }
        public void BulletShootByTank(Transform bulletSpawnPoint)
        {

            bulletController = poolService.GetBullet(bulletSOList.bulletList);
            bulletController.EnableBullet(bulletSpawnPoint);

        }

        public void BulletExplosion(BulletController bulletController, Vector3 position)
        {
            StartCoroutine(PlayExplosionEffect(position));

            bulletController.DisableBullet();
            poolService.ReturnItem(bulletController);
        }

        private IEnumerator PlayExplosionEffect(Vector3 position)
        {
            ParticleSystem explosion = bulletExplosionPoolService.GetExplosion(bulletExplosion);

            explosion.transform.position = position;
            explosion.gameObject.SetActive(true);
            explosion.Play();

            yield return new WaitForSeconds(2f);

            explosion.gameObject.SetActive(false);
            bulletExplosionPoolService.ReturnItem(explosion);

        }
    }
}

