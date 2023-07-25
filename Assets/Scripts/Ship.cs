using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.EventSystems.EventTrigger;

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
        [SerializeField] private GameObject m_PrefabExplosion;

        #region Unity Events
        protected override void Start()
        {
            base.Start();
            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;
            m_Rigid.inertia = 1;

            InitOffensive();
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();

            UpdateEnergyRegen();
        }
        #endregion

        /// <summary>
        /// Method, that get moveforce to ship
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigid.AddForce( ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force );

            m_Rigid.AddForce( -m_Rigid.velocity * ( m_Thrust / m_MaxLinearVelocity ) * Time.fixedDeltaTime, ForceMode2D.Force );

            m_Rigid.AddTorque( TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force );

            m_Rigid.AddTorque( -m_Rigid.angularVelocity * ( m_Mobility / m_MaxAngularVelocity ) * Time.fixedDeltaTime, ForceMode2D.Force );
        }

        #region Public API
        /// <summary>
        /// thrust control. from -1.0 to +1.0
        /// </summary>
        public float ThrustControl { get; set; }
        /// <summary>
        /// torque control. from -1.0 to +1.0
        /// </summary>
        public float TorqueControl { get; set; }

        protected override void OnDeath()
        {
            base.OnDeath();
            Instantiate(m_PrefabExplosion, LastPosition, Quaternion.identity);
        }


        #endregion

        [SerializeField] private Turret[] m_Turrets;

        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }

        [SerializeField] private int m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;
        [SerializeField] private int m_EnergyRegenPerSecond;

        private float m_PrimaryEnergy;
        private int m_SecondaryAmmo;

        public void AddEnergy(int energy)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + energy, 0, m_MaxEnergy);
        }
        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }
        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }
        private void UpdateEnergyRegen()
        {
            //m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime, 0, m_MaxEnergy);
        }

        public bool DrawEnergy(int count)
        {
            if (count == 0)
            {
                return true;
            }
            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }
            return false;
        }
        public bool DrawAmmo(int count)
        {
            if (count == 0)
            {
                return true;
            }
            if (m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }
            return false;
        }
    }
}

