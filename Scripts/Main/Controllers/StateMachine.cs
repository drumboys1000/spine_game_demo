using System.Collections.Generic;
using UnityEngine;

namespace Main.Controllers
{
	/// <summary>
	/// The state machine is useful for organizing behavior in the entity or actor that implements it. 
	/// Before using, an enum containing the state names would need to be created along with the states themselves.
	/// </summary>
	public class StateMachine : MonoBehaviour 
	{
		private List<State> states;

		private State currentState;

		/// <summary>
		/// Gets the state machine's states.
		/// </summary>
		/// <value>The states.</value>
		public List<State> States
		{
			get 
			{ 
				if (states == null)
				{
					states = new List<State> ();
				}
				return states; 
			}
		}

		/// <summary>
		/// Gets the current state the state machine is in.
		/// </summary>
		/// <value>The current state.</value>
		public State CurrentState
		{
			get { return currentState; }
			set { currentState = value; }
		}

		/// <summary>
		/// Ensures the state machine is in a null state.
		/// </summary>
		protected virtual void Awake()
		{
			States.Add (new NullState (this));
			AddStates ();
		}

		/// <summary>
		/// Set the current state to the null state.
		/// </summary>
		protected virtual void Start()
		{
			CurrentState = States[0];
		}

		/// <summary>
		/// Add states to the state machine.
		/// </summary>
		protected virtual void AddStates ()
		{
			Debug.Log ("This is the state machine base class; Add some states in its subclasses");
		}

		/// <summary>
		/// Change the state machine's current state to the state specified. 
		/// Additional input is accepted.
		/// </summary>
		/// <param name="stateID">State ID.</param>
		/// <param name="input">Input.</param>
		public void Change(int stateID, params object[] input)
		{
			CurrentState.Exit ();
			CurrentState = States [stateID];
			CurrentState.Enter (input);
		}
	}
}