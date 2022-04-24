using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius;

        [SerializeField] private TowerAsset m_TowerAsset;

        private Turret[] turrets;

        private Destructible target = null;

        private void Start()
        {
            m_Radius = m_TowerAsset.radius;
            turrets = GetComponentsInChildren<Turret>();
            foreach(var turret in turrets)
            {
                turret.Use(m_TowerAsset.props);
            }
        }

        private void Update()
        {
            if(target)
            {
                Vector2 targetVector = target.transform.position - transform.position;
                // TODO: попробовать так:
                //Vector2 targetVector = target.transform.position;

                if (targetVector.magnitude <= m_Radius)
                {
                    foreach (var turret in turrets)
                    {
                        turret.transform.up = targetVector;
                        turret.Fire();
                    }
                }    
                else
                {
                    target = null;
                }
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);

                if (enter)
                {
                    target = enter.transform.root.GetComponent<Destructible>();   
                }
            }  
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }

        public void Use(TowerAsset towerAsset)
        {
            m_TowerAsset = towerAsset;
        }
    }
}
