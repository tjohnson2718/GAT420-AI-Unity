using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AINavPath))]
public class AINavAgent : AIAgent
{
	[SerializeField] private AINavPath path;

	void Update()
	{
		if (path.HasTarget())
		{
			Debug.DrawLine(transform.position, path.destination);
			movement.MoveTowards(path.destination);
		}
	}

	#region AI_NAVNODE
	public AINavNode GetNearestAINavNode()
	{
		var nodes = AINavNode.GetAINavNodes().ToList();
		SortByDistance(nodes);

		return (nodes.Count == 0) ? null : nodes[0];
	}

	public AINavNode GetNearestAINavNode(Vector3 position)
	{
		var nodes = AINavNode.GetAINavNodes();
		AINavNode nearest = null;
		float nearestDistance = float.MaxValue;
		foreach (var node in nodes)
		{
			float distance = (position - node.transform.position).sqrMagnitude;
			if (distance < nearestDistance)
			{
				nearest = node;
				nearestDistance = distance;
			}
		}

		return nearest;
	}

	public void SortByDistance(List<AINavNode> nodes)
	{
		nodes.Sort(CompareDistance);
	}

	public int CompareDistance(AINavNode a, AINavNode b)
	{
		float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
		float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
		return squaredRangeA.CompareTo(squaredRangeB);
	}
	#endregion
}
