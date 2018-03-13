using UnityEngine;
using Main.Controllers;

namespace Entities.Overworld
{
	/// <summary>
	/// This is the state in which an entity's movement is blocked.
	/// Useful in cutscenes, events (i.e. displaying textboxes) and saving locational data.
	/// </summary>
	public class BlockMoveState : State 
	{
		private Transform _character;
		private Animator anim;

		/// <summary>
		/// Initializes a new instance of the <see cref="Entities.Overworld.BlockMoveState"/> class.
		/// </summary>
		/// <param name="controller">Controller.</param>
		/// <param name="character">Character.</param>
		public BlockMoveState(StateMachine controller, Transform character) : base(controller)
		{
			StateID = (int)OWStates.BlockMove;
			_character = character;
			anim = _character.gameObject.GetComponent<Animator>();
		}

		public override void Enter (params object[] data)
		{
			anim.SetBool("Moving", false);
		}

		public override void Exit () { }

		public override void Update (){ }
	}
}
