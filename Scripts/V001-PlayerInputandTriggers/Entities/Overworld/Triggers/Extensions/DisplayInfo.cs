using UnityEngine;
using Main;

namespace Entities.Overworld.Triggers
{
	/// <summary>
	/// This trigger is useful for displaying information upon button press
	/// i.e. examining stuff.
	/// </summary>
	public class DisplayInfo : Trigger {

		/// <summary>
		/// text to display
		/// </summary>
		public string info = "There is an object here!";

		public TextAsset infoFile;

		private GameEventHandler events;

		protected override void Start()
		{
			type = TriggerType.DisplayInfo;

			events = this.transform.GetComponent<GameEventHandler> ();
			if (events == null)
			{
				events = this.transform.GetComponentInParent<GameEventHandler> ();
			}
		}

		public override void OnButtonPress(Collider2D other)
		{
			if (infoFile != null)
			{
				events.CallDisplayTextboxEvent (infoFile);
			}
			else
			{
				events.CallDisplayTextboxEvent (info);
			}
		}
	}
}