using UnityEngine;

namespace IWantToBeAStar.MainGame
{
    public class DestroyByContact : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Boundary")
            {
                return;
            }
            if (other.tag == "Player")
            {
                GameData.IsGameEnd = true;
            }
            // Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}