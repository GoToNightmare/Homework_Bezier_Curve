using System;
using UnityEngine;

public class TargetCube : MonoBehaviour
{
    public event Action<TargetCube> OnCubeHoverStart;
    public event Action<TargetCube> OnCubeHoverEnd;

    #region Unity

    private void OnDestroy()
    {
        OnCubeHoverStart = null;
        OnCubeHoverEnd = null;
    }

    private void OnMouseEnter()
    {
        try
        {
            OnCubeHoverStart?.Invoke(this);
        }
        catch (Exception e)
        {
            Debug.LogError(e, this);
        }
    }

    private void OnMouseExit()
    {
        try
        {
            OnCubeHoverEnd?.Invoke(this);
        }
        catch (Exception e)
        {
            Debug.LogError(e, this);
        }
    }

    #endregion
}