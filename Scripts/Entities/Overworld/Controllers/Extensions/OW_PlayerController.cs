namespace Entities.Overworld
{
	/// <summary>
	/// This is the player's overworld controller.
	/// Use with the enum OWStates.
	/// </summary>
	public class OW_PlayerController : OW_Controller 
	{
		/// <summary>
		/// Ensure the player can move with physics.
		/// </summary>
		protected override void Awake()
		{
			useRigidbody = true;
			base.Awake ();
		}

		/// <summary>
		/// Add states to the state machine.
		/// </summary>
		protected override void AddStates()
		{
			States.Add (new PlayerWaitState (this, this.gameObject.transform));
			States.Add (new PlayerMoveState (this, this.gameObject.transform, moveSpeed));
			States.Add (new BlockMoveState (this, this.gameObject.transform));
		}

		/// <summary>
		/// Set the current state to the starting state.
		/// </summary>
		protected override void Start()
		{
			base.Start();
			Change((int)startingState);
		}
	}
}