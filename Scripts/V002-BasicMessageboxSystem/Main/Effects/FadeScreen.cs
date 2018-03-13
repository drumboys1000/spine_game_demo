using System.Collections;
using UnityEngine;
using Entities.Overworld;

namespace Main.Effects
{
	/// <summary>
	/// The fade screen is a useful Camera effect. Use it for transitions to different parts of the game.
	/// </summary>
	public class FadeScreen : MonoBehaviour 
	{
		private SpriteRenderer fade;

		/// <summary>
		/// The fade in color interval.
		/// </summary>
		public byte fadeInColorInterval = 12;
		/// <summary>
		/// The fade out color interval.
		/// </summary>
		public byte fadeOutColorInterval = 20;

		void Start () 
		{
			fade = this.GetComponent<SpriteRenderer> ();
			fade.enabled = false;
		}
		/// <summary>
		/// Public wrapper for fading in. Use with the Teleport trigger.
		/// </summary>
		/// <returns>The fade in.</returns>
		/// <param name="pc">Pc.</param>
		/// <param name="waitTime">Wait time.</param>
		/// <param name="fadeTime">Fade time.</param>
		public IEnumerator TeleportFadeIn(OW_Controller pc, float waitTime, float fadeTime)
		{
			pc.Change((int)OWStates.BlockMove);
			fade.enabled = true;
			yield return StartCoroutine (FadeIn (fadeTime));
			yield return new WaitForSeconds (waitTime);
		}
		/// <summary>
		/// Public wrapper for fading out. Use with the Teleport trigger.
		/// </summary>
		/// <returns>The fade out.</returns>
		/// <param name="pc">Pc.</param>
		/// <param name="waitTime">Wait time.</param>
		/// <param name="fadeTime">Fade time.</param>
		public IEnumerator TeleportFadeOut(OW_Controller pc, float waitTime, float fadeTime)
		{
			yield return StartCoroutine (FadeOut (fadeTime));
			yield return new WaitForSeconds (waitTime);
			fade.enabled = false;
			pc.Change ((int)OWStates.PlayerWait);
		}
		/// <summary>
		/// Public wrapper for fading in when the player quits the game. Use with the QuitGame choice event.
		/// </summary>
		/// <returns>The game fade in.</returns>
		/// <param name="waitTime">Wait time.</param>
		/// <param name="fadeTime">Fade time.</param>
		public IEnumerator QuitGameFadeIn(/*OW_Controller pc, */float waitTime, float fadeTime)
		{
			//pc.Change((int)OWStates.BlockMove);
			fade.enabled = true;
			yield return StartCoroutine (FadeIn (fadeTime));
			yield return new WaitForSeconds (waitTime);
			//fadeScreen
			#if UNITY_EDITOR
			// Application.Quit() does not work in the editor so
			// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit();
			#endif
		}
		/// <summary>
		/// The screen fades in.
		/// </summary>
		/// <returns>The in.</returns>
		/// <param name="fadeTime">Fade time.</param>
		private IEnumerator FadeIn(float fadeTime)
		{
			fade.enabled = true;
			while (true)
			{
				yield return new WaitForSeconds (fadeTime);
				fade.color += new Color32 (0, 0, 0, fadeInColorInterval);
				if (fade.color.a >= 0.95f)
				{
					fade.color = new Color32 (0, 0, 0, 255);
					break;
				}
			}
		}
		/// <summary>
		/// The screen fades out.
		/// </summary>
		/// <returns>The out.</returns>
		/// <param name="fadeTime">Fade time.</param>
		private IEnumerator FadeOut(float fadeTime)
		{
			while (true)
			{
				yield return new WaitForSeconds (fadeTime);
				fade.color -= new Color32 (0, 0, 0, fadeOutColorInterval);

				if (fade.color.a <= 0f)
				{
					fade.color = new Color32 (0, 0, 0, 0);
					break;
				}
			}
		}
	}
}