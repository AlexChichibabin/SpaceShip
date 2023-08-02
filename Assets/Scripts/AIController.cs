using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

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
            m_Ship.ThrustControl = m_NavigationLinear;

            ComputeAlignTorqueNormalized(new Vector3(-15, 4, 0), m_Ship.transform);
        }

        private static float ComputeAlignTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            Debug.Log(angle);

            angle = Mathf.Clamp(angle, -45, 45) / 45;

            

            return -angle;
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