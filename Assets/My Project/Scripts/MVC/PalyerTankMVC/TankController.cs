using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
namespace BattleTank
{
    public class TankController
    {
        private TankModel tankModel;
        private TankView tankView;
        private Vector3 movementDirection;
        private float tankSpeed;
        private Rigidbody rb;
        private int health;
        private Vector3 oldPosition;
        private float totalDistanceTravelled;
        private float currentDistance;
        private Slider _healthBar;


        public TankController(TankTypeScriptableObject playerSO,CameraController cameraController)
        {
            tankModel = new TankModel(playerSO);
            tankView = GameObject.Instantiate<TankView>(playerSO.tankView);
            tankView.SetTankController(this);
            tankModel.SetTankController(this);
            cameraController.SetTankTransform(tankView.transform); 
            tankSpeed = tankModel.Speed;
            rb = tankView.GetRigidbody();
            health = tankModel.Health; 
            _healthBar = tankView.GetHealthBar();
            _healthBar.value = health;
            oldPosition = rb.transform.position;
            Debug.Log(_healthBar.value);
            SetupHealthBar();
        }
        public Transform GetBulletSpwanTransfrom() => tankView.GetBulletSpawnPoint();
        private void ChangeTankColour()
        {
            for (int i = 0; i < tankView.GetTankBody().childCount; i++)
            {
                tankView.GetTankBody().GetChild(i).GetComponent<MeshRenderer>().material = tankModel.tankMaterial;
            }
        }
        public void Shoot(Transform gunTrasform)
        {
            TankService.Instance.ShootBullet(gunTrasform);
        }
        public void TankMove()
        {
            UIService uiService = UIService.Instance;
            if (uiService == null)
            {
                Debug.LogError("UIService instance is missing or not properly initialized.");
                return;
            }
            movementDirection.x = uiService.GetJoystickHorizontal();
            movementDirection.z = uiService.GetJoystickVertical();
            tankView.GetRigidbody().velocity = movementDirection * tankModel.Speed * Time.fixedDeltaTime;
            if (movementDirection != Vector3.zero)
            {
                tankView.transform.forward = movementDirection;
            }
        }
        internal void TakeDamage(int damage)
        {
            health -= damage;
            _healthBar.value = health;
            if (health < 0)
            {
                TankDeath();
            }
        }
        private void TankDeath()
        {
            TankService.Instance.DestoryTank(tankView);
        }
        public Transform PlayerTransform()
        {
           return tankView.transform;
        }
        private TankModel GetModel()
        {
            return tankModel;
        }
        public void Heal(int healAmount)
        {
            if (health < tankModel.Health || health > 0)
            {
                health += healAmount;
                _healthBar.value = health;
            }
            else
            {
                Debug.Log("Health is Full");
            }

        }
        private void CalculateDistance()
        {
            currentDistance = (rb.transform.position - oldPosition).magnitude;

            totalDistanceTravelled += currentDistance;
            oldPosition = rb.transform.position;

            TankService.Instance.distanceTravelled(totalDistanceTravelled);
        }
        private void SetupHealthBar()
        {
            _healthBar.value = tankModel.Health;
            _healthBar.maxValue = tankModel.Health;
        }
    }
}