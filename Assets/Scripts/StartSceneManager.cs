using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class StartSceneManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_Shared;

        private void Awake()
        {
            
        }

        void Start()
        {
            m_Shared.SetActive(true);
        }
    }
}