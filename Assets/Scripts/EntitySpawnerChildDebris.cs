using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    public class EntitySpawnerChildDebris : MonoBehaviour
    {
        [SerializeField] private Destructible m_DebrisPrefab;
        [SerializeField] private Vector3 m_ChildScale;
        [SerializeField] private float m_RandomSpeed;
        [SerializeField] private float m_MaxDebrisTorque;
        private Destructible m_ParentDebris;

        private void Start()
        {
            m_ParentDebris = GetComponent<Destructible>();
            m_ParentDebris.EventOnDeath.AddListener(OnParentDebrisDead);
        }

        private void OnParentDebrisDead()
        {
            SpawnChildDebris(m_ChildScale);
        }

        private void SpawnChildDebris(Vector3 scale)
        {
            for (int i = 0; i < Random.Range(2, 4); i++)
            {
                GameObject debris = Instantiate(m_DebrisPrefab.gameObject);

                debris.transform.position = transform.position;
                debris.transform.localScale = scale;
                debris.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));

                debris.GetComponent<LevelBoundaryLimiter>().enabled = true;
                debris.GetComponent<EntitySpawnerChildDebris>().enabled = false;
                debris.GetComponentInChildren<Collider2D>().enabled = true;
                Destructible debrisDestr = debris.GetComponent<Destructible>();
                debrisDestr.enabled = true;

                Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();

                if (rb != null && m_RandomSpeed > 0)
                {
                    rb.velocity = (Vector2)UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
                    rb.AddTorque(Random.Range(-m_MaxDebrisTorque, m_MaxDebrisTorque));
                }
            }
        }
    }
}