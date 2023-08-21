using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShip
{

    public class PlayerShipSelectionController : MonoBehaviour
    {
        [SerializeField] private GameObject m_ShipSelectPanel;

        [SerializeField] private Ship m_Prefab;

        [SerializeField] private Text m_Shipname;
        [SerializeField] private Text m_Hitpoints;
        [SerializeField] private Text m_Speed;
        [SerializeField] private Text m_Agility;

        [SerializeField] private Image m_Preview;


        private void Start()
        {
            if (m_Shipname != null)
            {
                m_Shipname.text = m_Prefab.name;
                m_Hitpoints.text = "Hitpoints: " + m_Prefab.HitPoints.ToString();
                m_Speed.text = "Speed: " + m_Prefab.MaxLinearVelocity.ToString();
                m_Agility.text = "Agility: " + m_Prefab.MaxAngularVelocity.ToString();

            }   

            if (m_Preview != null) m_Preview.sprite = m_Prefab.GetComponentInChildren<SpriteRenderer>().sprite;
        }

        public void OnButtonSelectShip()
        {
            LevelSequenceController.PlayerShip = m_Prefab;

            MainMenuController.Instance.gameObject.SetActive(true);

            m_ShipSelectPanel.SetActive(false);
        }

        public void OnButtonBackFromShipSelection()
        {
            MainMenuController.Instance.gameObject.SetActive(true);

            m_ShipSelectPanel.SetActive(false);
        }
    }
}
