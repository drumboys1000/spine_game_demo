using UnityEngine;

namespace Entities.Overworld.Triggers
{
	/// <summary>
	/// The trigger is useful for allowing certain actions to happen 
	/// upon detection of other colliders.
	/// </summary>
	public class Trigger : MonoBehaviour 
	{
		protected TriggerType type;

		/// <summary>
		/// Gets the trigger's type.
		/// </summary>
		/// <value>The type.</value>
		public TriggerType Type
		{
			get;
		}

		protected virtual void Start()
		{
			type = TriggerType.None;
		}

		/// <summary>
		/// Run actions once a collider enters the trigger.
		/// </summary>
		/// <param name="other">Other.</param>
		protected virtual void OnTriggerEnter2D(Collider2D other)
		{
			// For entering a trigger actions
		}

		/// <summary>
		/// Run actions while a collider is inside the trigger.
		/// </summary>
		/// <param name="other">Other.</param>
		protected virtual void OnTriggerStay2D(Collider2D other)
		{
			// While inside a trigger actions
		}

		/// <summary>
		/// Run actions once a collider exits the trigger.
		/// </summary>
		/// <param name="other">Other.</param>
		protected virtual void OnTriggerExit2D(Collider2D other)
		{
			// For on exiting a trigger actions
		}

		/// <summary>
		/// Run actions when a button is pressed inside the trigger. 
		/// The other collider must be a trigger and 
		/// use its Stay method for this to work.
		/// </summary>
		/// <param name="other">Other.</param>
		public virtual void OnButtonPress(Collider2D other)
		{
			// For button press inside trigger actions
		}
	}
}