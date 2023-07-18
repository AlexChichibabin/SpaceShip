using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    [RequireComponent(typeof(BackgroundElement))]
    public class SyncTransform : MonoBehaviour
    {
        [SerializeField] private Transform m_Target;

        private void Update()
        {
            transform.position = new Vector3(m_Target.transform.position.x, m_Target.transform.position.y, transform.position.z);
        }
    }
}
