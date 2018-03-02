using Main.Controllers;

namespace Entities.Overworld
{
	/// <summary>
	/// This is an entity's overworld controller.
	/// Use with the enum OWStates.
	/// </summary>
	public class OW_Controller : StateMachine 
	{
		/// <summary>
		/// This is the state that the entity begins in.
		/// </summary>
		public OWStates startingState = OWStates.BlockMove;

		/// <summary>
		/// Determines how the entity moves 
		/// a.k.a. Physics vs no physics
		/// </summary>
		public bool useRigidbody;

		/// <summary>
		/// The speed that an entity will move at.
		/// </summary>
	    public float moveSpeed;
			
		protected override void AddStates()
		{
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

		/// <summary>
		/// Update using non-physics.
		/// </summary>
		public void Update ()
		{
			if (!useRigidbody)
			{
				CurrentState.Update();
			}
		}

		/// <summary>
		/// Update using physics.
		/// </summary>
		public void FixedUpdate()
		{
			if (useRigidbody)
			{
				CurrentState.Update();
			}
		}
	}
}