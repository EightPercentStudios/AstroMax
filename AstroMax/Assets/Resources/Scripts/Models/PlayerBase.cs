﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBase : KillableEntityBase, IPlayer
{
	// Internal Properties
	private IWorld world;
	private GameObject playerGameObject;
	private int currentLane;
	private int targetLane;
	private float moveTime;
	private List<int> laneQueue;
	private IPlayerController controller;
	private bool moving;

	public PlayerBase(Vector2 position, int hitPoints, IWorld world, GameObject playerObject) : base(position, hitPoints)
	{
		this.world = world;
		this.playerGameObject = playerObject;

		this.currentLane = 3;
		this.targetLane = 3;
		this.moveTime = 0;
		this.laneQueue = new List<int>();
		this.moving = false;
		this.controller = new PlayerInputController();

		UpdatePosition();
	}

	public override void Update(float deltaTime)
	{
		HandleInput();
		Move(deltaTime);
		UpdatePosition();
	}

	private void HandleInput()
	{
		PlayerMoveDirection dir = this.controller.PollInput();
		if (dir == PlayerMoveDirection.NONE)
			return;
		
		int dirInt = dir == PlayerMoveDirection.LEFT ? -1 : 1;
		if (this.laneQueue.Count > 0)
		{
			int prevLane = this.laneQueue[laneQueue.Count-1];
			int newLane = Mathf.Clamp(prevLane + dirInt, 1, world.GetLaneCount());
			if (newLane != prevLane)
				this.laneQueue.Add(newLane);
		}
		else
		{
			int newLane = Mathf.Clamp(this.currentLane + dirInt, 1, world.GetLaneCount());
			this.laneQueue.Add(newLane);
		}
	}

	private void Move(float deltaTime)
	{
		if (!moving)
		{
			// See if the player should move
			if (this.laneQueue.Count > 0)
			{
				this.targetLane = this.laneQueue[0];
				this.laneQueue.RemoveAt(0);
				this.moveTime = 0;
				this.moving = true;
			}
		}
		else
		{
			// Continue movement
			moveTime += deltaTime;
			float duration = 0.03f;
			float t = moveTime / duration;

			this.position = Vector2.Lerp(world.LaneToEndPoint(currentLane), world.LaneToEndPoint(targetLane), t);

			if (moveTime > duration)
			{
				this.moving = false;
				this.currentLane = this.targetLane;
			}
		}
	}

	private void UpdatePosition()
	{
		this.playerGameObject.transform.position = this.position;
	}
}