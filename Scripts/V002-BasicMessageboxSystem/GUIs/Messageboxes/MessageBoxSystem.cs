using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUIs.Messageboxes
{
	/// <summary>
	/// The message box system handles mapping message I/O.
	/// A Message object is taken as input and a message box is created using its text values and arguments.
	/// </summary>
	public class MessageBoxSystem : MonoBehaviour 
	{
		[SerializeField]
		private Textbox textbox;
		[SerializeField]
		private Choicebox choicebox;

		private Queue<Textbox> messageBoxes;

		private Queue<Textbox> boxesToDestroy;
		[SerializeField]
		private Textbox currentBox;
		//[SerializeField]
		private MessageBoxTypes currentType;
		private Message currentMessage;
		//[SerializeField]
		private string[] fullMessageLines;
		//[SerializeField]
		private int currentMessageLineCount;
		//[SerializeField]
		private int currentLine;
		//[SerializeField]
		private int endLine;

		private bool activeDisplay;

		private bool stayActive;

		private delegate Textbox Box(string[] text, TextboxArgs[] args);
		private Box[] boxDelegates;

		/// <summary>
		/// Gets the current message box type.
		/// </summary>
		/// <value>The type of the current.</value>
		public MessageBoxTypes CurrentType
		{
			get { return currentType; }
		}
		/// <summary>
		/// Gets or sets a value indicating whether the system is active.
		/// </summary>
		/// <value><c>true</c> if the system is active; otherwise, <c>false</c>.</value>
		public bool IsActive
		{
			get { return activeDisplay; }
			set { activeDisplay = value; }
		}
			
		void Start () 
		{
			if (boxDelegates == null)
			{
				CreateDelegates ();
			}
			if (messageBoxes == null)
			{
				messageBoxes = new Queue<Textbox> ();
			}
			if (boxesToDestroy == null)
			{
				boxesToDestroy = new Queue<Textbox> ();
			}
		}

		void Update () 
		{
			// While the current box is open
			if (currentBox != null)
			{
				if (currentBox.IsFinished && messageBoxes.Count != 0)
				{
					StartCoroutine (Render ());
				}
				if (currentBox.IsFinished && !stayActive && messageBoxes.Count == 0)
				{
					StartCoroutine(CloseBoxes (false));
				}

			}
		}
		/// <summary>
		/// Displays the full message.
		/// </summary>
		/// <param name="message">Message.</param>
		public IEnumerator DisplayMessage(Message message)
		{
			activeDisplay = true;
			yield return StartCoroutine (DestroyFinishedBoxes ());
			currentLine = 0;
			endLine = message.numberOfLines - 1;

			fullMessageLines = message.messageFile.text.Split ('\n');
			currentMessage = message;

			messageBoxes.Clear ();
			yield return StartCoroutine(CreateBoxes());
		}
		/// <summary>
		/// Displays a particular part of the message.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="startingLine">Starting line.</param>
		/// <param name="endingLine">Ending line.</param>
		public IEnumerator DisplayMessage(Message message, int startingLine, int endingLine)
		{
			activeDisplay = true;
			yield return StartCoroutine (DestroyFinishedBoxes ());
			currentLine = startingLine;
			endLine = endingLine;

			fullMessageLines = message.messageFile.text.Split ('\n');
			currentMessage = message;

			messageBoxes.Clear ();
			yield return StartCoroutine (CreateBoxes());
		}
		/// <summary>
		/// Closes all open boxes and deactivates the system.
		/// </summary>
		public IEnumerator CloseBoxes(bool keepActive)
		{
			stayActive = keepActive;
			if (choicebox.IsActive)
			{
				yield return StartCoroutine(currentBox.Choicebox.Close ());
			}
			if (currentBox.IsActive)
			{
				yield return StartCoroutine (currentBox.Close ());
			}
			yield return StartCoroutine (DestroyFinishedBoxes ());
			messageBoxes.Clear ();
			activeDisplay = keepActive;
		}

		private IEnumerator DestroyFinishedBoxes()
		{
			while (messageBoxes.Count > 0)
			{
				Textbox leftover = messageBoxes.Dequeue ();
				boxesToDestroy.Enqueue (leftover);
			}
			while(boxesToDestroy.Count > 0)
			{
				Textbox deadBox = boxesToDestroy.Dequeue ();
				Destroy (deadBox.gameObject);
			}
			yield return new WaitForSeconds (0.01f);
		}

		/// <summary>
		/// Creates the boxes usings each line's text and arguments.
		/// </summary>
		private IEnumerator CreateBoxes()
		{
			while (currentLine <= endLine)
			{
				currentType = currentMessage.messageBoxArguments [currentLine].type;

				string[] currentLines = GetRespectiveLines ();
				TextboxArgs[] args = GetRespectiveArgs<TextboxArgs>().ToArray();
				Textbox boxToAdd = boxDelegates [(int)currentType] (currentLines, args);

				messageBoxes.Enqueue (boxToAdd);
				currentLine += currentMessageLineCount;
			}
			yield return StartCoroutine (Render ());
		}
		/// <summary>
		/// Render the current box.
		/// </summary>
		private IEnumerator Render()
		{
			currentBox = messageBoxes.Dequeue ();
			boxesToDestroy.Enqueue (currentBox);
			currentBox.transform.SetParent (this.transform);
			currentBox.transform.localPosition = new Vector2 (0, 0);
			currentBox.gameObject.SetActive (true);
			currentBox.Choicebox.gameObject.SetActive (true);

			yield return StartCoroutine (currentBox.Display ());
		}
		/// <summary>
		/// Gets the respective amount of text for the current type of box.
		/// </summary>
		/// <returns>The respective lines.</returns>
		private string[] GetRespectiveLines()
		{
			List<string> textLines = new List<string>();

			textLines.Add (fullMessageLines [currentLine]);
			currentMessageLineCount = 1;

			if (currentMessage.numberOfLines > 1)
			{
				int nextLine = currentLine + 1;
				MessageBoxTypes nextType;

				if (nextLine > endLine)
				{
					nextType = MessageBoxTypes.None;
				}
				else
				{
					nextType = currentMessage.messageBoxArguments [nextLine].type;
				}

				while (CurrentType == nextType)
				{
					textLines.Add(fullMessageLines[nextLine]);
					currentMessageLineCount++;
					nextLine++;

					if (nextLine >= currentMessage.numberOfLines)
					{
						break;
					}
					else
					{
						nextType = currentMessage.messageBoxArguments [nextLine].type;
					}
				}
			}
			return textLines.ToArray ();
		}
		/// <summary>
		/// Gets the respective arguments for the current type of box.
		/// </summary>
		/// <returns>The respective arguments.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		private List<T> GetRespectiveArgs<T>() where T : MessageBoxArgs
		{
			List<T> argsList = new List<T>();

			for(int a = currentLine; a < currentLine + currentMessageLineCount; a++)
			{
				var args = currentMessage.messageBoxArguments [a].GetArgs<T> ();
				argsList.Add (args);
			}

			return argsList;
		}
		/// <summary>
		/// Creates the delegates for handling creation of particular box types.
		/// </summary>
		private void CreateDelegates()
		{
			int boxTypeCount = Enum.GetNames(typeof(MessageBoxTypes)).Length;
			boxDelegates = new Box[boxTypeCount];

			boxDelegates [1] = FixedDialogue;
			boxDelegates [2] = FixedNoDialog;
			boxDelegates [3] = FittedSmall;
			boxDelegates [4] = FittedMedium;
			boxDelegates [5] = ImageBox;
		}
		/// <summary>
		/// The standard bottom fixed dialogue box. Useful for character speak.
		/// </summary>
		/// <returns>The box.</returns>
		/// <param name="text">Text.</param>
		/// <param name="args">Arguments.</param>
		private Textbox FixedDialogue(string[] text, TextboxArgs[] args)
		{
			Textbox box = Instantiate (textbox);
			box.Panel.initX = 0;
			box.Panel.initY = -240;
			box.Panel.initWidth = 5;
			box.Panel.initHeight = 5;
			box.Panel.newX = 0;
			box.Panel.newY = -240;
			box.Panel.newWidth = 1200;
			box.Panel.newHeight = 220;
			box.Panel.tweenScale = 12;
			box.Panel.padding = 30;

			box.Panel.Layout.childAlignment = TextAnchor.UpperLeft;

			box.MainText.fontSize = 45;
			box.MainText.alignment = TextAnchor.UpperLeft;

			box.textTalk = true;

			box.textLines = text;
			box.textArgsTable = args;

			return box;
		}
		/// <summary>
		/// The standard bottom fixed box. Useful for sign or message reads.
		/// </summary>
		/// <returns>The box.</returns>
		/// <param name="text">Text.</param>
		/// <param name="args">Arguments.</param>
		private Textbox FixedNoDialog(string[] text, TextboxArgs[] args)
		{
			Textbox box = Instantiate (textbox);
			box.Panel.initX = 0;
			box.Panel.initY = -240;
			box.Panel.initWidth = 5;
			box.Panel.initHeight = 5;
			box.Panel.newX = 0;
			box.Panel.newY = -240;
			box.Panel.newWidth = 1200;
			box.Panel.newHeight = 220;
			box.Panel.tweenScale = 12;
			box.Panel.padding = 30;

			box.Panel.Layout.childAlignment = TextAnchor.UpperLeft;

			box.MainText.fontSize = 45;
			box.MainText.alignment = TextAnchor.UpperLeft;

			box.textTalk = false;

			box.textLines = text;
			box.textArgsTable = args;

			return box;
		}
		/// <summary>
		/// A small center fitted text box. Useful for short comments.
		/// </summary>
		/// <returns>The box.</returns>
		/// <param name="text">Text.</param>
		/// <param name="args">Arguments.</param>
		private Textbox FittedSmall(string[] text, TextboxArgs[] args)
		{
			Textbox box = Instantiate (textbox);
			box.Panel.initX = 0;
			box.Panel.initY = 0;
			box.Panel.initWidth = 0;
			box.Panel.initHeight = 0;
			box.Panel.newX = 0;
			box.Panel.newY = 0;
			box.Panel.newWidth = 200;
			box.Panel.newHeight = 64;
			box.Panel.tweenScale = 4;
			box.Panel.padding = 15;

			box.MainText.resizeTextForBestFit = true;
			box.MainText.resizeTextMaxSize = 18;
			box.MainText.alignment = TextAnchor.MiddleCenter;

			box.textTalk = true;

			box.textLines = text;
			box.textArgsTable = args;

			return box;
		}
		/// <summary>
		/// A medium center fitted text box. Useful for item received/party member joined/game event stuff.
		/// </summary>
		/// <returns>The box.</returns>
		/// <param name="text">Text.</param>
		/// <param name="args">Arguments.</param>
		private Textbox FittedMedium(string[] text, TextboxArgs[] args)
		{
			Textbox box = Instantiate (textbox);
			box.Panel.initX = 0;
			box.Panel.initY = 0;
			box.Panel.initWidth = 3;
			box.Panel.initHeight = 3;
			box.Panel.newX = 0;
			box.Panel.newY = 0;
			box.Panel.newWidth = 400;
			box.Panel.newHeight = 128;
			box.Panel.tweenScale = 5;
			box.Panel.padding = 20;

			box.MainText.resizeTextForBestFit = true;
			box.MainText.resizeTextMaxSize = 32;
			box.MainText.alignment = TextAnchor.MiddleLeft;

			box.textTalk = false;

			box.textLines = text;
			box.textArgsTable = args;

			return box;
		}
		/// <summary>
		/// A textless box. Useful for only displaying images.
		/// </summary>
		/// <returns>The box.</returns>
		/// <param name="text">Text.</param>
		/// <param name="args">Arguments.</param>
		private Textbox ImageBox(string[] text, TextboxArgs[] args)
		{
			Textbox box = Instantiate (textbox);
			box.Panel.initX = 0;
			box.Panel.initY = 0;
			box.Panel.initWidth = 3;
			box.Panel.initHeight = 3;
			box.Panel.newX = 0;
			box.Panel.newY = 0;
			box.Panel.newWidth = 300;
			box.Panel.newHeight = 300;
			box.Panel.tweenScale = 7;
			box.Panel.padding = 20;

			box.textLines = text;
			box.textArgsTable = args;

			return box;
		}
	}
}