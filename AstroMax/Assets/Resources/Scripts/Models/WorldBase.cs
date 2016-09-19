using UnityEngine;
using System.Collections;

public class WorldBase : IWorld
{
	// Private properties
	private int laneCount;
	private Vector2 upperBound;
	private Vector2 lowerBound;

	public WorldBase(int lanes, Vector2 upperBound, Vector2 lowerBound)
	{
		this.laneCount = lanes;
		this.upperBound = upperBound;
		this.lowerBound = lowerBound;
	}

	public int GetLaneCount()
	{
		return this.laneCount;
	}

	public Vector2 GetUpperBound()
	{
		return this.upperBound;
	}

	public Vector2 GetLowerBound()
	{
		return this.lowerBound;
	}
		
	public Vector2 LaneToEndPoint(float lane)
	{
		// Clamp lane from 0 -> lane_count+1
		lane = Mathf.Clamp(lane, 0, this.laneCount+1);

		float x = Mathf.Lerp(lane / (float)(this.laneCount + 1), lowerBound.x, upperBound.x);
		float y = lowerBound.y;

		return new Vector2(x, y);
	}
}
