using UnityEngine;
using System.Collections;

public class WorldBase : IWorld
{
	// Private properties
	private int laneCount;
	private Vector2 upperBound;
	private Vector2 lowerBound;
	private float lowerFloor;

	public WorldBase(int lanes, Vector2 upperBound, Vector2 lowerBound)
	{
		this.laneCount = lanes;
		this.upperBound = upperBound;
		this.lowerBound = lowerBound;

		this.lowerFloor = 0.85f;
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

		float t = lane / (float)(this.laneCount + 1);
		int middleLane = Mathf.FloorToInt(this.laneCount / 2 + 1);
		float diff = Mathf.Abs(middleLane - lane);

		float x = Mathf.Lerp(lowerBound.x, upperBound.x, t);
		float y = lowerBound.y + this.lowerFloor + diff * -0.2f;;

		return new Vector2(x, y);
	}

	public bool IsPointOutOfBounds(Vector2 point)
	{
		return 
			point.x < this.lowerBound.x ||
			point.x > this.upperBound.x ||
			point.y < this.lowerBound.y ||
			point.y > this.upperBound.y;
	}
}
