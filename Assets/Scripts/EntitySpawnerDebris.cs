using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class EntitySpawnerDebris : MonoBehaviour
    {
        [SerializeField] private Destructible[] m_DebrisPrefabs;

        [SerializeField] private CircleArea m_Area;

        [SerializeField] private int m_NumDebris;

        [SerializeField] private float m_RandomSpeed;

        [SerializeField] private float m_MaxDebrisTorque;

        private void Start()
        {
            for (int i = 0; i < m_NumDebris; i++)
            {
                SpawnDebris();
            }

        }

        private void SpawnDebris()
        {
            int index = Random.Range(0, m_DebrisPrefabs.Length);

            GameObject debris = Instantiate(m_DebrisPrefabs[index].gameObject);

            debris.transform.position = m_Area.GetRandomInsideZone();
            debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);
            //debris.AddComponent<LevelBoundaryLimiter>();
            debris.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
                //rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360) * i));

            Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();

            if (rb != null && m_RandomSpeed > 0)
            {
                rb.velocity = (Vector2)UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
                rb.AddTorque(Random.Range(-m_MaxDebrisTorque, m_MaxDebrisTorque));
            }
        }



        private void OnDebrisDead()
        {
            SpawnDebris();
        }
    }
}
