using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float speed = 1f;
    public float yOffset = 1f;
    public Vector3 controllPointPosition;
    public Rigidbody body;
    [HideInInspector]
    public bool isPaused;
    ///
    [HideInInspector]
    public Vector3 startPoint;
    [HideInInspector]
    public Vector3 controlPoint;
    [HideInInspector]
    public Vector3 endPoint;

    #region Pause Management
    public void SetPause(bool pauseState)
    {
        isPaused = pauseState;
    }
    public float GetPauseFactor() => isPaused ? 0 : 1;
    #endregion
    #region Core  
    public void SetTrajectoryPoints(Vector3 start, Vector3 end)
    {
        startPoint = start;

        if (yOffset != 0)
            controlPoint = (start + end + Vector3.up * yOffset) / 2;
        else
            controlPoint = controllPointPosition;

        endPoint = end;
    }
    public IEnumerator LaunchProjectileRoutine()
    {        
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
            yield return null;
            body.angularVelocity = Vector2.zero;

            progress += speed * GetPauseFactor() * Time.deltaTime;
            Vector3 pointA = Vector3.Lerp(start, control, progress);
            Vector3 pointB = Vector3.Lerp(control, end, progress);
            Vector3 nextPosition = Vector3.Lerp(pointA, pointB, progress);

            if (Vector3.Distance(body.position, end) < 0.001f)
                break;

            transform.LookAt(nextPosition);  // Orienta o objeto para a nova posição.
            body.MovePosition(nextPosition);      // Move usando Rigidbody.
        }

        body.useGravity = true;
    }
    #endregion
}
