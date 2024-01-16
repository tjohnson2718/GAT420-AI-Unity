using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
	public static float Wrap(float value, float min, float max)
	{
		if (value > max) { return min; }
		else if (value < min) { return max; }

		return value;
	}

	public static Vector3 Wrap(Vector3 value, Vector3 min, Vector3 max)
	{
		value.x = Wrap(value.x, min.x, max.x);
		value.y = Wrap(value.y, min.y, max.y);
		value.z = Wrap(value.z, min.z, max.z);
		return value;
	}
	public static Vector3[] GetDirectionsInCircle(int num, float angle)
	{
		List<Vector3> result = new List<Vector3>();

		// if odd number, set first direction as forward (0, 0, 1)??
		if (num % 2 == 1) result.Add(Vector3.forward);

		// compute the angle between rays??
		float angleOffset = (angle * 2) / num;

		// add the +/- directions around the circle??
		for (int i = 1; i <= num / 2; i++)
		{
			float modifier = (i == 1 && num % 2 == 0) ? 0.65f : 1;
			result.Add(Quaternion.AngleAxis(+angleOffset * i * modifier, Vector3.up) * Vector3.forward);
			result.Add(Quaternion.AngleAxis(-angleOffset * i * modifier, Vector3.up) * Vector3.forward);
		}

		return result.ToArray();
	}
}
