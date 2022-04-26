using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        private int levelScore = 3;

        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                ResultPanelController.Instance?.ShowResult(false);
            };

            m_ReferenceTime += Time.time;

            m_EventLevelCompleted.AddListener(() =>
            {
                StopLevelActivity();

                if(m_ReferenceTime <= Time.time)
                {
                    levelScore--;
                }

                MapCompletion.SaveEpisodeResult(levelScore);
            });

            void LifeScoreChange(int _)
            {
                levelScore--;
                TDPlayer.OnLifeUpdate -= LifeScoreChange;
            }

            TDPlayer.OnLifeUpdate += LifeScoreChange;
        }

        private void StopLevelActivity()
        {
            foreach(var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpaceShip>().enabled = false;
                enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            DisableAll<Spawner>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
        }

        private void DisableAll<T>() where T: MonoBehaviour
        {
            foreach (var obj in FindObjectsOfType<T>())
            {
                obj.enabled = false;
            }
        }
    }
}
