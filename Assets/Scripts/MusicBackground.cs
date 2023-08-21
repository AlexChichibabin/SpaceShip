using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShip
{
    public class MusicBackground : SingletonBase<MusicBackground>
    {
        [SerializeField] private AudioSource m_Sourse;
        [SerializeField] private AudioClip m_Music;

        private void Start()
        {
            m_Sourse.clip = m_Music;
            m_Sourse.Play();
        }
    }
}