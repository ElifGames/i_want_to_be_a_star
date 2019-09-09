using IWantToBeAStar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHazard : MonoBehaviour
{
    void Start()
    {
        HazardStart();
    }

    void Awake()
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
