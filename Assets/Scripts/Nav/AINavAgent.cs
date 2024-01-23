using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AINavPath))]
public class AINavAgent : AIAgent
{
	[SerializeField] private AINavPath path;

	void Update()
	{
		if (path.HasPath())
		{
			Debug.DrawLine(transform.position, path.destination);
			movement.MoveTowards(path.destination);
		}
	}
}
