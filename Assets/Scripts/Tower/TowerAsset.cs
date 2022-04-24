using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        public int goldCost = 10;

        public Sprite sprite;

        public Sprite GUISprite;

        public TurretProperties props;

        public float radius;
    }
}
