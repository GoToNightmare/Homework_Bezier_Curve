using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    #region ResetAction

    private const KeyCode ResetKey = KeyCode.Space;
    public event Action OnResetKeyDown;

    private void ResetKeyDown()
    {
        try
        {
            OnResetKeyDown?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    #endregion


    #region Click

    private const int LeftClickKeyCode = 0;
    public event Action OnLeftClickDown;

    private void LeftClickDown()
    {
        try
        {
            OnLeftClickDown?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    #endregion


    #region Unity

    private void Update()
    {
        if (Input.GetKeyDown(ResetKey))
        {
            ResetKeyDown();
        }

        if (Input.GetMouseButtonDown(LeftClickKeyCode))
        {
            LeftClickDown();
        }
    }

    private void OnDestroy()
    {
        OnResetKeyDown = null;
        OnLeftClickDown = null;
    }

    #endregion
}