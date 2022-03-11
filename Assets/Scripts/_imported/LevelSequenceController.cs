using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        #region Properties

        public static string MainMenuSceneNickname = "MainMenu";

        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public bool LastLevelResult { get; private set; }

        public PlayerStatistics LevelStatistics { get; private set; }

        public static SpaceShip PlayerShip { get; set; }
        
        #endregion

        public void StartEpisode(Episode episode)
        {
            CurrentEpisode = episode;
            CurrentLevel = 0;

            LevelStatistics = new PlayerStatistics();
            LevelStatistics.Reset();

            SceneManager.LoadScene(episode.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void FinishCurrentLevel(bool success)
        {
            LastLevelResult = success;
            CalculateLevelStatistic();

            ResultPanelController.Instance?.ShowResult(LevelStatistics, LastLevelResult);
        }

        public void AdvanceLevel()
        {
            LevelStatistics.Reset();

            CurrentLevel++;

            if(CurrentEpisode.Levels.Length >= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }

        private void CalculateLevelStatistic()
        {
            if ((int)LevelController.Instance.LevelTime <= LevelController.Instance.ReferenceTime)
                LevelStatistics.score = Player.Instance.Score * 2;
            else
                LevelStatistics.score = Player.Instance.Score;

            LevelStatistics.time = (int)LevelController.Instance.LevelTime;
            LevelStatistics.numKills = Player.Instance.NumKills;

            GlobalPlayerStatistics.Instance?.AddScore(LevelStatistics.score);
            GlobalPlayerStatistics.Instance?.AddKills(LevelStatistics.numKills);
            GlobalPlayerStatistics.Instance?.AddTime(LevelStatistics.time);
        }
    }
}
