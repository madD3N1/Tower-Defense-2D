using UnityEngine;

namespace SpaceShooter
{
    public class ImpactEffect : MonoBehaviour
    {
        [SerializeField] private float m_Lifetime;

        private float m_CurrentTime;

        private void Update()
        {
            m_CurrentTime += Time.deltaTime;

            if(m_CurrentTime >= m_Lifetime)
            {
                Destroy(gameObject);
            }
        }
    }
}
