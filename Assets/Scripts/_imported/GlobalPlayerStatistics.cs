using UnityEngine;

namespace SpaceShooter
{
    public class GlobalPlayerStatistics : SingletonBase<GlobalPlayerStatistics>
    {
        private int totalScore;
        public int Score => totalScore;

        private int totalKills;
        public int Kills => totalKills;

        private int totalGameTime;
        public int Time => totalGameTime;

        public void Reset()
        {
            totalScore = 0;
            totalKills = 0;
            totalGameTime = 0;
        }

        public void AddScore(int score)
        {
            totalScore += score;
        }

        public void AddKills(int kills)
        {
            totalKills += kills;
        }

        public void AddTime(int time)
        {
            totalGameTime += time;
        }
    }
}
