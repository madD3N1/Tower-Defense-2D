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

        // TODO: добавить настройки для турели башни.

        public TurretProperties props;

        public float radius;
    }
}
