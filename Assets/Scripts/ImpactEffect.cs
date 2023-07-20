using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class ImpactEffect : MonoBehaviour
    {    
        [SerializeField] private float m_LifeTime;

        private void Start()
        {
            Destroy(gameObject, m_LifeTime);
        }
    }
}

