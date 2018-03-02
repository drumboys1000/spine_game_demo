using UnityEngine;
using Main;
using Main.Controllers;

namespace Entities.Overworld
{
	/// <summary>
	/// This is the state in which the player is moving.
	/// </summary>
	public class PlayerMoveState : State
	{
		private Transform _character;
		private Vector2 _moveInput;
		private Vector2 _lastMove;

		private Rigidbody2D rb;
		private Animator anim;
		private GameEventHandler events;

		private float _moveSpeed;

		/// <summary>
		/// Initializes a new instance of the <see cref="Entities.Overworld.PlayerMoveState"/> class.
		/// </summary>
		/// <param name="controller">Controller.</param>
		/// <param name="character">Character.</param>
		/// <param name="moveSpeed">Move speed.</param>
		public PlayerMoveState(StateMachine controller, Transform character, float moveSpeed) : base(controller)
		{
			StateID = (int)OWStates.PlayerMove;
			_character = character;
			_moveSpeed = moveSpeed;

			rb = _character.gameObject.GetComponent<Rigidbody2D>();
			anim = _character.gameObject.GetComponent<Animator>();
			events = _character.gameObject.GetComponent<GameEventHandler>();
		}

		/// <summary>
		/// This occurs when the player begins moving.
		/// </summary>
		/// <param name="data">Data.</param>
		public override void Enter (params object[] data)
		{
			OW_Controller pc = (OW_Controller)Controller;
			_moveSpeed = pc.moveSpeed;
			anim.SetBool("Moving", true);
		}

		/// <summary>
		/// This occurs when the player stops moving.
		/// </summary>
		public override void Exit ()
		{
			anim.SetFloat("LastMoveX", _lastMove.x);
			anim.SetFloat("LastMoveY", _lastMove.y);
			rb.velocity = Vector2.zero;
		}

		/// <summary>
		/// Run this state's behavior.
		/// </summary>
		public override void Update ()
		{
			_moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

			anim.SetFloat("MoveX", _moveInput.x);
			anim.SetFloat("MoveY", _moveInput.y);

			if (_moveInput != Vector2.zero)
			{
				events.CallMoveEvent();
				rb.velocity = new Vector2(_moveInput.x * _moveSpeed, _moveInput.y * _moveSpeed);
				_lastMove = _moveInput;
			}
			else
			{
				Controller.Change((int)OWStates.PlayerWait);
			}
		}
	}

}