using UnityEngine;


namespace SpaceShip 
{ 
/// <summary>
/// some destructible object that can own hitpoints
/// </summary>
public class Destructible : Entity
{
    #region Properties
    /// <summary>
    /// object ignores damages
    /// </summary>
    [SerializeField] private bool m_Indestructible;
    public bool IsIndestructible => m_Indestructible;

    /// <summary>
    /// start hitpoints amount
    /// </summary>
    [SerializeField] private int m_HitPoints;

    /// <summary>
    /// current hitpoints amount
    /// </summary>
    private int m_CurrentHitPoints;
    public int CurrentHitPoints => m_CurrentHitPoints;

    #endregion

    #region Unity Events

    protected virtual void Start()
    {
        m_CurrentHitPoints = m_HitPoints;
    }

    #endregion

    #region Public API

    /// <summary>
    /// apply damage to object
    /// </summary>
    /// <param name="damage"></param>
    public void ApplyDamage(int damage)
    {
        if(m_Indestructible) return;

        m_CurrentHitPoints -= damage;

        if (m_CurrentHitPoints <= 0)
        {
            OnDeath();
        }
    }

    #endregion

    /// <summary>
    /// virtual event of object destroy, when current hitpoints becomes below or equal zero
    /// </summary>
    protected virtual void OnDeath()
    {
        Destroy(gameObject);
    }

}
}
