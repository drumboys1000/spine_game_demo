using UnityEngine;
using Main.Effects;
namespace GUIs.Messageboxes.ChoiceEvents
{
	/// <summary>
	/// This event cause the screen to fade in and quits the game.
	/// </summary>
	public class QuitGame : ChoiceEvent
	{
		[SerializeField]
		protected MessageBoxSystem messageBoxSystem;
		[SerializeField]
		protected FadeScreen fadeScreen;

		public override void OnClick()
		{
			StartCoroutine(messageBoxSystem.CloseBoxes (true));
			StartCoroutine(fadeScreen.QuitGameFadeIn (3, 0.1f));
		}
	}
}
