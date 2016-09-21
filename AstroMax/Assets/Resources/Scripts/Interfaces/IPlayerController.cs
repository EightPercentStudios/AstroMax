using UnityEngine;
using System.Collections;

public enum PlayerMoveDirection
{
	LEFT,
	RIGHT,
	NONE
}

// Control the player's movements
public interface IPlayerController
{
	PlayerMoveDirection PollInput();
}
