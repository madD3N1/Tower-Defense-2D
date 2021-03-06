#if UNITY_EDITOR

using UnityEditor;

#endif

using System;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int m_Damage = 1;

        [SerializeField] private int m_Gold = 1;

        public event Action OnEnd;

        private void OnDestroy() { OnEnd?.Invoke(); }

        public void Use(EnemyAsset asset)
        {
            var sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();

            sr.color = asset.color;
            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);

            sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;

            GetComponent<SpaceShip>().Use(asset);

            GetComponentInChildren<CircleCollider2D>().radius = asset.radius;

            m_Damage = asset.damage;
            m_Gold = asset.gold;
        }

        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceLife(m_Damage);
        }

        public void GivePlayerGold()
        {
            TDPlayer.Instance.ChangeGold(m_Gold);
        }
    }

    #if UNITY_EDITOR
    
    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector: Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var asset = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;

            if(asset)
            {
                (target as Enemy).Use(asset);
            }
        }
    }

    #endif
}
