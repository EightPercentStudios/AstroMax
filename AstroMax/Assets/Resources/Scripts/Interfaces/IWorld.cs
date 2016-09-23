using UnityEngine;
using System.Collections;

// A world with vertical lanes
public interface IWorld
{
	// Get the total number of lanes
	int GetLaneCount();

	// Get bounding area for world in Unity units
	Vector2 GetUpperBound();
	Vector2 GetLowerBound();

	// Convert lane number to a lane end point
	Vector2 LaneToEndPoint(float lane);

	// Check if given position is off the world
	bool IsPointOutOfBounds(Vector2 position);
}
