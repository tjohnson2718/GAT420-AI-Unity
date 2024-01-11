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
}
