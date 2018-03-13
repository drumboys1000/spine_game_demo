using UnityEngine;

namespace GUIs.Messageboxes.ChoiceEvents
{
	/// <summary>
	/// Choice event. Used for indicating what happens when a user selects a particular choice.
	/// </summary>
	public abstract class ChoiceEvent : MonoBehaviour 
	{
		/// <summary>
		/// Raises the click event.
		/// </summary>
		public abstract void OnClick ();
	}
}
