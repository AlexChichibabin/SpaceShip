using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    [RequireComponent(typeof(BackgroundElement))]
    public class SyncTransform : MonoBehaviour
    {
        private Ship m_ship;
        private Transform m_transform;

        private void Start()
        {
            m_transform = GetComponent<Transform>();
            m_ship = FindFirstObjectByType<Ship>();
        }

        private void Update()
        {
            m_transform.position = new Vector3(m_ship.transform.position.x, m_ship.transform.position.y, m_transform.position.z);
        }
    }
}
