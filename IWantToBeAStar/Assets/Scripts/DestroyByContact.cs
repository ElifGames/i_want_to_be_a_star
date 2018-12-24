using UnityEngine;

namespace IWantToBeAStar
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
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}