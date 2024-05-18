using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BattleTank
{
    public class TankView : MonoBehaviour, IDamageable
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private Transform tankBody;
        [SerializeField] private Slider healthBar;
        private TankController tankController;
        public Transform GetBulletSpawnPoint() => bulletSpawnPoint;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.freezeRotation = true;
            }
            Debug.Log(healthBar.value);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(tankController);
                Debug.Log(bulletSpawnPoint);
                tankController.Shoot(bulletSpawnPoint);
            }
        }
        private void FixedUpdate()
        {        
            tankController.TankMove();
        }
        public void SetTankController(TankController _tankController)
        {
            tankController = _tankController;
        }
        public Rigidbody GetRigidbody()
        {
            return rb;
        }
        public Transform GetTransform()
        {
            return transform;
        }
        public Transform GetTankBody()
        {
            return tankBody;
        }
        void IDamageable.TakeDamage(int Damage)
        {
            tankController.TakeDamage(Damage);
        }
        public Slider GetHealthBar()
        {
            return healthBar;
        }
        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.GetComponent<EnemyView>() != null)
            {
                EnemyView enemyView = col.gameObject.GetComponent<EnemyView>();
                tankController.TakeDamage(enemyView.GetEnemyStrength());
            }
        }
        public void AddHeal(int healamount)
        {
            tankController.Heal(healamount);
        }
    }
}