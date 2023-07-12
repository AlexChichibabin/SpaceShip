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
        [SerializeField] private VirtualJoystick m_MobileJoystick;

        private ControlMode m_ControlMode;
    }
}
