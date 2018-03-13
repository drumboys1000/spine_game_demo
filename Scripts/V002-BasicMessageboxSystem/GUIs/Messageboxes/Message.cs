using UnityEngine;

namespace GUIs.Messageboxes
{
	public class Message : MonoBehaviour 
	{
		/// <summary>
		/// The number of lines a message has.
		/// </summary>
		public int numberOfLines;
		/// <summary>
		/// The file to read the message from.
		/// </summary>
		public TextAsset messageFile;
		/// <summary>
		/// A table of arguments for each messagebox. 
		/// Includes stuff like the position, size, and ids for preset values (i.e. Fixed dialogue, fitted windows, etc)
		/// </summary>
		public MessageBoxArgs[] messageBoxArguments;
	}
}
