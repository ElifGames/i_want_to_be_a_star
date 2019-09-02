using UnityEngine;

namespace IWantToBeAStar.MainGame
{
    public class DestroyByContact : GameManager
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Boundary")
            {
                return;
            }
            if (other.tag == "Player")
            {
                OnGameEndedEvent();
            }
            // Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}