using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BattleTank
{
    public class BulletPoolService : GenericPoolService<BulletController>
    {
        private BulletScriptableObject _bulletScriptable;
        public BulletController GetBullet(BulletScriptableObject bulletScriptable)
        {
            _bulletScriptable = bulletScriptable;
            return GetItem();
        }
        protected override BulletController CreateItem()
        {
            
            BulletController bullet = new BulletController(_bulletScriptable);
            return bullet;
        }
    }
}
