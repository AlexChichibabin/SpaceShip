using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class MainMenuController : SingletonBase<MainMenuController>
    {
        [SerializeField] private GameObject m_EpisodeSelectionPanel;
        [SerializeField] private GameObject m_ShipSelectionPanel;
        [SerializeField] private Ship m_DefaultShip;

        private void Start()
        {
            LevelSequenceController.PlayerShip = m_DefaultShip;
        }

        public void OnButtonStartNewGame()
        {
            m_EpisodeSelectionPanel.SetActive(true);
            gameObject.SetActive(false);
        }
        public void OnButtonSelectShip()
        {
            m_ShipSelectionPanel.SetActive(true);
            gameObject.SetActive(false);
        }
        public void OnButtonExit()
        {
            Application.Quit();
        }
    }
}