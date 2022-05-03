using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class WaveLevelCondition : MonoBehaviour, ILevelCondition
    {
        private bool isCompleted;

        private void Start()
        {
            FindObjectOfType<EnemyWaveManager>().OnAllWavesDead += () => 
            { 
                isCompleted = true; 
            };
        }

        public bool IsCompleted { get { return isCompleted; } }
    }
}
