using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GUIs.Panels
{
	/// <summary>
	/// This panel can expand and shrink.
	/// </summary>
	public class ScalePanel : PanelAppearance 
	{
		/// <summary>
		/// Activate and expand the panel.
		/// </summary>
		public override IEnumerator Enable()
		{
			yield return StartCoroutine(base.Enable ());
			yield return StartCoroutine(Expand(newX, newY, newWidth + padding, newHeight + padding));

			sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			sizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
			activePanel = false;
		}
		/// <summary>
		/// Initializes values.
		/// </summary>
		protected override IEnumerator Start ()
		{
			yield return StartCoroutine(base.Start());

			initWidth = 0;
			initHeight = 0;

			sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			sizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
		}
		/// <summary>
		/// Deactivate and shrink this panel.
		/// </summary>
		public override IEnumerator Disable()
		{
			sizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			sizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
			layout.padding = new RectOffset (0, 0, 0, 0);
			yield return StartCoroutine(Shrink(newX, newY, newWidth, newHeight));

			yield return StartCoroutine(base.Disable ());
		}
		/// <summary>
		/// Expand the panel to the new width and height.
		/// </summary>
		/// <param name="xMin">X minimum.</param>
		/// <param name="yMin">Y minimum.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		IEnumerator Expand(float xMin, float yMin, float width, float height)
		{
			// set to initial values
			Position (initX, initY, initWidth, initHeight);
			float currentWidth = initWidth, currentHeight = initHeight;
			while (true)
			{
				// expand size
				if (currentWidth < width)
				{
					currentWidth+=(6*tweenScale);
				}
				if (currentHeight < height)
				{
					currentHeight+=(4*tweenScale);
				}
				// if current size matches new size
				if (currentWidth >= Mathf.Abs (width) && currentHeight >= Mathf.Abs (height))
				{
					Position (xMin, yMin, width, height);
					break;
				}
				else
				{
					Position (xMin, yMin, currentWidth, currentHeight);
				}
				yield return new WaitForSeconds (0.01f);
			}
			yield return new WaitForSeconds (0.01f);
		}
		/// <summary>
		/// Shrink the panel to the initial width and height.
		/// </summary>
		/// <param name="xMin">X minimum.</param>
		/// <param name="yMin">Y minimum.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		IEnumerator Shrink(float xMin, float yMin, float width, float height)
		{
			activePanel = true;
			Position (xMin, yMin, width, height);
			while (true)
			{
				yield return new WaitForSeconds (0.01f);

				if (width > initWidth)
				{
					width-=(6*tweenScale);
				}
				if (height > initHeight)
				{
					height-=(4*tweenScale);
				}

				if (width <= initWidth && height <= initHeight)
				{
					Position (xMin, yMin, initWidth, initHeight);
					break;
				}
				else
				{
					Position (xMin, yMin, width, height);
				}
			}
		}
	}

}