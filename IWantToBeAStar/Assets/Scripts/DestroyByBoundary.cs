using UnityEngine;

namespace IWantToBeAStar
{
    public class DestroyByBoundary : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            Destroy(other.gameObject);
        }
    }
}