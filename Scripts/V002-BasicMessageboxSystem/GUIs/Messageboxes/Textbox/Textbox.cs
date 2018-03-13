using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GUIs.Messageboxes
{
	/// <summary>
	/// The textbox displays messages on the screen
	/// Messages are received through the message box system.
	/// Passed along by events.
	/// </summary>
	public class Textbox : MessageBox 
	{
		[SerializeField]
		private Text mainTextArea;
		//[SerializeField]
		//private NamePanel namePanel;
		//[SerializeField]
		//private Text nameTextArea;
		[SerializeField]
		private Choicebox choicebox;
		[SerializeField]
		private int currentLine;
		private int endLine;
		/// <summary>
		/// The current speed at which to delay characters of a text.
		/// </summary>
		private float currentTypeDelay;
		/// <summary>
		/// The number of characters to output at a time.
		/// </summary>
		private int characterRate;
		/// <summary>
		/// Determines whether or not the textbox's text is still scrolling
		/// </summary>
		private bool activeTyping;
		/// <summary>
		/// Determines whether or not to cancel typing
		/// </summary>
		private bool cancelTyping;

		/// <summary>
		/// The text split into lines
		/// </summary>
		public string[] textLines;
		/// <summary>
		/// The text arguments table
		/// </summary>
		public TextboxArgs[] textArgsTable;
		/// <summary>
		/// The speed at which to delay characters of a text.
		/// </summary>
		public float typeDelay;
		/// <summary>
		/// Determines whether or not to display text like a character is speaking.
		/// </summary>
		public bool textTalk;
		/// <summary>
		/// Determines whether or not to raise the name panel.
		/// </summary>
		public bool raiseName;
		/// <summary>
		/// Gets the main text portion of the text box.
		/// </summary>
		/// <value>The main text.</value>
		public Text MainText
		{
			get { return mainTextArea; }
		}
		/// <summary>
		/// Gets the choicebox, if applicable.
		/// </summary>
		/// <value>The choicebox.</value>
		public Choicebox Choicebox
		{
			get { return choicebox; }
		}
			
		void Update () 
		{
			// if the textbox is not actively displaying text
			if (!IsActive)
			{
				return;
			}
			if (!choicebox.IsActive)
			{
				// if the user presses a button
				HandleInput();
			}
		}
		/// <summary>
		/// Used for user input, if neccessary
		/// </summary>
		protected override void HandleInput ()
		{
			if (Input.GetButtonDown ("Cancel"))
			{
				if (!panel.IsActive && !choicebox.IsActive)
				{
					StartCoroutine (Disable ());
				}
			}
			if (Input.GetButton ("MainAction"))
			{
				characterRate = 3;
			}
			if (Input.GetButtonUp ("MainAction"))
			{
				characterRate = 1;
			}
			if (Input.GetButtonDown("MainAction"))
			{
				if (!activeTyping && !choicebox.IsActive)
				{
					currentLine += 1;
					if (currentLine > endLine)
					{
						StartCoroutine (Disable ());

					}
					else
					{
						if (currentLine <= endLine)
						{
							StartCoroutine (Render ());
						}
					}
				}
			}
		}
		/// <summary>
		/// Public method for displaying text.
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="_textLines">Text lines.</param>
		/// <param name="args">Arguments.</param>
		public IEnumerator DisplayText(string[] _textLines, TextboxArgs[] args)
		{
			textLines = _textLines;
			textArgsTable = args;

			characterRate = 1;
			currentLine = 0;
			endLine = textLines.Length - 1;
			currentTypeDelay = typeDelay;
			yield return StartCoroutine(Enable ());
		}
		/// <summary>
		/// Public method for displaying text.
		/// </summary>
		public IEnumerator Display()
		{
			characterRate = 1;
			currentLine = 0;
			endLine = textLines.Length - 1;
			currentTypeDelay = typeDelay;
			yield return StartCoroutine(Enable ());
		}
		/// <summary>
		/// Sets up the box's values. 
		/// Also sets up the panel, image, name and choicebox values.
		/// </summary>
		protected override IEnumerator Render()
		{
			activeTyping = true;
			// no specified type in that line's args. Used for custom boxes.
			if (textArgsTable[currentLine].type == MessageBoxTypes.None)
			{
				Panel.initX = textArgsTable [currentLine].panelX;
				Panel.initY = textArgsTable [currentLine].panelY;
				Panel.initWidth = textArgsTable [currentLine].panelWidth;
				Panel.initHeight = textArgsTable [currentLine].panelHeight;
				Panel.newX = textArgsTable [currentLine].newPanelX;
				Panel.newY = textArgsTable [currentLine].newPanelY;
				Panel.newWidth = textArgsTable [currentLine].newPanelWidth;
				Panel.newHeight = textArgsTable [currentLine].newPanelHeight;
				Panel.tweenScale = textArgsTable [currentLine].tweenScale;
				Panel.padding = textArgsTable [currentLine].padding;

				mainTextArea.fontSize = textArgsTable [currentLine].fontSize;
				mainTextArea.alignment = textArgsTable [currentLine].alignment;

				textTalk = textArgsTable [currentLine].textTalk;
			}
			//nameTextArea.text = textArgsTable [currentLine].entityName;
			typeDelay = textArgsTable [currentLine].typeDelay;
			raiseName = textArgsTable [currentLine].raiseName;

			mainTextArea.rectTransform.localPosition = new Vector2 (0, 0);
			mainTextArea.rectTransform.sizeDelta = new Vector2 (Panel.initWidth, Panel.initHeight);

			if (currentLine == 0)
			{
				yield return StartCoroutine(Panel.Enable());
			}
			// Display name
			if(raiseName)
			{
				// Start Coroutine - raise name panel
			}
			// Display image
			Sprite sprite = textArgsTable [currentLine].image;
			if (sprite != null)
			{
				image.sprite = sprite;
				image.rectTransform.sizeDelta = 
					new Vector2 (Panel.newHeight - Panel.padding, Panel.newHeight - Panel.padding);
				LayoutElement layout = image.GetComponent<LayoutElement> ();
				layout.minWidth = Mathf.Abs(Panel.newHeight - Panel.padding);
				layout.minHeight = Mathf.Abs(Panel.newHeight - Panel.padding);
				layout.preferredWidth = Mathf.Abs(Panel.newHeight - Panel.padding);
				layout.preferredHeight = Panel.newHeight - Panel.padding;

				LayoutElement textLayout = mainTextArea.GetComponent<LayoutElement> ();
				textLayout.minWidth = Mathf.Abs(Panel.newWidth - layout.minWidth - (Panel.padding*2));
				textLayout.minHeight = layout.minHeight;
				textLayout.preferredWidth = Mathf.Abs(Panel.newWidth - layout.minWidth - (Panel.padding*2));
				textLayout.preferredHeight = layout.preferredHeight;
				image.gameObject.SetActive (true);
			}
			else
			{
				LayoutElement textLayout = mainTextArea.GetComponent<LayoutElement> ();
				textLayout.minWidth = Mathf.Abs(Panel.newWidth - (Panel.padding*2));
				textLayout.minHeight = Mathf.Abs(Panel.newHeight - Panel.padding);
				textLayout.preferredWidth = textLayout.minWidth;
				textLayout.preferredHeight = textLayout.minHeight;
				image.gameObject.SetActive (false);
			}
			// Display text
			if (textLines[currentLine] != "_")
			{
				//mainTextArea.gameObject.SetActive (true);
				yield return StartCoroutine(ReadText(textLines[currentLine]));
				//Debug.Log ("Display line " + textLines[currentLine]);
			}
			// Display choicebox
			ChoiceboxArgs choiceArgs = textArgsTable [currentLine].choiceBoxArgs;
			if (choiceArgs != null)
			{
				choicebox = Instantiate (choicebox);
				choicebox.transform.SetParent (this.transform);
				choicebox.transform.localPosition = Vector2.zero;
				choicebox.choiceArgs = choiceArgs;
				yield return StartCoroutine (choicebox.DisplayChoices ());
			}
			activeTyping = false;
		}
		/// <summary>
		/// Deactivate this textbox.
		/// </summary>
		protected override IEnumerator Disable()
		{
			//nameTextArea.gameObject.SetActive (false);
			mainTextArea.gameObject.SetActive (false);
			image.gameObject.SetActive (false);
			yield return StartCoroutine(base.Disable ());
		}
		/// <summary>
		/// Reads the current line's text.
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="line">Line.</param>
		private IEnumerator ReadText(string line)
		{
			if (textTalk)
			{
				int letterPos = 0;
				mainTextArea.text = "";
				cancelTyping = false;
				while (activeTyping && !cancelTyping && (letterPos < line.Length - 1))
				{
					for (int c = 0; c < characterRate; c++)
					{
						if (letterPos == line.Length - 1)
						{
							break;
						}
						mainTextArea.text += line [letterPos];
						letterPos += 1;
					}
					yield return new WaitForSeconds (currentTypeDelay);
				}
			}
			mainTextArea.text = line;
			//activeTyping = false;
			cancelTyping = false;
		}
	}
}