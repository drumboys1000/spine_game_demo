using System;
using UnityEngine;

namespace Main
{
	/// <summary>
	/// The GE Handler serves as a vessel for entities and actors in the game 
	/// to fire their respective events and let listeners of them act upon them.
	/// </summary>
	public class GameEventHandler: MonoBehaviour
	{
		/// <summary>
		/// Occurs when an entity or actor is moving.
		/// </summary>
		public event Action moveEvent;

		/// <summary>
		/// Occurs when a text box needs to be displayed.
		/// </summary>
		public event Action<string> displayStringTextboxEvent;

		/// <summary>
		/// Occurs when a text box needs to be displayed.
		/// </summary>
		public event Action<TextAsset> displayTextFileTextboxEvent;

		/// <summary>
		/// Calls the move event.
		/// </summary>
		public void CallMoveEvent()
		{
			if (moveEvent != null)
			{
				moveEvent();
			}
		}

		/// <summary>
		/// Calls the display textbox event.
		/// </summary>
		/// <param name="text">Text.</param>
		public void CallDisplayTextboxEvent(string text)
		{
			if (displayStringTextboxEvent != null)
			{
				displayStringTextboxEvent (text);
			}
		}

		/// <summary>
		/// Calls the display textbox event.
		/// </summary>
		/// <param name="textFile">Text file.</param>
		public void CallDisplayTextboxEvent(TextAsset textFile)
		{
			if (displayTextFileTextboxEvent != null)
			{
				displayTextFileTextboxEvent (textFile);
			}
		}
	}

}