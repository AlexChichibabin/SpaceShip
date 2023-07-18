using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";

        [SerializeField] private float m_ConstantDamage;
        [SerializeField] private float m_VelocityDamageModifier;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag) return;

            var destructible = transform.root.GetComponent<Destructible>();

            if (destructible != null)
            {
                destructible.ApplyDamage((int)(m_ConstantDamage + collision.relativeVelocity.magnitude * m_VelocityDamageModifier));
            }
        }
    }
}
