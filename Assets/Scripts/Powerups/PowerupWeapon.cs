using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class PowerupWeapon : Powerup
    {
        [SerializeField] private TurretProperties m_Properties;
        protected override void OnPickedUp(Ship ship)
        {
            ship.AssignWeapon(m_Properties);
        }
    }
}