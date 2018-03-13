using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GUIs.Messageboxes.ChoiceEvents;

namespace GUIs.Messageboxes
{
	/// <summary>
	/// The choicebox displays the choices a player can make.
	/// </summary>
	public class Choicebox : MessageBox 
	{
		[SerializeField]
		private List<Choice> choices;
		[SerializeField]
		protected Choice choiceObject;
		protected GameObject selectedObject;

		/// <summary>
		/// The choicebox's arguments.
		/// </summary>
		public ChoiceboxArgs choiceArgs;
		/// <summary>
		/// The cursor image.
		/// </summary>
		public Sprite cursorImage;

		void Start () 
		{
			if (choices == null)
			{
				choices = new List<Choice> ();
			}
			selectedObject = this.gameObject;
		}

		void Update()
		{
			if (EventSystem.current.currentSelectedGameObject == null)
			{
				EventSystem.current.SetSelectedGameObject(selectedObject);
			}
			else
			{
				selectedObject = EventSystem.current.currentSelectedGameObject;
			}
		}

		public void CloseBox()
		{
			StartCoroutine ("Close");
		}

		/// <summary>
		/// Public wrapper for displaying the choicebox.
		/// </summary>
		/// <returns>The choices.</returns>
		public IEnumerator DisplayChoices()
		{
			yield return StartCoroutine(Enable ());
		}
		/// <summary>
		/// Sets up the box's values and choices.
		/// </summary>
		protected override IEnumerator Render()
		{
			Panel.initX = choiceArgs.panelX;
			Panel.initY = choiceArgs.panelY;
			Panel.initWidth = choiceArgs.panelWidth;
			Panel.initHeight = choiceArgs.panelHeight;
			Panel.newX = choiceArgs.newPanelX;
			Panel.newY = choiceArgs.newPanelY;
			Panel.newWidth = choiceArgs.newPanelWidth;
			Panel.newHeight = choiceArgs.newPanelHeight;
			Panel.tweenScale = choiceArgs.tweenScale;
			Panel.Layout.padding.left = choiceArgs.choicePaddingLeft;
			Panel.Layout.padding.right = choiceArgs.choicePaddingRight;
			Panel.Layout.padding.top = choiceArgs.choicePaddingTop;
			Panel.Layout.padding.bottom = choiceArgs.choicePaddingBottom;
			Panel.Layout.spacing = choiceArgs.choiceSpacing;

			cursorImage = choiceArgs.cursorImage;
			// Clear the list of choices and create the choice objects
			choices.Clear ();
			for (int i = 0; i < choiceArgs.choiceCount; i++)
			{
				Choice c = Instantiate (choiceObject);
				c.desc.text = choiceArgs.desciptions [i];
				if (choiceArgs.icons [i] != null)
				{
					c.icon.sprite = choiceArgs.icons [i];
				}
				c.onClick = choiceArgs.choiceEvents [i].OnClick;
				choices.Add (c);
			}
			// Reset the position and add the close event
			foreach (Choice c in choices)
			{
				c.cursor.sprite = cursorImage;
				c.cursor.color = new Color32 (255, 255, 255, 0);
				c.onClick += CloseBox;

				c.transform.SetParent (Panel.transform);
			}

			yield return StartCoroutine(Panel.Enable ());

			Panel.SizeFit.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			Panel.SizeFit.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			// Set the choices active
			foreach (Choice c in choices)
			{
				c.gameObject.transform.localScale = Vector3.one;
				c.gameObject.SetActive (true);
				if (c.icon.sprite != null)
				{
					c.icon.enabled = true;
					c.icon.gameObject.SetActive (true);
				}
			}
			// Set the selected choice to the first choice
			selectedObject = choices[0].gameObject;
			EventSystem.current.SetSelectedGameObject(selectedObject);
		}

		/// <summary>
		/// Deactivate this choicebox and its choices.
		/// </summary>
		protected override IEnumerator Disable()
		{
			foreach (Choice c in choices)
			{
				c.gameObject.SetActive (false);
			}
			EventSystem.current.SetSelectedGameObject(null);
			yield return StartCoroutine(base.Disable ());
		}
	}

}