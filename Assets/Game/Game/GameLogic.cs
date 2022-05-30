using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    #region Inputs

    [SerializeField]
    private InputHandler gameInputHandler;

    private void InitInputEvents()
    {
        gameInputHandler.OnResetKeyDown += ResetProcess;
        gameInputHandler.OnLeftClickDown += LeftClick;
    }


    private void DestroyInputEvents()
    {
        gameInputHandler.OnResetKeyDown -= ResetProcess;
        gameInputHandler.OnLeftClickDown -= LeftClick;
    }

    private void LeftClick()
    {
        if (CubeSelected)
        {
            movingCubeRef.LeftClick(LastSelectedCube);
        }
    }

    private void ResetProcess()
    {
        movingCubeRef.Reset();
    }

    #endregion


    #region OnCubeHover

    private bool CubeSelected { get; set; }
    private TargetCube LastSelectedCube { get; set; }

    [SerializeField]
    private List<TargetCube> AllTargetCubes = new List<TargetCube>();

    private void InitCubeEvents()
    {
        foreach (TargetCube targetCube in AllTargetCubes)
        {
            targetCube.OnCubeHoverStart += TargetCubeOnOnCubeHoverStart;
            targetCube.OnCubeHoverEnd += TargetCubeOnOnCubeHoverEnd;
        }
    }

    private void DestroyCubeEvents()
    {
        foreach (TargetCube targetCube in AllTargetCubes)
        {
            targetCube.OnCubeHoverStart -= TargetCubeOnOnCubeHoverStart;
            targetCube.OnCubeHoverEnd -= TargetCubeOnOnCubeHoverEnd;
        }
    }

    private void TargetCubeOnOnCubeHoverStart(TargetCube obj)
    {
        CubeSelected = true;
        LastSelectedCube = obj;
    }

    private void TargetCubeOnOnCubeHoverEnd(TargetCube obj)
    {
        CubeSelected = false;
        LastSelectedCube = null;
    }

    #endregion


    #region MovingCube

    [SerializeField]
    private MovingCube movingCubeRef;

    #endregion


    #region Unity

    private void Awake()
    {
        InitCubeEvents();
        InitInputEvents();
    }

    private void OnDestroy()
    {
        DestroyCubeEvents();
        DestroyInputEvents();
    }

    #endregion
}