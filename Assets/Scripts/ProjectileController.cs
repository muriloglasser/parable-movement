using System.Collections;
using UnityEngine;
using UnityEditor;

public class ProjectileController : MonoBehaviour
{
    [Header("Gizmos")]
    public float sphereRadius = 0.5f;
    [Space(5)]
    [Header("Tile to move along path")]
    public Projectile tile;
    [Space(5)]
    [Header("Trajectory Points")]
    public Transform startPoint;
    public Transform controlPoint;
    public Transform endPoint;

    #region Unity
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchProjectile(startPoint.position, controlPoint.position, endPoint.position);
        }
    }
    #endregion   
    #region Core
    public void LaunchProjectile(Vector3 start, Vector3 controlPoint, Vector3 end)
    {
        if (tile.isPerformingPath)
            return;

        tile.SetTrajectoryPoints(start, controlPoint, end);

        if (tile.parableMode == ParableMode.Auto)
            this.controlPoint.position = tile.controlPoint;

        StartCoroutine(tile.LaunchProjectileRoutine());
    }
    #endregion
    #region Gizmos and Handles Drawing
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (startPoint == null || controlPoint == null || endPoint == null) return;

        Gizmos.color = Color.cyan;
        Vector3 previousPoint = startPoint.position;
        int segments = 20;

        for (int i = 1; i <= segments; i++)
        {
            float t = i / (float)segments;
            Vector3 pointA = Vector3.Lerp(startPoint.position, controlPoint.position, t);
            Vector3 pointB = Vector3.Lerp(controlPoint.position, endPoint.position, t);
            Vector3 currentPoint = Vector3.Lerp(pointA, pointB, t);

            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }

        Debug.DrawLine(startPoint.position, controlPoint.position, Color.red);
        Debug.DrawLine(controlPoint.position, endPoint.position, Color.red);

        DrawSelectableHandle(startPoint);
        DrawSelectableHandle(controlPoint);
        DrawSelectableHandle(endPoint);
    }
    private void DrawSelectableHandle(Transform point)
    {
        Handles.color = Color.red;

        if (Handles.Button(point.position, Quaternion.identity, sphereRadius, sphereRadius, Handles.SphereHandleCap))
        {
            Selection.activeTransform = point;
        }
    }
#endif
    #endregion
}
