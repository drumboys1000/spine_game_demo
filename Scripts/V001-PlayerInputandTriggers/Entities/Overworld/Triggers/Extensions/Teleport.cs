using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using Main;

namespace Entities.Overworld.Triggers
{
	/// <summary>
	/// This trigger teleports the player to another area.
	/// Useful for entering/exiting areas.
	/// </summary>
	public class Teleport : Trigger 
	{
		/// <summary>
		/// The target coordinates to transport the player to.
		/// </summary>
		public float targetX, targetY;

		/// <summary>
		/// The offset time to wait for each fade.
		/// </summary>
		public float waitTime = 0.3f;

		/// <summary>
		/// The amount of time to fade in or out for.
		/// </summary>
		public float fadeTime = 0.01f;

		private FadeScreen fadeScreen;

		/// <summary>
		/// Get the camera's fade screen.
		/// </summary>
		protected override void Start()
		{
			type = TriggerType.Teleport;

			GameObject go = GameObject.FindWithTag ("Game");

			if (go.name == "Game")
			{
				Game g = go.GetComponent<Game> ();
				fadeScreen = g.MainCamera.GetComponentInChildren<FadeScreen> ();
			}
			else
			{
				Assert.IsNull (go, "Gameobject not found");
			}
		}

		protected override void OnTriggerEnter2D(Collider2D other)
		{
			StartCoroutine(Move(other));
		}

		/// <summary>
		/// Move the player to the target destination
		/// </summary>
		/// <param name="other">Other.</param>
		IEnumerator Move(Collider2D other)
		{
			OW_PlayerController player = other.GetComponent<OW_PlayerController> ();
			if (fadeScreen != null && player != null)
			{
				yield return StartCoroutine (fadeScreen.TeleportFadeIn (player, waitTime, fadeTime));
				other.transform.position = new Vector2(targetX, targetY);
				yield return StartCoroutine (fadeScreen.TeleportFadeOut (player, waitTime, fadeTime));
			}
		}
	}

}