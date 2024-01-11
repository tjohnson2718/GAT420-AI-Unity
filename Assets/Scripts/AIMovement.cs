using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIMovement : MonoBehaviour
{
    [Range(1, 10)] public float maxSpeed = 5;
    [Range(1, 10)] public float minSpeed = 5;
    [Range(1, 100)] public float maxForce = 5;
    [Range(1, 360)] public float turnRate = 90;

    public virtual Vector3 Velocity { get; set; } = Vector3.zero;
    public virtual Vector3 Acceleration { get; set; } = Vector3.zero;
    public virtual Vector3 Direction { get { return Velocity.normalized; } }
    public virtual Vector3 Destination { get; set; } = Vector3.zero;

    public abstract void ApplyForce(Vector3 force);
    public abstract void MoveTowards(Vector3 target);
    public abstract void Stop();
    public abstract void Resume();
}
