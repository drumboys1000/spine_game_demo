using System.Collections.Generic;
using UnityEngine;
using GUIs.Messageboxes.ChoiceEvents;

namespace GUIs.Messageboxes
{
	/// <summary>
	/// Choicebox arguments.
	/// </summary>
	public class ChoiceboxArgs : MessageBoxArgs 
	{
		[Space(20)]
		[Header("Choicebox")]
		/// <summary>
		/// The cursor image.
		/// </summary>
		public Sprite cursorImage;
		/// <summary>
		/// Left side padding for the choicebox
		/// </summary>
		public int choicePaddingLeft;
		/// <summary>
		/// Right side padding for the choicebox
		/// </summary>
		public int choicePaddingRight;
		/// <summary>
		/// Top side padding for the choicebox
		/// </summary>
		public int choicePaddingTop;
		/// <summary>
		/// Bottom side padding for the choicebox
		/// </summary>
		public int choicePaddingBottom;
		/// <summary>
		/// Spacing for the choicebox
		/// </summary>
		public float choiceSpacing;

		[Space(10)]
		[Header("Choices")]
		/// <summary>
		/// The number of choices.
		/// </summary>
		public int choiceCount;
		/// <summary>
		/// The desciptions for each choice.
		/// </summary>
		public List<string> desciptions;
		/// <summary>
		/// The icons for each choice.
		/// </summary>
		public List<Sprite> icons;
		/// <summary>
		/// The choice events for each choice.
		/// </summary>
		public List<ChoiceEvent> choiceEvents;
	}
}
