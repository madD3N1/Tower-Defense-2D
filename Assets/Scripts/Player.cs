using UnityEngine;
using System.Collections;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ������� ��������� ��������� ������.
    /// </summary>
    public class Player : SingletonBase<Player>
    {
        #region Properties

        /// <summary>
        /// ���-�� ������.
        /// </summary>
        [SerializeField] private int m_NumLives;
        public int NumLives => m_NumLives;

        /// <summary>
        /// �������, ������� ��������� �����.
        /// </summary>
        [SerializeField] private SpaceShip m_Ship;

        /// <summary>
        /// ������ �������.
        /// </summary>
        [SerializeField] private GameObject m_PlayerShipPrefab;

        public SpaceShip ActiveShip => m_Ship;

        //[SerializeField] private CameraController m_CameraController;
        //[SerializeField] private MovementController m_MovementController;

        #endregion

        #region Unity Events

        protected override void Awake()
        {
            base.Awake();

            if (m_Ship != null)
                Destroy(m_Ship.gameObject);
        }

        private void Start()
        {
            Respawn();
        }

        #endregion

        #region Private API

        private void OnShipDeath()
        {
            StartCoroutine(Death());
        }

        IEnumerator Death()
        {
            yield return new WaitForSecondsRealtime(1.1f);

            m_NumLives--;

            if (m_NumLives > 0)
            {
                Respawn();
            }
            else
            {
                LevelSequenceController.Instance?.FinishCurrentLevel(false);
            }
        }

        /// <summary>
        /// �����, ������� ��������� ������.
        /// </summary>
        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

                m_Ship = newPlayerShip.GetComponent<SpaceShip>();
                m_Ship.EventOnDeath.AddListener(OnShipDeath);

                //m_CameraController.SetTarget(m_Ship.transform);
                //m_MovementController.SetTargetShip(m_Ship);
            }
        }

        #endregion

        protected void TakeDamage(int damage)
        {
            m_NumLives -= damage;

            if(m_NumLives <= 0)
            {
                LevelSequenceController.Instance?.FinishCurrentLevel(false);
            }
        }

        #region Score

        public int Score { get; private set; }

        public int NumKills { get; private set; }

        public void AddKill()
        {
            NumKills++;
        }

        public void AddScore(int num)
        {
            Score += num;
        }

        #endregion
    }
}
