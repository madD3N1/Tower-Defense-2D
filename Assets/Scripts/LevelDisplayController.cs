using UnityEngine;

namespace TowerDefense
{
    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private MapLevel[] levels;

        private void Start()
        {
            int drawLevel = 0;
            int score = 1;

            while(score != 0 && drawLevel < levels.Length && 
                MapCompletion.Instance.TryIndex(drawLevel, out var episode, out score))
            {
                levels[drawLevel].SetLevelData(episode, score);
                drawLevel++;
            }

            for(int i = drawLevel; i < levels.Length; i++)
            {
                levels[i].gameObject.SetActive(false);
            }
        }
    }
}
