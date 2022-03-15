using UnityEngine;
using System.Collections.Generic;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol
        }

        [SerializeField] private AIBehaviour m_AIBehaviour;

        [SerializeField] private List<AIPointPatrol> m_PatrolPoints;

        private int m_IndexCurrentPatrolPoint;

        private bool m_IsForward;

        public bool IsForward => m_IsForward;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        [SerializeField] private float m_RandomSelectMovePointTime;

        [SerializeField] private float m_FindNewTargetTime;

        [SerializeField] private float m_MaxDistanceFindTarget;

        [SerializeField] private float m_ShootDelay;

        [SerializeField] private float m_EvadeRayLength;

        private SpaceShip m_SpaceShip;

        private Vector3 m_MovePosition;

        private Destructible m_SelectedTarget;

        private Timer m_RandomizeDirectionTimer;

        private Timer m_FireTimer;

        private Timer m_FindNewTargetTimer;

        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();
            m_SpaceShip.EventOnDeath.AddListener(OnDeath);

            m_IndexCurrentPatrolPoint = 0;
            m_IsForward = true;

            InitTimers();
        }

        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }

        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol()
        {
            ActionFindNewMovePosition();
            //ActionAvadeCollision();
            ActionControlShip();
            //ActionFindNewAttackTarget();
            //ActionFire();      
        }

        private void ActionFindNewMovePosition()
        {
            if(m_AIBehaviour == AIBehaviour.Patrol)
            {
                if(m_SelectedTarget != null)
                {
                    m_MovePosition = MakeLead();
                }
                else
                {
                    if (m_PatrolPoints != null)
                    {
                        bool isInsidePatrolZone = (m_PatrolPoints[m_IndexCurrentPatrolPoint].transform.position - transform.position).sqrMagnitude
                                                        < m_PatrolPoints[m_IndexCurrentPatrolPoint].Radius * m_PatrolPoints[m_IndexCurrentPatrolPoint].Radius;

                        if (isInsidePatrolZone == true)
                        {
                            //if (m_PatrolPoints.Count == 1)
                            //{
                            //    if (m_RandomizeDirectionTimer.IsFinished == true)
                            //    {
                            //        Vector2 newPoint = Random.onUnitSphere * m_PatrolPoints[m_IndexCurrentPatrolPoint].Radius + m_PatrolPoints[m_IndexCurrentPatrolPoint].transform.position;

                            //        m_MovePosition = newPoint;

                            //        m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);

                            //        return;
                            //    }
                            //    else
                            //        return;
                            //}

                            MoveNextPosition();

                            //else
                            //{
                            //    if (m_IndexCurrentPatrolPoint == 0)
                            //    {
                            //        m_IsForward = true;
                            //        m_IndexCurrentPatrolPoint++;
                            //    }
                            //    else
                            //    {
                            //        m_IndexCurrentPatrolPoint--;
                            //    }
                            //}
                        }
                        else
                        {
                            m_MovePosition = m_PatrolPoints[m_IndexCurrentPatrolPoint].transform.position;
                        }
                    }
                }
            }
        }

        protected virtual void MoveNextPosition()
        {
            if (m_IsForward == true)
            {
                if (m_IndexCurrentPatrolPoint == m_PatrolPoints.Count - 1)
                {
                    m_IsForward = false;
                    //m_IndexCurrentPatrolPoint--;
                }
                else
                {
                    m_IndexCurrentPatrolPoint++;
                }
            }
        }

        private void ActionAvadeCollision()
        {
            if(Physics2D.Raycast(transform.position + transform.up, transform.up, m_EvadeRayLength) == true
                || Physics2D.Raycast(transform.position + -transform.right, -transform.right, m_EvadeRayLength) == true)
            {
                m_MovePosition = transform.position + transform.right * 100.0f;
            }
        }

        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLinear;

            m_SpaceShip.TorqueControl = ComputeAlignTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        private const float MAX_ANGLE = 45.0f; 

        private static float ComputeAlignTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }

        private void ActionFindNewAttackTarget()
        {
            if (m_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindNearestDestructableTarget(m_MaxDistanceFindTarget);

                m_FindNewTargetTimer.Restart();
            }
        }

        private void ActionFire()
        {
            if(m_SelectedTarget != null)
            {
                if(m_FireTimer.IsFinished == true)
                {
                    //m_SpaceShip.Fire(TurretMode.Primary);

                    m_FireTimer.Restart();
                }
            }
        }

        private Destructible FindNearestDestructableTarget(float maxDist)
        {
            Destructible potentialTarget = null;

            foreach(var dest in Destructible.AllDestructibles)
            {
                if (dest.GetComponent<SpaceShip>() == m_SpaceShip) continue;

                if (dest.TeamId == Destructible.TeamIdNeutral) continue;

                if (dest.TeamId == m_SpaceShip.TeamId) continue;

                float dist = Vector2.Distance(m_SpaceShip.transform.position, dest.transform.position);

                if(dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = dest;
                }
            }

            return potentialTarget;
        }

        private Vector3 MakeLead()
        {
            var newPos = new Vector3();
            var rand = Random.Range(0, 2);
            
            if(rand == 0)
            {
                newPos = m_SelectedTarget.transform.position + m_SelectedTarget.transform.right * -2.5f + Vector3.up * 2;
            }
            
            if(rand == 1)
            {
                newPos = m_SelectedTarget.transform.position + m_SelectedTarget.transform.right * 2.5f + Vector3.up * 2;
            } 
            
            if(rand == 2)
            {
                newPos = m_SelectedTarget.transform.position + m_SelectedTarget.transform.right;
            }

            return newPos;
        }

        private void OnDeath()
        {
            //Player.Instance.AddKill();
        }

        #region Timers

        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
        }

        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
        }

        #endregion

        public void SetPatrolBehaviour(List<AIPointPatrol> points)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoints = points;
        }
    }
}
