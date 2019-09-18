using UnityEngine;

/// <summary>
/// Hazard 기본 클래스입니다.
/// </summary>
public abstract class BaseHazard : MonoBehaviour
{
    private void Start()
    {
        // Hazard 공통으로 실행되는 코드 추가하기
        HazardStart();
    }

    private void Awake()
    {
        HazardAwake();
    }

    private void Update()
    {
        HazardUpdate();
    }

    private void FixedUpdate()
    {
        HazardFixedUpdate();
    }

    protected abstract void HazardStart();

    protected abstract void HazardAwake();

    protected abstract void HazardUpdate();

    protected abstract void HazardFixedUpdate();
}