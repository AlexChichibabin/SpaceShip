using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ship : Destructible
    {
        /// <summary>
        /// mass for automatic rigid set
        /// </summary>
        [Header("Space Ship")]
        [SerializeField] private float m_Mass;
        /// <summary>
        /// force power
        /// </summary>
        [SerializeField] private float m_Thrust;
        /// <summary>
        /// rotate power
        /// </summary>
        [SerializeField] private float m_Mobility;
        /// <summary>
        /// maximum linear speed
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        /// <summary>
        /// maximum rotate speed
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;

        /// <summary>
        /// saved link for rigid
        /// </summary>
        private Rigidbody2D m_Rigid;

        #region Unity Events
        protected override void Start()
        {
            base.Start();
            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;
            m_Rigid.inertia = 1;
        }
        #endregion


        #region Public API
        /// <summary>
        /// thrust control. from -1.0 to +1.0
        /// </summary>
        public float ThrustControl { get; set; }
        /// <summary>
        /// torque control. from -1.0 to +1.0
        /// </summary>
        public float TorqueControl { get; set; }


        #endregion
    }
}

