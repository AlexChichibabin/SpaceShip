using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SpaceShip
{
    [RequireComponent(typeof(Ship))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol,
            PointPatrol
        }

        [SerializeField] private AIBehaviour m_AIBehaviour;

        [SerializeField] private AIPointPatrol m_PatrolPoint;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        [SerializeField] private float m_RandomSelectMovePointTime;

        [SerializeField] private float m_FindNewTargetTime;

        [SerializeField] private float m_EvadeCollisionTime;

        [SerializeField] private float m_ShootDelay;

        [SerializeField] private float m_EvadeRadius;

        [SerializeField] private float m_ShootDistance;

        [SerializeField] private float m_EnemyDetectDistance;

        //[SerializeField] private Vector3[] m_SpecifiedPositions;

        private int m_PatrolPointNumber;

        [SerializeField] private int m_PatrolPointAccuracy;

        private Ship m_Ship;

        private Vector3 m_MovePosition;

        private Destructible m_SelectedTarget;

        private Timer m_RandomizeDirectionTimer;
        private Timer m_EvadeCollisionTimer;
        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;


        private void Start()
        {
            m_Ship = GetComponent<Ship>();

            InitTimers();

            InitPatrolPoint();
        }

        private void Update()
        {
            UpdateTimers();

            UpdateAI();

            
        }

        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Null)
            {

            }

            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }

            if (m_AIBehaviour == AIBehaviour.PointPatrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionEvadeCollision();
        }

        private void FixedUpdate()
        {
            //Debug.Log(m_MovePosition);
        }




        private void ActionFindNewMovePosition()
        {
            if (m_AIBehaviour == AIBehaviour.PointPatrol)
            {
                if (m_SelectedTarget != null) // ���� ���� ���� �����
                {
                    m_MovePosition = m_SelectedTarget.transform.position;
                }
                else // ���� ��� ���� �����
                {
                    //Debug.Log("SelectedTarget" + m_SelectedTarget);
                    {
                        if ((m_Ship.transform.position - m_MovePosition).magnitude < m_PatrolPointAccuracy) // ���� ������� ���������� ������ � �����, ����� ������� ���������
                        {
                            if (m_PatrolPoint.SpecifiedPositions.Length > 1) // ���� � ��������� ������� �� 2� ����� � ������
                            {
                                m_PatrolPointNumber++;
                                if (m_PatrolPointNumber == m_PatrolPoint.SpecifiedPositions.Length) m_PatrolPointNumber = 0;
                                m_MovePosition = m_PatrolPoint.SpecifiedPositions[m_PatrolPointNumber];
                                //Debug.Log("MovePosition" + m_MovePosition);
                            }
                        }
                        if (m_EvadeCollisionTimer.IsFinished == true && m_MovePosition != m_PatrolPoint.SpecifiedPositions[m_PatrolPointNumber]) // ���� ������ ������, � ������� �������� �� � ������ ����� ��������������
                        {
                            m_MovePosition = m_PatrolPoint.SpecifiedPositions[m_PatrolPointNumber];
                            m_RandomizeDirectionTimer.Start(m_FindNewTargetTime);
                        }
                    } 
                }
            }

            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_SelectedTarget != null) // ���� ���� ���� �����
                {
                    m_MovePosition = m_SelectedTarget.transform.position;
                    Debug.Log("SelectedTarget" + m_SelectedTarget);
                }
                else // ���� ��� ���� �����
                {
                    if (m_PatrolPoint != null)
                    {
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - m_Ship.transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;

                        //Debug.Log("PatrolZone" + isInsidePatrolZone);
                        

                        if (isInsidePatrolZone == true)
                        {
                            if (m_RandomizeDirectionTimer.IsFinished == true)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;

                                m_MovePosition = newPoint;

                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                        }
                        else
                        {
                            m_MovePosition = m_PatrolPoint.transform.position;
                        }
                    }
                }
            }
        }

        private void InitPatrolPoint() // ���������������� ����� �������������� ��� ������
        {
            if (m_PatrolPoint.SpecifiedPositions.Length > 1) // ���� � ��������� ������� �� 2� ����� � ������
            {
                m_PatrolPointNumber = m_PatrolPoint.SpecifiedPositions.Length - 1;
                m_PatrolPointNumber++;
                if (m_PatrolPointNumber == m_PatrolPoint.SpecifiedPositions.Length) m_PatrolPointNumber = 0;
                m_MovePosition = m_PatrolPoint.SpecifiedPositions[m_PatrolPointNumber];
            }
        }

        private void ActionEvadeCollision()
        {
            Collider2D hit = Physics2D.OverlapCircle(m_Ship.transform.position, m_EvadeRadius);

            //float angle = ComputeAlignTorqueNormalized(hit.gameObject.transform.position, m_Ship.transform, 90);

            Vector2 localTargetPosition = m_Ship.transform.InverseTransformPoint(hit.transform.position);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -90, 90);


            Debug.Log(angle);

            if (hit != null && m_EvadeCollisionTimer.IsFinished == true)
            {
                if (angle > 0)
                {
                    m_MovePosition = transform.position - transform.right * 0.5f * angle;
                }
                if (angle < 0)
                {
                    m_MovePosition = transform.position + transform.right * 0.5f * angle;
                }
                m_EvadeCollisionTimer.Start(m_EvadeCollisionTime);
            }

            /*if (m_EvadeCollisionTimer.IsFinished == true)
            {
                //hit = null;
                m_EvadeCollisionTimer.Start(m_EvadeCollisionTime);
            }*/
        }


        private void ActionControlShip()
        {
            m_Ship.ThrustControl = m_NavigationLinear;

            m_Ship.TorqueControl = ComputeAlignTorqueNormalized(m_MovePosition, m_Ship.transform, MAX_ANGLE) * m_NavigationAngular;
        }

        private const float MAX_ANGLE = 45.0f;
        private static float ComputeAlignTorqueNormalized(Vector3 targetPosition, Transform ship, float maxAngle)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -maxAngle, maxAngle) / maxAngle;

            //Debug.Log(-angle);

            return -angle;
        }

        private void ActionFindNewAttackTarget()
        {
            if (m_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindNearestDestructibleTarget();

                m_FindNewTargetTimer.Start(m_FindNewTargetTime);
            }
        }

        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = m_EnemyDetectDistance;

            Destructible potentialTarget = null;

            if (m_SelectedTarget != null) //��������� ������� ����
            {
                potentialTarget = m_SelectedTarget; 
                return potentialTarget;
            }     

            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<Destructible>() == m_Ship) continue;

                if (v.TeamId == Destructible.TeamIdNeutral) continue;

                if (v.TeamId == m_Ship.TeamId) continue;

                float dist = Vector2.Distance(m_Ship.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }
            return potentialTarget;
        }

        private void ActionFire()
        {
            if (m_SelectedTarget != null)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, m_ShootDistance); // �������� ��� ������� ����������
                
                if (hit)
                {
                    Destructible barrier = hit.collider.transform.root.GetComponent<Destructible>();

                    if (barrier != null && barrier == m_SelectedTarget)
                    {
                        if (m_FireTimer.IsFinished == true)
                        {
                            m_Ship.Fire(TurretMode.Primary);

                            m_FireTimer.Start(m_ShootDelay);
                        }
                    }
                }
            }
        }

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoint = point;
        }

        #region Timers
        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(0);
            m_FireTimer = new Timer(0);
            m_FindNewTargetTimer = new Timer(0);
            m_EvadeCollisionTimer = new Timer(0);
        }

        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
            m_EvadeCollisionTimer.RemoveTime(Time.deltaTime);
        }
        #endregion


    }
}