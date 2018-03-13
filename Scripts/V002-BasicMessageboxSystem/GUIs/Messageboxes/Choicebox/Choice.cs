using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GUIs.Messageboxes
{
	/// <summary>
	/// The choice is what populates a choicebox.
	/// </summary>
	public class Choice : MonoBehaviour, ISelectHandler, IDeselectHandler 
	{
		[SerializeField]
		protected Button selection;
		[SerializeField]
		protected HorizontalLayoutGroup layout;

		/// <summary>
		/// The image container for the cursor.
		/// </summary>
		public Image cursor;
		/// <summary>
		/// The icon for a particular choice (if applicable)
		/// </summary>
		public Image icon;
		/// <summary>
		/// The choice's description. This is what is displayed to the player.
		/// </summary>
		public Text desc;

		/// <summary>
		/// Click event delegate.
		/// </summary>
		public delegate void ClickEvent();
		/// <summary>
		/// On click method for when the player confirms a choice.
		/// </summary>
		public ClickEvent onClick;

		void Start () 
		{
			selection.onClick.AddListener (DoOnClick);
		}
		/// <summary>
		/// When the player selects the choice.
		/// </summary>
		/// <param name="eventData">Event data.</param>
		public void OnSelect(BaseEventData eventData)
		{
			cursor.color = new Color32 (255, 255, 255, 255);
		}
		/// <summary>
		/// When the player is done selecting the choice.
		/// </summary>
		/// <param name="data">Data.</param>
		public void OnDeselect(BaseEventData data)
		{
			cursor.color = new Color32 (255, 255, 255, 0);
		}
		/// <summary>
		/// Default click method.
		/// </summary>
		public void DoOnClick()
		{
			if (onClick != null)
			{
				onClick ();
			}
			//else
			//{
			//	Debug.Log ("Selected " + selection.name);
			//}
		}
	}
}
