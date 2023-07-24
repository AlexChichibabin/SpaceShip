using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class Projectile : Entity
    {
        [SerializeField] private float m_Velocity;
        [SerializeField] private float m_LifeTime;
        [SerializeField] private int m_Damage;
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        private float m_Timer;

        private void Update()
        {
            float stepLenght = m_Velocity * Time.deltaTime;
            Vector2 step = transform.up * stepLenght;

            m_Timer += Time.deltaTime;
            if (m_Timer > m_LifeTime) Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }
    }
}