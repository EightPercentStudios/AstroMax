﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	// Inspector References
	public LevelBase levelModel;
	public Text ScoreText;
	public CameraShake cameraShake;
	private bool debugDrawOn = false;

	// Model
	//private ILevel levelModel;
	private IWorld worldModel;

	// Delegates
	public delegate void ShakeCameraMethod(float amount);

	void Awake()
	{
		// Create base world
		Vector2 lowerPoint = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		Vector2 upperPoint = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		this.worldModel = new WorldBase(5, upperPoint, lowerPoint);

		// Start level model
		this.levelModel.Setup(this.worldModel, this.ShakeCamera);
		this.levelModel.Load();

		//
		debugDrawOn = true;
	}

	void Start()
	{
		//this.levelModel.Start();
	}

	void Update()
	{
		//this.levelModel.Update(Time.deltaTime);
		this.ScoreText.text = this.levelModel.GetScore().ToString();
	}

	public void ShakeCamera(float amount)
	{
		this.cameraShake.ShakeImpulse(amount);
	}

	// Debug draw
	void OnDrawGizmos()
	{
		if (!debugDrawOn)
			return;

		// Draw world boundaries
		Gizmos.color = Color.red;
		Gizmos.DrawLine(
			new Vector3(worldModel.GetUpperBound().x, worldModel.GetUpperBound().y, 0),
			new Vector3(worldModel.GetLowerBound().x, worldModel.GetUpperBound().y, 0));
		Gizmos.DrawLine(
			new Vector3(worldModel.GetLowerBound().x, worldModel.GetUpperBound().y, 0),
			new Vector3(worldModel.GetLowerBound().x, worldModel.GetLowerBound().y, 0));
		Gizmos.DrawLine(
			new Vector3(worldModel.GetLowerBound().x, worldModel.GetLowerBound().y, 0),
			new Vector3(worldModel.GetUpperBound().x, worldModel.GetLowerBound().y, 0));
		Gizmos.DrawLine(
			new Vector3(worldModel.GetUpperBound().x, worldModel.GetLowerBound().y, 0),
			new Vector3(worldModel.GetUpperBound().x, worldModel.GetUpperBound().y, 0));

		// Draw world lanes
		Gizmos.color = Color.gray;
		for (int i = 1; i < worldModel.GetLaneCount()+1; i++)
		{
			Vector2 laneEndPoint = worldModel.LaneToEndPoint(i);
			Gizmos.DrawLine(
				new Vector3(laneEndPoint.x, laneEndPoint.y, 0),
				new Vector3(laneEndPoint.x, worldModel.GetUpperBound().y, 0));
		}
	}
}
