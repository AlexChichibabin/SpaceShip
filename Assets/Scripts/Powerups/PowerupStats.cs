using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class PowerupStats : Powerup
    {
        public enum EffectType
        {
            AddAmmo,
            AddEnergy,
            Indestructibility,
            Acceleration
        }

        [SerializeField] private EffectType m_EffectType;

        [SerializeField] private float m_Value;

        [SerializeField] private float m_EffectDuration;

        protected override void OnPickedUp(Ship ship)
        {
            if (m_EffectType == EffectType.AddEnergy)
            {
                ship.AddEnergy((int)m_Value);
            }

            if (m_EffectType == EffectType.AddAmmo)
            {
                ship.AddAmmo((int)m_Value);
            }

            if (m_EffectType == EffectType.Indestructibility)
            {
                ship.SetIndestructibilityTimer(m_EffectDuration);
                ship.SetIndestructibility(true);
            }
            if (m_EffectType == EffectType.Acceleration)
            {
                ship.SetAccelerationTimer(m_EffectDuration);
                ship.AddThrust((int)m_Value);
            }
        }
    }
}