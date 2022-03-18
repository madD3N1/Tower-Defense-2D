using UnityEngine;
using SpaceShooter;
using UnityEngine.Events;

namespace TowerDefense
{
    public class TDPatrolController : AIController
    {
        private Path m_Path;

        private int pathIndex;

        [SerializeField] private UnityEvent OnEndPath;

        public void SetPath(Path path)
        {
            m_Path = path;
            SetPatrolBehaviour(m_Path.Points);
        }

        protected override void MoveNextPosition()
        {
            base.MoveNextPosition();

            if(IsForward == false)
            {
                OnEndPath?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
