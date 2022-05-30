using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    #region Inputs

    [SerializeField]
    private InputHandler gameInputHandler;

    private void InitInputEvents()
    {
        gameInputHandler.OnResetKeyPressed += ResetProcess;
    }


    private void DestroyInputEvents()
    {
        gameInputHandler.OnResetKeyPressed -= ResetProcess;
    }

    private void ResetProcess()
    {
        Debug.LogError("reset");
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
        Debug.LogError($"hover start {obj.name}");
        CubeSelected = true;
        LastSelectedCube = obj;
    }

    private void TargetCubeOnOnCubeHoverEnd(TargetCube obj)
    {
        Debug.LogError($"hover end {obj.name}");
        CubeSelected = false;
        LastSelectedCube = null;
    }

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


    /*
 * Задача: реализовать по клику на каждый из 6 кубов плавное перемещение по 
кривой безье 7мого куба к выбранному (в процессе перемещения куб принимает rotation 
цели),при нажатии кнопки пробел куб-7 возвращается на исходную позицию. 

Использование встроенной функции lerp нежелательно.
Для повышения ставки сразу предлагается усложнение.
1е усложнение: куб-7 ищет пути к своим целям через соседей пример (куб-7 
находится в исходной позиции по центру, целью выбран куб 1_1, куб 7 
передвигается к нему через куб 1_2. То есть движение происходит к цели через 
ближайший к нему куб.

 */
}