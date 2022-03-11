using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        #region Properties

        [SerializeField] private float m_Velocity;

        [SerializeField] private float m_Lifetime;

        [SerializeField] private int m_Damage;

        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        private float m_Timer;

        #endregion

        #region Unity Events

        private void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLength;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                var dest = hit.collider.transform.root.GetComponent<Destructible>();

                if (dest != null && dest != m_Parent)
                {
                    dest.ApplyDamage(m_Damage);

                    if(m_Parent == Player.Instance.ActiveShip)
                    {
                        Player.Instance.AddScore(dest.ScoreValue);
                    }
                }

                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;
            if (m_Timer >= m_Lifetime)
            {
                Destroy(gameObject);
            }

            transform.position += new Vector3(step.x, step.y, 0);
               
        }

        #endregion

        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            var impactEffect = Instantiate(m_ImpactEffectPrefab);
            impactEffect.transform.position = pos;

            Destroy(gameObject);
        }

        private Destructible m_Parent;

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }
    }
}
