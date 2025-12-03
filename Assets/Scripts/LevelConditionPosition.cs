using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class LevelConditionPosition : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private CircleArea m_Area;
        [SerializeField] private float m_StayInCollisionTime;

        private Timer m_StayInCollisionTimer;
        private bool m_Reached;

        private void Start()
        {
            m_StayInCollisionTimer = new Timer(0);
            m_StayInCollisionTimer.Start(m_StayInCollisionTime);
        }
        private void Update()
        {
            if (Player.Instance != null && Player.Instance.ActiveShip != null)
            {
                if ((Player.Instance.ActiveShip.transform.position - gameObject.transform.position).sqrMagnitude <= m_Area.Radius * m_Area.Radius)
                {
                    m_StayInCollisionTimer.RemoveTime(Time.deltaTime);
                }
                else m_StayInCollisionTimer.Start(m_StayInCollisionTime);
            }
        }
        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null && m_Area != null)
                {                 
                    if ((Player.Instance.ActiveShip.transform.position - gameObject.transform.position).sqrMagnitude <= m_Area.Radius * m_Area.Radius && m_StayInCollisionTimer.IsFinished == true)
                    {
                        m_Reached = true;
                    }
                }
                return m_Reached;
            }
        }
    }
}