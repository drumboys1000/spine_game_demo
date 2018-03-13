using UnityEngine;

namespace GUIs.Messageboxes
{
	/// <summary>
	/// Textbox arguments.
	/// </summary>
	public class TextboxArgs : MessageBoxArgs 
	{
		[Space(20)]
		[Header("Textbox")]
		/// <summary>
		/// The name of the entity speaking.
		/// </summary>
		public string entityName;
		/// <summary>
		/// Determines whether to display the name.
		/// </summary>
		public bool raiseName;
		/// <summary>
		/// Determines whether to display text like a character is speaking
		/// </summary>
		public bool textTalk;
		/// <summary>
		/// The speed at which to delay characters of a text.
		/// </summary>
		public float typeDelay;
		/// <summary>
		/// The text's alignment.
		/// </summary>
		public TextAnchor alignment;
		/// <summary>
		/// The choice box arguments (if applicable)
		/// </summary>
		public ChoiceboxArgs choiceBoxArgs;
	}
}
