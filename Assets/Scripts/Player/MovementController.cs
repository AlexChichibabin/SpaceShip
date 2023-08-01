using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class MovementController : MonoBehaviour
    {
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }
        [SerializeField] private Ship m_TargetShip;
        public void SetTargetShip(Ship ship) => m_TargetShip = ship;

        [SerializeField] private VirtualJoystick m_MobileJoystick;

        [SerializeField] private ControlMode m_ControlMode;

        [SerializeField] private PointerClickHold m_MobileFirePrimary;
        [SerializeField] private PointerClickHold m_MobileFireSecondary;

        private void Start()
        {
            if (m_TargetShip == null)
            {
                return;
            }
            
            if (m_ControlMode == ControlMode.Keyboard)
            {
                m_MobileJoystick.gameObject.SetActive(false);

                m_MobileFirePrimary.gameObject.SetActive(false);
                m_MobileFireSecondary.gameObject.SetActive(false);
            }
            if (m_ControlMode == ControlMode.Mobile)
            {
                m_MobileJoystick.gameObject.SetActive(true);

                m_MobileFirePrimary.gameObject.SetActive(true);
                m_MobileFireSecondary.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (m_ControlMode == ControlMode.Keyboard)
            {
                ControlKeyboard();
            }
            if (m_ControlMode == ControlMode.Mobile)
            {
                ControlMobile();
            }
        }

        private void ControlMobile()
        {
            /*Vector3 dir = m_MobileJoystick.Value;

            var dot = Vector2.Dot(dir, m_TargetShip.transform.up);
            var dot2 = Vector2.Dot(dir, m_TargetShip.transform.right);

            m_TargetShip.ThrustControl = Mathf.Max(0, dot);
            m_TargetShip.TorqueControl = -dot2;*/

            {
                var dir = m_MobileJoystick.Value;
                m_TargetShip.ThrustControl = dir.y;
                m_TargetShip.TorqueControl = -dir.x;
            }

            if (m_MobileFirePrimary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }
            if (m_MobileFireSecondary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }
        }

        private void ControlKeyboard()
        {
            float thrust = 0;
            float torque = 0;

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                thrust = 1.0f;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                thrust = -1.0f;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                torque = 1.0f;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                torque = -1.0f;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }
            if (Input.GetKey(KeyCode.V))
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }
            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;
        }
    }
}
