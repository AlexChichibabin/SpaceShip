using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    [RequireComponent(typeof(Ship))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol
        }

        [SerializeField] private AIBehaviour m_AIBehaviour;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        [SerializeField] private float m_RandomSelectMovePointTime;

        [SerializeField] private float m_FindNewTargetTime;

        [SerializeField] private float m_ShootDelay;

        [SerializeField] private float m_EvadeRayLength;

        [SerializeField] private Ship m_Ship;

        [SerializeField] private Vector3 m_MovePosition;

        [SerializeField] private Destructible m_SelectedTarget;


        private void Start()
        {
            m_Ship = GetComponent<Ship>();

            InitTimers();
        }

        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }

        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Null)
            {

            }

            if (m_AIBehaviour != AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
        }

        private void ActionFindNewMovePosition()
        {

        }

        private void ActionControlShip()
        {

        }

        private void ActionFindNewAttackTarget()
        {

        }

        private void ActionFire()
        {

        }

        #region Timers
        private void InitTimers()
        {

        }

        private void UpdateTimers()
        {

        }
        #endregion


    }
}