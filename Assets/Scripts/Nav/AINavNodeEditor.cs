using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[ExecuteInEditMode]
public class NavNodeEditor : MonoBehaviour
{
	[SerializeField] GameObject navNodePrefab;
	[SerializeField] LayerMask layerMask;
	[SerializeField] bool active = true;

	private Vector3 position = Vector3.zero;
	private bool spawnable = false;
	private AINavNode navNode = null;
	private AINavNode activeNavNode = null;
	
	private void OnEnable()
	{
		if (!Application.isEditor)
		{
			Destroy(this);
		}
		SceneView.duringSceneGui += OnScene;
	}

	private void OnDisable()
	{
		SceneView.duringSceneGui -= OnScene;
	}


	void OnScene(SceneView scene)
	{
		if (!active) return;

		Event e = Event.current;

		// get scene mouse position
		Vector3 mousePosition = e.mousePosition;
		mousePosition.y = scene.camera.pixelHeight - mousePosition.y * EditorGUIUtility.pixelsPerPoint;
		mousePosition.x *= EditorGUIUtility.pixelsPerPoint;

		Ray ray = scene.camera.ScreenPointToRay(mousePosition);

		// check mouse over spawn/nav layer
		if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
		{
			position = hitInfo.point;

			if (hitInfo.collider.gameObject.TryGetComponent<AINavNode>(out navNode))
			{
				if (activeNavNode == null)
				{
					Selection.activeGameObject = navNode.gameObject;
				}
				spawnable = false;
			}
			else spawnable = true;
		}
		else
		{
			navNode = null;
			spawnable = false;
		}

		// if spawnable and mouse pressed, create nav node
		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha1)
		{
			if (spawnable && navNode == null && activeNavNode == null) Instantiate(navNodePrefab, position, Quaternion.identity, transform);
			if (navNode != null && activeNavNode == null)
			{
				activeNavNode = navNode;
				navNode = null;
			}
		}
		// add connection to active nav node
		if (e.type == EventType.KeyUp && e.keyCode == KeyCode.Alpha1)
		{
			if (activeNavNode != null && navNode != null && activeNavNode != navNode)
			{
				if (!activeNavNode.neighbors.Contains(navNode))
				{
					activeNavNode.neighbors.Add(navNode);
				}

				if (!navNode.neighbors.Contains(activeNavNode))
				{
					navNode.neighbors.Add(activeNavNode);
				}
			}
			activeNavNode = null;
		}

		// delete nav node
		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha2)
		{
			if (navNode != null)
			{
				DestroyImmediate(navNode.gameObject);
			}
		}

		// use the event
		e.Use();
	}

	private void OnDrawGizmos()
	{
		if (!active) return;

		if (spawnable && navNode == null)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(position, 1);
		}
		if (navNode != null && navNode != activeNavNode)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(navNode.gameObject.transform.position, 1);
		}
		if (activeNavNode != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(activeNavNode.gameObject.transform.position, 1.5f);
			Gizmos.DrawLine(activeNavNode.gameObject.transform.position, position);
		}

		// draw connections
		var nodes = AINavNode.GetAINavNodes();
		foreach (AINavNode node in nodes)
		{
			foreach (AINavNode neighbors in node.neighbors)
			{
				Gizmos.color = Color.green;
				Gizmos.DrawLine(node.transform.position, neighbors.transform.position);
			}
		}

	}
}
