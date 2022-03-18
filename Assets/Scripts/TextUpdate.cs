using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource
        {
            Gold,
            Life
        }

        public UpdateSource source = UpdateSource.Gold;

        private Text m_Text;

        private void Start()
        {
            m_Text = GetComponent<Text>();

            switch(source)
            {
                case UpdateSource.Gold:
                    TDPlayer.GoldUpdateSubsribe(UpdateText);
                    break;
                case UpdateSource.Life:
                    TDPlayer.LifeUpdateSubsribe(UpdateText);
                    break;
            }
        }

        private void UpdateText(int value)
        {
            m_Text.text = value.ToString();
        }
    }
}
