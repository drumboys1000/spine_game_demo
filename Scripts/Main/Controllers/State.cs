namespace Main.Controllers
{
	/// <summary>
	/// The state determines the behavior the object implementing the state machine will do.
	/// </summary>
	public abstract class State 
	{
		private int _stateID;
		private StateMachine _controller;

		/// <summary>
		/// Gets or sets the state's ID. This is also known as the state's name.
		/// </summary>
		/// <value>The state ID.</value>
		public int StateID
		{
			get { return _stateID; }
			set { _stateID = value; }
		}

		/// <summary>
		/// Gets or sets the controller of this state.
		/// </summary>
		/// <value>The controller.</value>
		public StateMachine Controller
		{
			get
			{
				return _controller;
			}

			set
			{
				_controller = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Main.Controllers.State"/> class.
		/// </summary>
		/// <param name="controller">Controller.</param>
		public State(StateMachine controller)
		{
			Controller = controller;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Main.Controllers.State"/> class.
		/// </summary>
		/// <param name="stateID">State ID.</param>
		/// <param name="controller">Controller.</param>
		public State(int stateID, StateMachine controller)
		{
			StateID = stateID;
			Controller = controller;
		}

		/// <summary>
		/// This occurs when the state machine is entering this state.
		/// Its arguments may be used for whatever fit.
		/// </summary>
		/// <param name="data">Data.</param>
		public abstract void Enter (params object[] data);

		/// <summary>
		/// This occurs when the state machine is exiting this state.
		/// </summary>
		public abstract void Exit ();

		/// <summary>
		/// Run this state's behavior.
		/// </summary>
		public abstract void Update ();
	}

}