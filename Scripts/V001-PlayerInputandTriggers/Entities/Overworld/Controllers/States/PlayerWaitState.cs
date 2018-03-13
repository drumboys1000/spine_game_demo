using UnityEngine;
using Main.Controllers;

namespace Entities.Overworld
{
	/// <summary>
	/// This state runs when the player stops moving.
	/// </summary>
	public class PlayerWaitState : State 
	{
		private Transform _character;
		private Animator anim;

		/// <summary>
		/// Initializes a new instance of the <see cref="Entities.Overworld.PlayerWaitState"/> class.
		/// </summary>
		/// <param name="controller">Controller.</param>
		/// <param name="character">Character.</param>
		public PlayerWaitState(StateMachine controller, Transform character) : base(controller)
		{
			StateID = (int)OWStates.PlayerWait;
			_character = character;
			anim = _character.gameObject.GetComponent<Animator>();
		}

		/// <summary>
		/// This occurs when the state machine is entering this state.
		/// </summary>
		/// <param name="data">Data.</param>
		public override void Enter (params object[] data)
		{
			anim.SetBool("Moving", false);
		}

		public override void Exit () { }

		/// <summary>
		/// Run this state's behavior.
		/// </summary>
		public override void Update ()
		{
			// Player begins movement
			if ((Input.GetButton("Vertical") || Input.GetButton("Horizontal")))
			{
				Controller.Change((int)OWStates.PlayerMove);
			}
		}
	}

}