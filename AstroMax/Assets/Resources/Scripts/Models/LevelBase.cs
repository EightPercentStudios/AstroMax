using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBase : MonoBehaviour, ILevel // Add spawner (monobehaviour)
{
	// Internal Properties
	private IWorld world;
	private IPlayer player;
	private List<BulletBase> bullets;
	private List<EnemyBase> enemies;
	private List<EnergyBit> energyBits;

	// Object pools
	private IObjectPool bulletPool;
	public GameObject bulletPrefab;

	private IObjectPool enemyPool;
	public GameObject enemyPrefab;

	private IObjectPool energyBitPool;
	public GameObject energyBitPrefab;

	// Misc
	private bool paused;
	private int score;
	private bool setup = false;

	// Public delegates
	public delegate void FireBulletMethod(Vector2 position);

	public void Setup(IWorld world)
	{
		this.world = world;

		this.paused = false;
		this.score = 0;
		this.bullets = new List<BulletBase>();
		this.enemies = new List<EnemyBase>();
		this.energyBits = new List<EnergyBit>();

		this.bulletPool = new ObjectPool("Bullet Pool", bulletPrefab, 10);
		this.enemyPool = new ObjectPool("Test Enemy Pool", enemyPrefab, 5);
		this.energyBitPool = new ObjectPool("Energy Bit Pool", energyBitPrefab, 75);

		this.setup = true;
	}

	public void Load()
	{
		// Load player prefab
		GameObject playerObject = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
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

		//GameObject enemyGraphics = this.spawner.SpawnPrefab("Enemy");
		GameObject enemyGraphics = this.enemyPool.GetObjectInstance();
		EnemyBase enemy = new EnemyBase(randomPosition, 2);
		enemy.SetGameObject(enemyGraphics);
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

	private void SpawnEnergyBits(Vector2 position)
	{
		int numOfBits = 10;
		for (int i = 0; i < numOfBits; i++)
		{
			float velX = Random.Range(-8f, 8f);
			float velY = Random.Range(-1f, 35f);
			Vector2 randVeloc = new Vector2(velX, velY);
			float tintFactor = Random.Range(0.5f, 1f);
			float scale = Random.Range(0.6f, 1f);

			GameObject energyBitGraphics = this.energyBitPool.GetObjectInstance();
			energyBitGraphics.GetComponentInChildren<SpriteRenderer>().color = Color.white * tintFactor;
			energyBitGraphics.transform.localScale = Vector3.one * scale;
			EnergyBit energyBit = new EnergyBit(position, randVeloc);
			energyBit.SetGameObject(energyBitGraphics);
			this.energyBits.Add(energyBit);
		}
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

		foreach (EnemyBase enemy in this.enemies)
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

		foreach (EnergyBit energyBit in this.energyBits)
		{
			if (!energyBit.IsAlive())
				continue;

			energyBit.Update(deltaTime, this.world, this.player.GetPosition());

			// Check for collisions with the player
			Vector2 pos = energyBit.GetPosition();
			if ((pos.y - this.world.GetLowerBound().y) < 0.5f &&
				Mathf.Abs(pos.x - this.player.GetPosition().x) < 0.5f)
			{
				energyBit.TakeDamage(1);
				this.score++;
			}
		}
			

		// Destroy all dead entities
		foreach (BulletBase bullet in this.bullets)
		{
			if (!bullet.IsAlive())
			{
				this.bulletPool.ReturnObjectInstance(bullet.ReturnGameObject());
				bullet.Destroy();
			}
		}
		foreach (EnemyBase enemy in this.enemies)
		{
			if (!enemy.IsAlive())
			{
				this.enemyPool.ReturnObjectInstance(enemy.ReturnGameObject());
				enemy.Destroy();

				// Spawn energy bits
				if (enemy.GetPosition().y > this.world.GetLowerBound().y + 1f)
					SpawnEnergyBits(enemy.GetPosition());
			}
		}
		foreach (EnergyBit energyBit in this.energyBits)
		{
			if (!energyBit.IsAlive())
			{
				this.energyBitPool.ReturnObjectInstance(energyBit.ReturnGameObject());
				energyBit.Destroy();
			}
		}
		
		this.bullets.RemoveAll(b => !b.IsAlive()); 		// Remove all dead bullets
		this.enemies.RemoveAll(e => !e.IsAlive()); 		// Remove all dead enemies
		this.energyBits.RemoveAll(e => !e.IsAlive()); 	// Remove all dead energy bits
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
