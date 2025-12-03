using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class LevelConditionScore : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private int score;
        [SerializeField] private int numKills;

        private bool m_Reached;

        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    if (Player.Instance.NumKills >= numKills || Player.Instance.NumKills >= numKills)
                    {
                        m_Reached = true; 
                    }
                }
                return m_Reached;
            }
        }
    }
}