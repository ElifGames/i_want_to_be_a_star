using UnityEngine;

namespace IWantToBeAStar.MainGame
{
    public class DestroyByBoundary : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            Destroy(other.gameObject);
        }
    }
}