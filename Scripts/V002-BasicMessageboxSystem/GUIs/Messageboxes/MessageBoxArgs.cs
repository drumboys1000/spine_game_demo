using UnityEngine;
using UnityEngine.UI;

namespace GUIs.Messageboxes
{
	/// <summary>
	/// Message box arguments.
	/// </summary>
	public class MessageBoxArgs : MonoBehaviour
	{
		/// <summary>
		/// The type of message box. 
		/// Can be set to an preset type so that certain values like position and size don't need to be set.
		/// </summary>
		public MessageBoxTypes type;

		[Header("General")]
		/// <summary>
		/// The image a message box may use.
		/// </summary>
		public Sprite image;
		/// <summary>
		/// The box's initial position.
		/// </summary>
		public float panelX, panelY;
		/// <summary>
		/// The box's initial size.
		/// </summary>
		public float panelWidth, panelHeight;
		/// <summary>
		/// The box's new position (if applicable)
		/// </summary>
		public float newPanelX, newPanelY;
		/// <summary>
		/// The box's new size (if applicable)
		/// </summary>
		public float newPanelWidth, newPanelHeight;
		/// <summary>
		/// Indicates the time it takes for a panel to change appearance. Larger values mean quicker scales.
		/// </summary>
		public float tweenScale;
		/// <summary>
		/// A box's padding.
		/// </summary>
		public int padding;
		/// <summary>
		/// The size of the font.
		/// </summary>
		public int fontSize;

		/// <summary>
		/// Gets the box's arguments.
		/// </summary>
		/// <returns>The arguments.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T GetArgs<T>() where T: MessageBoxArgs
		{
			return this as T;
		}
	}
}
