using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    #region ResetAction

    private const KeyCode ResetKey = KeyCode.Space;
    public event Action OnResetKeyPressed;

    private void ResetKeyPressed()
    {
        try
        {
            OnResetKeyPressed?.Invoke();
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
            ResetKeyPressed();
        }
    }

    private void OnDestroy()
    {
        OnResetKeyPressed = null;
    }

    #endregion
}