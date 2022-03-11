using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private TextMeshProUGUI m_Result;

        [SerializeField] private TextMeshProUGUI m_Kills;

        [SerializeField] private TextMeshProUGUI m_Score;

        [SerializeField] private TextMeshProUGUI m_Time;

        [SerializeField] private TextMeshProUGUI m_ButtonNextText;

        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResult(PlayerStatistics levelResult, bool success)
        {
            gameObject.SetActive(true);

            m_Success = success;

            m_Result.text = success ? "Win" : "Lose";
            m_ButtonNextText.text = success ? "Next" : "Restart";
            m_Kills.text = $"Kills: {levelResult.numKills}";
            m_Score.text = $"Score: {levelResult.score}";
            m_Time.text = $"Time: {levelResult.time}";

            Time.timeScale = 0;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if(m_Success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                LevelSequenceController.Instance.RestartLevel();
            }
        }
    }
}
