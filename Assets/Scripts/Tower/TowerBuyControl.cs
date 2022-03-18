using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TowerBuyControl : MonoBehaviour
    {
        [SerializeField] private TowerAsset m_TowerAsset;

        [SerializeField] private Text m_Text;

        [SerializeField] private Button m_Button;

        [SerializeField] private Transform m_BuildSite;

        public Transform BuildSite { set { m_BuildSite = value; } }

        private void Start()
        {
            TDPlayer.GoldUpdateSubsribe(GoldStatusCheck);
            m_Text.text = m_TowerAsset.goldCost.ToString();
            m_Button.GetComponent<Image>().sprite = m_TowerAsset.GUISprite;
        }

        private void GoldStatusCheck(int gold)
        {
            if(gold >= m_TowerAsset.goldCost != m_Button.interactable)
            {
                m_Button.interactable = !m_Button.interactable;
                m_Text.color = m_Button.interactable ? Color.white : Color.red;
            }
        }

        public void Buy()
        {
            TDPlayer.Instance.TryBuild(m_BuildSite, m_TowerAsset);
        }
    }
}
