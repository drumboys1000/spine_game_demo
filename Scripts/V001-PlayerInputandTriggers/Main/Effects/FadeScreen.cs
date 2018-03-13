using System.Collections;
using UnityEngine;
using Entities.Overworld;

public class FadeScreen : MonoBehaviour {

	public byte fadeInColorInterval = 12;
	public byte fadeOutColorInterval = 20;

	private SpriteRenderer fade;

	void Start () 
	{
		fade = this.GetComponent<SpriteRenderer> ();
		fade.enabled = false;
	}

	public IEnumerator TeleportFadeIn(OW_Controller pc, float waitTime, float fadeTime)
	{
		pc.Change((int)OWStates.BlockMove);
		fade.enabled = true;
		yield return StartCoroutine (FadeIn (fadeTime));
		yield return new WaitForSeconds (waitTime);
	}

	public IEnumerator TeleportFadeOut(OW_Controller pc, float waitTime, float fadeTime)
	{
		yield return StartCoroutine (FadeOut (fadeTime));
		yield return new WaitForSeconds (waitTime);
		fade.enabled = false;
		pc.Change ((int)OWStates.PlayerWait);
	}

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
