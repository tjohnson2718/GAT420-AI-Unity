using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AINavNode : MonoBehaviour
{
	[SerializeField] public List<AINavNode> neighbors = new List<AINavNode>();
	public float Cost { get; set; } = float.MaxValue;
	public AINavNode Parent { get; set; } = null;

	public AINavNode GetRandomNeighbor()
	{
		return (neighbors.Count > 0) ? neighbors[Random.Range(0, neighbors.Count)] : null;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent<AINavPath>(out AINavPath navPath))
		{
			if (navPath.targetNode == this)
			{
				navPath.targetNode = GetRandomNeighbor();
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.TryGetComponent<AINavPath>(out AINavPath navPath))
		{
			if (navPath.targetNode == this)
			{
				navPath.targetNode = GetRandomNeighbor();
			}
		}
	}


	#region HELPER_FUNCTIONS

	public static AINavNode[] GetAINavNodes()
	{
		return FindObjectsOfType<AINavNode>();
	}

	public static AINavNode[] GetAINavNodesWithTag(string tag)
	{
		var allNodes = GetAINavNodes();

		// add nodes with tag to nodes
		List<AINavNode> nodes = new List<AINavNode>();
		foreach (var node in allNodes)
		{
			if (node.CompareTag(tag))
			{
				nodes.Add(node);
			}
		}

		return nodes.ToArray();
	}

	public static AINavNode GetRandomAINavNode()
	{
		var nodes = GetAINavNodes();
		return (nodes == null) ? null : nodes[Random.Range(0, nodes.Length)];
	}

	#endregion
}
