using UnityEngine;
using SpaceShooter;
using System.Collections.Generic;

namespace TowerDefense
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private CircleArea startArea;
        public CircleArea StartArea => startArea;

        [SerializeField] private List<AIPointPatrol> points;
        public List<AIPointPatrol> Points => points;

        public int Length { get => points.Count; }

        public AIPointPatrol this[int i] { get => points[i]; }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            foreach(var point in points)
            {
                Gizmos.DrawWireSphere(point.transform.position, point.Radius);
            }     
        }
    }
}
