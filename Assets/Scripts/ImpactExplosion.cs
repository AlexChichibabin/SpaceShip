using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class ImpactExplosion : ImpactEffect
    {
        [SerializeField] private int m_Damage;
        [SerializeField] private float m_Radius;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, m_Radius);

            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i])
                {
                    Destructible dest = hit[i].transform.root.GetComponent<Destructible>();

                    if (dest != null && dest != m_Parent)
                    {
                        dest.ApplyDamage(m_Damage);
                    }
                }
            }
        }

        private Destructible m_Parent;
        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }
    }
#endif
}
