using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class PlayerStatistics : MonoBehaviour
    {
        public int NumKills;
        public int Scores;
        public int Time;

        public void Reset()
        {
            NumKills = 0;
            Scores = 0;
            Time = 0;
        }
    }
}