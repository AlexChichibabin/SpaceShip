using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class LevelBoundary : SingletonBase<LevelBoundary>
    {
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        public enum Mode 
        { 
            Limit,
            Teleport
        }

        [SerializeField] private Mode m_Mode;
        public Mode LimitMode => m_Mode;

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);
        }
        #endif

    }
}
