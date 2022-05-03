using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private Text bonusAmount;

        private EnemyWaveManager manager;

        private float timeToNextWave;

        private void Start()
        {
            manager = FindObjectOfType<EnemyWaveManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                timeToNextWave = time;
            };
        }

        private void Update()
        {
            var bonus = (int)timeToNextWave;

            if (bonus < 0) bonus = 0;

            bonusAmount.text = $"{bonus}";
            timeToNextWave -= Time.deltaTime;
        }

        public void CallWave()
        {
            manager.ForceNextWave();
        }
    }
}
