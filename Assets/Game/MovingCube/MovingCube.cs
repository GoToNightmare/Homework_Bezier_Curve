using System.Collections;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    private Coroutine CoroutineRef { get; set; }

    [SerializeField]
    private Vector3 InitialPosition;

    [SerializeField]
    private Vector3 MiddlePosition;

    [SerializeField]
    private Vector3 EndPosition;

    [SerializeField]
    private bool CoroutineActive;

    [SerializeField]
    [Range(0.01f, 1f)]
    private float Speed = 1f;

    private Quaternion InitialRotation;
    private Quaternion EndRotation;

    #region Unity

    private void Awake()
    {
        InitialPosition = transform.position;
        MiddlePosition = MiddlePosition == Vector3.zero ? InitialPosition + Vector3.down * 5 : MiddlePosition;

        InitialRotation = transform.rotation;
    }

    #endregion

    private void ResetPositionAndRotation()
    {
        Transform thisCubeTransform = transform;
        thisCubeTransform.position = InitialPosition;
        thisCubeTransform.rotation = InitialRotation;
    }

    /// <summary>
    /// https://en.wikibooks.org/wiki/Cg_Programming/Unity/B%C3%A9zier_Curves
    /// </summary>
    /// <returns></returns>
    private IEnumerator MovingCoroutine()
    {
        Vector3 p0 = InitialPosition;
        Vector3 p1 = MiddlePosition;
        Vector3 p2 = EndPosition;

        Quaternion rotationInitial = InitialRotation;
        Quaternion rotationEnd = EndRotation;
        float diffX = rotationEnd.x - rotationInitial.x;
        float diffY = rotationEnd.y - rotationInitial.y;
        float diffZ = rotationEnd.z - rotationInitial.z;
        float diffW = rotationEnd.w - rotationInitial.w;

        Transform thisCubeTransform = transform;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * Speed;
            t = Mathf.Clamp(t, 0, 1);

            float oneMinusT = 1.0f - t;
            Vector3 position = oneMinusT * oneMinusT * p0
                               + 2.0f * oneMinusT * t * p1
                               + t * t * p2;

            thisCubeTransform.position = position;

            thisCubeTransform.rotation = new Quaternion(rotationInitial.x + diffX * t, rotationInitial.y + diffY * t, rotationInitial.z + diffZ * t, rotationInitial.w + diffW * t);

            yield return null;
        }

        CoroutineActive = false;
    }


    public void LeftClick(TargetCube lastSelectedCube)
    {
        InitialRotation = transform.rotation;

        Transform selectedCubeTransform = lastSelectedCube.transform;
        EndPosition = selectedCubeTransform.position;
        EndRotation = selectedCubeTransform.rotation;

        if (!CoroutineActive)
        {
            CoroutineActive = true;
            CoroutineRef = StartCoroutine(MovingCoroutine());
        }
    }

    public void Reset()
    {
        if (CoroutineActive)
        {
            CoroutineActive = false;
            StopCoroutine(CoroutineRef);
        }

        ResetPositionAndRotation();
    }
}