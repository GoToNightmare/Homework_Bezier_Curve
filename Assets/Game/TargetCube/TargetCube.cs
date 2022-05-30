using System;
using UnityEngine;

public class TargetCube : MonoBehaviour
{
    [SerializeField]
    private Material stateActive;

    [SerializeField]
    private Material stateInactive;


    public event Action<TargetCube> OnCubeHoverStart;
    public event Action<TargetCube> OnCubeHoverEnd;


    private MeshRenderer MaterialRef { get; set; }

    #region Unity

    private void Awake()
    {
        MaterialRef = GetComponent<MeshRenderer>();
    }

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

        MaterialRef.material = stateActive;
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

        MaterialRef.material = stateInactive;
    }

    #endregion
}