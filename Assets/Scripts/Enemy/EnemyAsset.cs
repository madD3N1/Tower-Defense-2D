using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        [Header("Appearance")]

        public Color color = Color.white;

        public Vector2 spriteScale = new Vector2(4, 4);

        public RuntimeAnimatorController animations;

        [Header("Game parameters")]

        public float moveSpeed = 1;

        public int hp = 1;

        public int score = 1;

        public float radius = 0.35f;

        public int damage = 1;

        public int gold = 1;
    }
}
