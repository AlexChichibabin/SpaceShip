using UnityEngine;
using UnityEngine.Events;


namespace SpaceShip
{
    /// <summary>
    /// some destructible object that can own hitpoints
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// object ignores damages
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// start hitpoints amount
        /// </summary>
        [SerializeField] private int m_HitPoints;

        /// <summary>
        /// current hitpoints amount
        /// </summary>
        private int m_CurrentHitPoints;
        public int CurrentHitPoints => m_CurrentHitPoints;

        /// <summary>
        ///  last position value before death
        /// </summary>
        private Vector3 m_LastPosition;
        public Vector3 LastPosition => m_LastPosition;

        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }

        #endregion

        #region Public API

        /// <summary>
        /// apply damage to object
        /// </summary>
        /// <param name="damage"></param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0)
            {
                OnDeath();
            }
        }

        #endregion

        /// <summary>
        /// virtual event of object destroy, when current hitpoints becomes below or equal zero
        /// </summary>
        protected virtual void OnDeath()
        {
            m_LastPosition = gameObject.transform.position;

            Destroy(gameObject);

            m_EventOnDeath?.Invoke();
        }

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;
    }
}
