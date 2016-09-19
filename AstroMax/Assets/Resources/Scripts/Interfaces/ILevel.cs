using UnityEngine;
using System.Collections;

public interface ILevel
{
	// Events
	void Load();
	void Start();
	void Pause();
	void Resume();
	void Cleanup();

	// Game Tick
	void Update();

	// Get Properties
	int GetScore();
	bool IsPaused();
}
