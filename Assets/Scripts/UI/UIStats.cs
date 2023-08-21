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
            Kills,
            Energy,
            Ammo,
            Health
        }
        
        [SerializeField] private Text m_Text;
        [SerializeField] private ValueMode m_ValueMode;

        private int m_LastScore;
        private int m_LastKills;
        private int m_LastEnergy;
        private int m_LastAmmo;
        private int m_LastHitPoints;

        private int MaxEnergy;
        private int MaxAmmo;
        private int MaxHitPoints;

        private void Start()
        {
            MaxEnergy = (int)Player.Instance.ActiveShip.MaxEnergy;
            MaxAmmo = Player.Instance.ActiveShip.MaxAmmo;
            MaxHitPoints = Player.Instance.ActiveShip.HitPoints;
        }
        private void Update()
        {
            if(m_ValueMode == ValueMode.Scores) UpdateScores();
            if (m_ValueMode == ValueMode.Kills) UpdateKills();
            if (m_ValueMode == ValueMode.Energy) UpdateEnergy();
            if (m_ValueMode == ValueMode.Ammo) UpdateAmmo();
            if (m_ValueMode == ValueMode.Health) UpdateHealth();
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
        private void UpdateEnergy()
        {
            if (Player.Instance != null)
            {
                int currentEnergy = (int)Player.Instance.ActiveShip.PrimaryEnergy;

                if (m_LastEnergy != currentEnergy)
                {
                    m_LastEnergy = currentEnergy;

                    m_Text.text = "Energy: " + m_LastEnergy.ToString() + " / " + MaxEnergy;
                }
            }
        }
        private void UpdateAmmo()
        {
            if (Player.Instance != null)
            {
                int currentAmmo = (int)Player.Instance.ActiveShip.SecondaryAmmo;

                if (m_LastAmmo != currentAmmo)
                {
                    m_LastAmmo = currentAmmo;

                    m_Text.text = "Ammo: " + m_LastAmmo.ToString() + " / " + MaxAmmo;
                }
            }
        }
        private void UpdateHealth()
        {
            if (Player.Instance != null)
            {
                int currentHiPoints = (int)Player.Instance.ActiveShip.CurrentHitPoints;

                if (m_LastHitPoints != currentHiPoints)
                {
                    m_LastHitPoints = currentHiPoints;

                    m_Text.text = "Health: " + m_LastHitPoints.ToString() + " / " + MaxHitPoints;
                }
            }
        }
    }
}