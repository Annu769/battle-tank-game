using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

namespace BattleTank
{
    [RequireComponent(typeof(EnemyView))]
    public class StateBase : MonoBehaviour
    {
        protected EnemyView enemyView;

        private void Awake()
        {
            enemyView = GetComponent<EnemyView>();
        }

        public virtual void OnStateEnter()
        {
            this.enabled = true;
        }

        public virtual void OnStateExit()
        {
            this.enabled = false;
        }

        public virtual void Tick() { }
    }
}
