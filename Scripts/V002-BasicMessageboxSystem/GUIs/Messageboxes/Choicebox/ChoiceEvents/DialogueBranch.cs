using UnityEngine;
namespace GUIs.Messageboxes.ChoiceEvents
{
	/// <summary>
	/// This event displays another message.
	/// </summary>
	public class DialogueBranch : ChoiceEvent 
	{
		[SerializeField]
		protected MessageBoxSystem messageBoxSystem;

		/// <summary>
		/// The message to display.
		/// </summary>
		public Message message;
		/// <summary>
		/// The starting and ending lines. Determines what part of the message to display.
		/// </summary>
		public int startingLine, endingLine;

		public override void OnClick()
		{
			StartCoroutine(messageBoxSystem.CloseBoxes (true));
			StartCoroutine(messageBoxSystem.DisplayMessage (message, startingLine, endingLine));
		}
	}
}
