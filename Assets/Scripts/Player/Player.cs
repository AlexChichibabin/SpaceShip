using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int m_NumLives;
        [SerializeField] private Ship m_Ship;
        [SerializeField] private GameObject m_PlayerShipPrefab;
        [HideInInspector] public Ship ActiveShip => m_Ship;

        [SerializeField] private CameraController m_CameraController;
        [SerializeField] private MovementController m_MovementController;

        private void Start()
        {
            m_Ship.EventOnDeath.AddListener(OnShipDeath);
        }

        private void OnShipDeath()
        {
            m_NumLives--;

            if (m_NumLives > 0)
            {
                Respawn();
            }
        }

        private void Respawn()
        {
            var newPlayerShip = Instantiate(m_PlayerShipPrefab);

            m_Ship = newPlayerShip.GetComponent<Ship>();
            m_CameraController.SetTarget(m_Ship.transform);
            m_MovementController.SetTargetShip(m_Ship);
            m_Ship.EventOnDeath.AddListener(OnShipDeath);
        }

    }
}
