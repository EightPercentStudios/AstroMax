using UnityEngine;
using System.Collections;

public class LevelBase : ILevel
{
	// Internal Properties
	private IWorld world;
	private IPlayer player;

	private bool paused;
	private int score;

	public LevelBase(IWorld world)
	{
		this.world = world;

		this.paused = false;
		this.score = 0;
	}

	public void Load()
	{
		// Do nothing
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

	public void Update()
	{
		// Update all the game objects
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
