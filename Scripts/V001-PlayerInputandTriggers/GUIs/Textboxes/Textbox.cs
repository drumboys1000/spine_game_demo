using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Main;
using Entities.Overworld;

namespace GUIs
{
	/// <summary>
	/// The textbox displays messages on the screen
	/// Messages are received using events.
	/// </summary>
	public class Textbox : MonoBehaviour 
	{
		/// <summary>
		/// The text's area of the box.
		/// </summary>
		public Text textArea;
		/// <summary>
		/// The text split into lines
		/// </summary>
		public string[] textLines;
		/// <summary>
		/// The current line in the text.
		/// </summary>
		public int currentLine;
		/// <summary>
		/// The ending line.
		/// </summary>
		public int endLine;
		/// <summary>
		/// The speed at which to display text.
		/// </summary>
		public float typeSpeed;

		/// <summary>
		/// Determines whether or not the textbox is active.
		/// </summary>
		public bool activeDisplay;
		/// <summary>
		/// Determines whether or not the textbox's text is still scrolling
		/// </summary>
		private bool activeTyping;
		/// <summary>
		/// Determines whether or not to cancel typing
		/// </summary>
		private bool cancelTyping;

		private List<Transform> entities;

		void Start () 
		{
			GameObject go = GameObject.FindWithTag ("Game");

			if (go.name == "Game")
			{
				Game g = go.GetComponent<Game> ();
				entities = g.Entities;
				AddDisplayEvents ();
			}
			else
			{
				Assert.IsNull (go, "Gameobject not found");
			}
			this.gameObject.SetActive (false);
		}

		private void AddDisplayEvents()
		{
			foreach (Transform ent in entities)
			{
				GameEventHandler handler = ent.GetComponent<GameEventHandler> ();
				if (handler != null)
				{
					handler.displayStringTextboxEvent += DisplayText;
					handler.displayTextFileTextboxEvent += DisplayTextFromFile;
				}
			}
		}
			
		void Update () 
		{
			// if the textbox is not actively displaying text
			if (!activeDisplay)
			{
				return;
			}
			// if the user presses a button
			if (Input.GetButtonDown ("Cancel"))
			{
				Disable ();
			}
			if (Input.GetButtonDown("MainAction"))
			{
				if (!activeTyping)
				{
					currentLine += 1;
					if (currentLine > endLine)
					{
						Disable ();
					}
					else
					{
						StartCoroutine(ReadText(textLines[currentLine]));
					}
				}
			}
		}
		/// <summary>
		/// Public method for displaying text.
		/// </summary>
		/// <param name="_text">Text.</param>
		public void DisplayText(string _text)
		{
			textLines = _text.Split('\n');

			endLine = textLines.Length - 1;
			Enable ();
		}
		/// <summary>
		/// Public method for displaying text.
		/// </summary>
		/// <param name="_textFile">Text file.</param>
		public void DisplayTextFromFile(TextAsset _textFile)
		{
			textLines = _textFile.text.Split('\n');
			endLine = textLines.Length - 1;
			Enable ();
		}

		/// <summary>
		/// Enable this textbox.
		/// </summary>
		public void Enable()
		{
			foreach(Transform t in entities)
			{
				OW_Controller entity = t.GetComponent<OW_Controller> ();
				if (entity != null)
				{
					entity.Change ((int)OWStates.BlockMove);
				}
			}
			this.gameObject.SetActive (true);
			activeDisplay = true;
			currentLine = 0;

			StartCoroutine(ReadText(textLines[currentLine]));
		}

		/// <summary>
		/// Disable this textbox.
		/// </summary>
		public void Disable()
		{
			foreach(Transform t in entities)
			{
				OW_Controller player = t.GetComponent<OW_Controller> ();
				if (player != null)
				{
					player.Change ((int)OWStates.PlayerWait);
				}
			}
			this.gameObject.SetActive (false);
			activeDisplay = false;
		}

		private IEnumerator ReadText(string line)
		{
			int letterPos = 0;
			textArea.text = "";
			activeTyping = true;
			cancelTyping = false;
			while (activeTyping && !cancelTyping && (letterPos < line.Length - 1))
			{
				textArea.text += line [letterPos];
				letterPos += 1;
				yield return new WaitForSeconds (typeSpeed);
			}
			textArea.text = line;
			activeTyping = false;
			cancelTyping = false;
		}
	}

}