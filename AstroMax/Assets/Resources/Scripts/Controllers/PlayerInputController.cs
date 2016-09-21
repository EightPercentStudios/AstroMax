using UnityEngine;
using System.Collections;

public class PlayerInputController : IPlayerController
{
	// Check if the user is pressing anything (eventually switch to delegates)
	public PlayerMoveDirection PollInput()
	{
		bool left = false;
		bool right = false;

		if (Input.GetKeyDown("left"))
			left = true;
		if (Input.GetKeyDown("right"))
			right = true;

		if (left && right)
			return PlayerMoveDirection.NONE;
		else if (left)
			return PlayerMoveDirection.LEFT;
		else if (right)
			return PlayerMoveDirection.RIGHT;
		else
			return PlayerMoveDirection.NONE;
	}
}
