using UnityEngine;
using System.Collections;

public class PlayerBase : KillableEntityBase, IPlayer
{
	public PlayerBase(Vector2 position, int hitPoints) : base(position, hitPoints)
	{
		
	}
}
