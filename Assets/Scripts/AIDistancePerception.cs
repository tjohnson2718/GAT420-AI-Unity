using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIDistancePerception : AIPerception
{
    public override GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, distance);
        foreach (Collider collider in colliders)
        {
            // check if collision is self, skip if so
            if (collider.gameObject == gameObject) continue;

            if (tagName == "" || collider.CompareTag(tagName))
            {
                // calculate angle from transform forward vector to direction of game object
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                float angle = Vector3.Angle(transform.forward, direction);
                // if angle is less than max angle, add game object
                if (angle <= maxAngle)
                {
                    result.Add(collider.gameObject);
                }
            }
        }

        return result.ToArray();
    }
}
