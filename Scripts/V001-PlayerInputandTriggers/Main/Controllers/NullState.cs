namespace Main.Controllers
{
	/// <summary>
	/// The null state ensures that the state machine will do nothing once entered.
	/// </summary>
	public class NullState : State 
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Main.Controllers.NullState"/> class.
		/// </summary>
		/// <param name="controller">Controller.</param>
	    public NullState(StateMachine controller) : base(controller)
	    {
	    }

	    public override void Enter (params object[] data)
		{
			//Debug.Log ("NullState : Enter()");
		}

		public override void Exit ()
		{
			//Debug.Log ("NullState : Exit()");
		}
			
		public override void Update ()
		{
			//Debug.Log ("NullState : Update");
		}
	}
}