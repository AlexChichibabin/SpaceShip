using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShip
{
    public class UIStats : MonoBehaviour
    {
        enum ValueMode
        {
            Scores,
            Kills
        }
        
        [SerializeField] private Text m_Text;
        [SerializeField] private ValueMode m_ValueMode;

        private int m_LastScore;
        private int m_LastKills;

        private void Update()
        {
            if(m_ValueMode == ValueMode.Scores) UpdateScores();
            if (m_ValueMode == ValueMode.Kills) UpdateKills();
        }

        private void UpdateScores()
        {
            if (Player.Instance != null)
            {
                int currentScore = Player.Instance.Score;

                if (m_LastScore != currentScore)
                {
                    m_LastScore = currentScore;

                    m_Text.text = "Score: " + m_LastScore.ToString();
                }
            }
        }
        private void UpdateKills()
        {
            if (Player.Instance != null)
            {
                int currentKills = Player.Instance.NumKills;

                if (m_LastKills != currentKills)
                {
                    m_LastKills = currentKills;

                    m_Text.text = "Kills: " + m_LastKills.ToString();
                }
            }
        }
    }
}