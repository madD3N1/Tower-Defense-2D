using UnityEngine;
using SpaceShooter;
using System.Collections.Generic;

namespace TowerDefense
{
    public class TDPatrolController : AIController
    {
        private Path m_Path;

        private int pathIndex;

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
                Destroy(gameObject);
            }
        }
    }
}
