using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Parable mode")]
    public ParableMode parableMode = ParableMode.Custom;
    [Space(5)]
    [Header("Projectile Settings")]
    public float speed = 1f;
    public Rigidbody body;
    [Space(5)]
    [Header("Auto mode properties")]
    public float yOffset = 1f;
    ///
    public bool isPaused;
    [HideInInspector]
    public Vector3 startPoint;
    [HideInInspector]
    public Vector3 controlPoint;
    [HideInInspector]
    public Vector3 endPoint;
    [HideInInspector]
    public bool isPerformingPath = false;


    #region Pause Management
    public void SetPause(bool pauseState)
    {
        isPaused = pauseState;
    }
    public float GetPauseFactor() => isPaused ? 0 : 1;
    #endregion
    #region Core  
    public void SetTrajectoryPoints(Vector3 start, Vector3 controlPoint, Vector3 end)
    {
        startPoint = start;

        if (parableMode == ParableMode.Auto)
            this.controlPoint = (start + end + Vector3.up * yOffset) / 2;
        else if (parableMode == ParableMode.Custom)
            this.controlPoint = controlPoint;

        endPoint = end;
    }
    public IEnumerator LaunchProjectileRoutine()
    {
        isPerformingPath = true;
        body.angularVelocity = Vector2.zero;
        body.velocity = Vector2.zero;
        body.useGravity = false;
        body.position = startPoint;

        float progress = 0f;
        Vector3 start = startPoint;
        Vector3 control = controlPoint;
        Vector3 end = endPoint;

        while (progress < 1f)
        {
            yield return new WaitForFixedUpdate();
            body.angularVelocity = Vector2.zero;

            progress += speed * GetPauseFactor() * Time.deltaTime;
            Vector3 pointA = Vector3.Lerp(start, control, progress);
            Vector3 pointB = Vector3.Lerp(control, end, progress);
            Vector3 nextPosition = Vector3.Lerp(pointA, pointB, progress);

            if (Vector3.Distance(body.position, end) < 0.001f)
                break;

            transform.LookAt(nextPosition);  
            body.MovePosition(nextPosition);     
        }

        body.useGravity = true;
        isPerformingPath = false;
    }
    #endregion
}

public enum ParableMode
{
    Auto,
    Custom
}