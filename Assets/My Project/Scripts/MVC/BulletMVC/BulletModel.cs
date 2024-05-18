using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BattleTank
{
    public class BulletModel
    {
        public int damage { get; }
        public int range { get; }
        public TankType tankType { private set; get; }

        private BulletController bulletController;

        public BulletModel(BulletScriptableObject _bullet)
        {
            damage = _bullet.damage;
            range = _bullet.range;

        }

        public void SetTankType(TankType _tankType)
        {
            tankType = _tankType;
        }

        public void SetBulletController(BulletController _bulletController)
        {
            bulletController = _bulletController;
        }
    }
}