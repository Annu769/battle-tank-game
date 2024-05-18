using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BattleTank
{
    public class BulletController
    {
        private BulletModel bulletModel;
        private BulletView bulletView;
        private Rigidbody rb;

        public BulletController(BulletScriptableObject _bullet)
        {
            bulletView = GameObject.Instantiate<BulletView>(_bullet.bulletView);
            bulletModel = new BulletModel(_bullet);

            bulletView.SetBulletController(this);
            bulletModel.SetBulletController(this);

            rb = bulletView.GetRigidbody();
        }

        public void SetBulletTankType(TankType tankType)
        {
            bulletModel.SetTankType(tankType);
        }

        public void Shoot()
        {
            rb.AddForce(rb.transform.forward * bulletModel.range, ForceMode.Impulse);
        }

        public void BulletCollision(Vector3 position)
        {
            rb.rotation = Quaternion.identity;

            BulletService.Instance.BulletExplosion(this, position);
        }

        public int GetBulletDamage()
        {
            return bulletModel.damage;
        }

        public TankType GetTankType()
        {
            return bulletModel.tankType;
        }

        public void EnableBullet(Transform gunTransform)
        {
            

            rb.transform.position = gunTransform.position;
            rb.transform.rotation = gunTransform.rotation;

            rb.gameObject.SetActive(true);
            Shoot();
        }

        public void DisableBullet()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.rotation = Quaternion.identity;

            rb.gameObject.SetActive(false);
        }
    }
}