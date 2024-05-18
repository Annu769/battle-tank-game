using UnityEngine;
using TMPro;
using BattleTank;
using UnityEditor.MPE;

namespace BattleTank
{
	public class AchievementSystem : GenericSingleTon<AchievementSystem>
	{
        [SerializeField] private AchievementScript achievementPrefab;
        [SerializeField] private int[] bulletCheckpoints;
        [SerializeField] private int[] enemiesDestroyedCheckpoints;

        void Start()
        {
            EventServices.Instance.OnPlayerFiredBullet += PlayerBulletAchievement;
            EventServices.Instance.OnDistanceTravelled += DistanceTravelledAchievement;
            EventServices.Instance.OnEnemyDestroy += EnemyDeathAchievement;
        }

        public void PlayerBulletAchievement(int bulletCount)
        {
            for (int i = 0; i < bulletCheckpoints.Length; i++)
            {
                if (bulletCheckpoints[i] == bulletCount)
                    UnlockAchievement($"{bulletCount} Bullets fired!");
            }
        }

        public void EnemyDeathAchievement(int enemiesDestroyedCount)
        {
            for (int i = 0; i < enemiesDestroyedCheckpoints.Length; i++)
            {
                if (bulletCheckpoints[i] == enemiesDestroyedCount)
                    UnlockAchievement($"{enemiesDestroyedCount} enemies destroyed!");
            }
        }

        public void DistanceTravelledAchievement(float distance)
        {
            UnlockAchievement($"Distance Travelled {distance}!");
        }

        public void UnlockAchievement(string _achievement)
        {
            AchievementScript newAchievement = Instantiate<AchievementScript>(achievementPrefab);

            newAchievement.transform.SetParent(this.transform);
            newAchievement.SetLocalTransform();
            newAchievement.SetMessage(_achievement);

            newAchievement.ShowcaseAchievement();
        }

        void OnDestroy()
        {
            EventServices.Instance.OnPlayerFiredBullet -= PlayerBulletAchievement;
            EventServices.Instance.OnDistanceTravelled -= DistanceTravelledAchievement;
            EventServices.Instance.OnEnemyDestroy -= EnemyDeathAchievement;
        }
    }
}
