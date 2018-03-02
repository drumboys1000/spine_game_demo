using UnityEngine;
using Main;

namespace Entities.Overworld.Triggers
{
	/// <summary>
	/// This trigger represents the player's facing direction. 
	/// Useful for button press events on other triggers.
	/// </summary>
	public class PlayerFace : Trigger 
	{
		private Collider2D thisCollider;
		private OW_PlayerController player;
		private GameEventHandler events;

		[SerializeField]
		private bool facingObject;
		[SerializeField]
		private Transform targetObject;

		protected override void Start()
		{
			type = TriggerType.PlayerFace;

			thisCollider = this.transform.GetComponent<Collider2D> ();
			facingObject = false;

			player = this.transform.GetComponentInParent<OW_PlayerController> ();
			events = this.transform.GetComponentInParent<GameEventHandler> ();
		}
			
		protected void Update()
		{
			// if the player's movement is not blocked
			if (player.CurrentState.StateID != (int)OWStates.BlockMove)
			{
				// if the player is facing a trigger and presses action button
				if (facingObject && Input.GetButtonDown ("MainAction"))
				{
					// use other trigger's button press event
					targetObject.GetComponent<Trigger> ().OnButtonPress (thisCollider);
				}
				// if the player is not facing anything and presses action button
				if (!facingObject && Input.GetButtonDown ("MainAction"))
				{
					// display text box with default message
					DisplayDefault ();
				}
			}
		}

		protected override void OnTriggerEnter2D(Collider2D other)
		{
			facingObject = true;
			targetObject = other.transform;
			// Perhaps get other trigger's icon indicator and display?
		}

		protected override void OnTriggerStay2D(Collider2D other){ }

		protected override void OnTriggerExit2D(Collider2D other)
		{
			facingObject = false;
			// Perhaps remove other trigger's icon indicator?
		}

		private void DisplayDefault()
		{
			string defaultText = "Nothing here";
			events.CallDisplayTextboxEvent (defaultText);
		}
	}
}
