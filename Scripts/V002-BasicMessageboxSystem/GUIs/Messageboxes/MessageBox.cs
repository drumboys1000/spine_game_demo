using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GUIs.Panels;

namespace GUIs.Messageboxes
{
	/// <summary>
	/// This is the basics a messagebox needs when displayed. Must be implemented by subclasses.
	/// </summary>
	public abstract class MessageBox : MonoBehaviour 
	{
		[SerializeField]
		protected PanelAppearance panel;
		[SerializeField]
		protected Image image;

		[SerializeField]
		protected bool activeDisplay;
		[SerializeField]
		protected bool finished;

		/// <summary>
		/// The panel is used to contain the messagebox's contents. A panel appearance dictates how a panel appears on the screen.
		/// </summary>
		/// <value>The panel.</value>
		public PanelAppearance Panel
		{
			get { return panel; }
		}
		/// <summary>
		/// An image the box might need to display.
		/// </summary>
		public Image Image
		{
			get { return image; }
		}
			
		/// <summary>
		/// Gets a value indicating whether the box is active.
		/// </summary>
		/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
		public bool IsActive
		{
			get { return activeDisplay; }
		}
		/// <summary>
		/// Gets a value indicating whether the box is finished.
		/// </summary>
		/// <value><c>true</c> if this instance is finished; otherwise, <c>false</c>.</value>
		public bool IsFinished
		{
			get { return finished; }
		}

		/// <summary>
		/// Sets up the box's values
		/// </summary>
		protected abstract IEnumerator Render ();

		/// <summary>
		/// Used for user input, if neccessary
		/// </summary>
		protected virtual void HandleInput () { }

		/// <summary>
		/// Activate the box.
		/// </summary>
		protected virtual IEnumerator Enable ()
		{
			activeDisplay = true;
			finished = false;
			yield return StartCoroutine(Render ());
		}

		/// <summary>
		/// Deactivate the box.
		/// </summary>
		protected virtual IEnumerator Disable ()
		{
			yield return StartCoroutine (Panel.Disable ());
			activeDisplay = false;
			this.gameObject.SetActive (false);
			finished = true;
		}

		/// <summary>
		/// Public wrapper for deactivating the box.
		/// </summary>
		public IEnumerator Close()
		{
			if (this.gameObject.activeSelf)
			{
				yield return StartCoroutine (Disable ());
			}
		}
	}

}