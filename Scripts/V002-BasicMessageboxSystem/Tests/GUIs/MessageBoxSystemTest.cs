using System.Collections;
using UnityEngine;
using GUIs.Messageboxes;

namespace Tests
{
	// Test out the messagebox system
	public class MessageBoxSystemTest : MonoBehaviour
	{
		public MessageBoxSystem messageBoxSystem;

		public Message message;

		void Update ()
		{
			if (Input.GetButtonDown("MainAction"))
			{
				if (!messageBoxSystem.IsActive)
				{
					StartCoroutine(messageBoxSystem.DisplayMessage (message));
				}
			}
		}
	}
}
