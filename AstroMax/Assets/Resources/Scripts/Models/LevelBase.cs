using UnityEngine;
using System.Collections;

public class LevelBase : ILevel // Add spawner (monobehaviour)
{
	// Internal Properties
	private IWorld world;
	private IPlayer player;
	private ISpawner spawner;

	private bool paused;
	private int score;

	public LevelBase(IWorld world, ISpawner spawner)
	{
		this.world = world;
		this.spawner = spawner;

		this.paused = false;
		this.score = 0;
	}

	public void Load()
	{
		// Load player prefab
		GameObject playerObject = this.spawner.SpawnPrefab("Player");
		this.player = new PlayerBase(world.LaneToEndPoint(3), 3, this.world, playerObject);
	}

	public void Start()
	{
		this.paused = false;
	}

	public void Pause()
	{
		this.paused = true;
	}

	public void Resume()
	{
		this.paused = false;
	}

	public void Cleanup()
	{
		// Let the GC get everything
	}

	public void Update(float deltaTime)
	{
		// Update all the game objects
		this.player.Update(deltaTime);
	}

	public int GetScore()
	{
		return this.score;
	}

	public bool IsPaused()
	{
		return this.paused;
	}
}
