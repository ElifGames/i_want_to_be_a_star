using UnityEngine;

namespace IWantToBeAStar.MainGame
{
    public class DestroyByContact : MonoBehaviour
    {
        GameManager gameManager;
        private void Start()
        {
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Boundary")
            {
                return;
            }
            if (other.tag == "Player")
            {
                gameManager.PlayerHasDead();
            }
            // Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}