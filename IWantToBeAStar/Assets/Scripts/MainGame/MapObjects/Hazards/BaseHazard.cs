using UnityEngine;

public class BaseHazard : MonoBehaviour
{
    private void Start()
    {
        HazardStart();
    }

    private void Awake()
    {
        HazardAwake();
    }

    /// <summary>
    /// base.HazardStart()를 먼저 사용 후 사용하세요.
    /// </summary>
    protected virtual void HazardStart()
    {
    }

    protected virtual void HazardAwake()
    {
    }
}