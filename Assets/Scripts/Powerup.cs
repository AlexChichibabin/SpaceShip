using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShip
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Powerup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Ship ship = collision.transform.root.GetComponent<Ship>();

            if (ship != null && Player.Instance.ActiveShip)
            {
                OnPickedUp(ship);
                Destroy(gameObject);
            }
        }
        protected abstract void OnPickedUp(Ship ship);
    }
}