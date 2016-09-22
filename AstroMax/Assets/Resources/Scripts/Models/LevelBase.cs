using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBase : ILevel // Add spawner (monobehaviour)
{
	// Internal Properties
	private IWorld world;
	private IPlayer player;
	private ISpawner spawner;
	private List<BulletBase> bullets;
	private List<IKillableEntity> enemies;

	private bool paused;
	private int score;

	// Public delegates
	public delegate void FireBulletMethod(Vector2 position);

	public LevelBase(IWorld world, ISpawner spawner)
	{
		this.world = world;
		this.spawner = spawner;

		this.paused = false;
		this.score = 0;
		this.bullets = new List<BulletBase>();
		this.enemies = new List<IKillableEntity>();
	}

	public void Load()
	{
		// Load player prefab
		GameObject playerObject = this.spawner.SpawnPrefab("Player");
		this.player = new PlayerBase(world.LaneToEndPoint(3), 3, playerObject, this.FireBullet);
	}

	public void Start()
	{
		this.paused = false;

		// Start spawning enemies
		//System.Random r = new System.Random();
		//System.Threading.Timer timer = new System.Threading.Timer(callback => { SpawnEnemy((int)(r.Next(1, this.world.GetLaneCount()))); },
		//	null, 1000, System.Threading.Timeout.Infinite);
	}

	private void SpawnEnemy(int lane)
	{
		// Random lane
		Vector2 randomPosition = new Vector2(this.world.LaneToEndPoint(lane).x, this.world.GetUpperBound().y);


		var enemy = new EnemyBase(randomPosition, 2, null);
		this.enemies.Add((IKillableEntity)enemy);
	}

	private void FireBullet(Vector2 position)
	{
		GameObject bullet = this.spawner.SpawnPrefab("Bullet");
		BulletBase b = new BulletBase(position, 20.0f, bullet);
		this.bullets.Add(b);
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
		this.player.Update(deltaTime, this.world);

		foreach (IKillableEntity enemy in this.enemies)
		{
			enemy.Update(deltaTime, this.world);
			if (!enemy.IsAlive())
				enemy.Destroy();
		}

		foreach (BulletBase bullet in this.bullets)
		{
			bullet.Update(deltaTime, this.world);
			if (!bullet.IsAlive())
				bullet.Destroy();
		}

		this.bullets.RemoveAll(b => !b.IsAlive()); // Remove all dead bullets
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
