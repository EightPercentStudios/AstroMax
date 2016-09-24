using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBase : MonoBehaviour, ILevel // Add spawner (monobehaviour)
{
	// Internal Properties
	private IWorld world;
	private IPlayer player;
	private ISpawner spawner;
	private List<BulletBase> bullets;
	private List<IKillableEntity> enemies;

	// Object pools
	private IObjectPool bulletPool;
	public GameObject bulletPrefab;

	private bool paused;
	private int score;
	private bool setup = false;

	// Public delegates
	public delegate void FireBulletMethod(Vector2 position);

	public void Setup(IWorld world, ISpawner spawner)
	{
		this.world = world;
		this.spawner = spawner;

		this.paused = false;
		this.score = 0;
		this.bullets = new List<BulletBase>();
		this.enemies = new List<IKillableEntity>();

		this.bulletPool = new ObjectPool("Bullet Pool", bulletPrefab, 10);

		this.setup = true;
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
		InvokeRepeating("SpawnEnemy", 2f, 0.65f);
	}

	private void SpawnEnemy()
	{
		// Random lane
		int lane = Random.Range(0, this.world.GetLaneCount()) + 1;
		Vector2 randomPosition = new Vector2(this.world.LaneToEndPoint(lane).x, this.world.GetUpperBound().y);
		//Debug.Log(lane);

		GameObject enemyGraphics = this.spawner.SpawnPrefab("Enemy");
		IKillableEntity enemy = new EnemyBase(randomPosition, 2, enemyGraphics);
		this.enemies.Add(enemy);
	}

	private void FireBullet(Vector2 position)
	{
		//GameObject bullet = this.spawner.SpawnPrefab("Bullet");
		GameObject bullet = this.bulletPool.GetObjectInstance();
		BulletBase b = new BulletBase(position, 20.0f);
		b.SetGameObject(bullet);
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

	public void Update()
	{
		if (!setup)
			return;
		
		float deltaTime = Time.deltaTime;

		// Update all the game objects
		this.player.Update(deltaTime, this.world);

		foreach (IKillableEntity enemy in this.enemies)
		{
			if (!enemy.IsAlive())
				continue;
			
			enemy.Update(deltaTime, this.world);
		}


		foreach (BulletBase bullet in this.bullets)
		{
			if (!bullet.IsAlive())
				continue;
			
			bullet.Update(deltaTime, this.world);

			// Check for collisions against enemies
			foreach (IKillableEntity enemy in this.enemies)
			{
				float dist = Vector2.SqrMagnitude(enemy.GetPosition() - bullet.GetPosition());
				if (dist < 0.2f)
				{
					enemy.TakeDamage(1);
					bullet.TakeDamage(bullet.GetMaxHitPoints() + 1); // Kill bullet
				}
			}
		}
			

		// Destroy all dead entities
		foreach (BulletBase bullet in this.bullets)
			if (!bullet.IsAlive())
			{
				this.bulletPool.ReturnObjectInstance(bullet.ReturnGameObject());
				bullet.Destroy();
			}
		foreach (IKillableEntity enemy in this.enemies)
			if (!enemy.IsAlive())
				enemy.Destroy();
		
		this.bullets.RemoveAll(b => !b.IsAlive()); // Remove all dead bullets
		this.enemies.RemoveAll(e => !e.IsAlive()); // Remove all dead enemies
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
