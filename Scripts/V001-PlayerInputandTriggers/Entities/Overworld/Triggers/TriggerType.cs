using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Overworld.Triggers
{
	/// <summary>
	/// The names of the trigger types.
	/// May be used as an ID.
	/// </summary>
	public enum TriggerType {

		None = 0,
		PlayerFace = 1,
		Teleport = 2,
		DisplayInfo = 3
	}
}
