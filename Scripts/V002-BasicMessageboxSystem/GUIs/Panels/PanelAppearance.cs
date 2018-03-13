using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GUIs.Panels
{
	/// <summary>
	/// This is how the panel appears and disappears on screen.
	/// </summary>
	public abstract class PanelAppearance : MonoBehaviour 
	{
		protected RectTransform rt;
		[SerializeField]
		protected ContentSizeFitter sizeFitter;
		[SerializeField]
		protected HorizontalOrVerticalLayoutGroup layout;
		protected bool activePanel;

		/// <summary>
		/// The texture a panel uses.
		/// </summary>
		public Sprite texture;
		/// <summary>
		/// The panel's initial position and size.
		/// </summary>
		public float initX, initY, initWidth, initHeight;
		/// <summary>
		/// The panel's new position and size.
		/// </summary>
		public float newX, newY, newWidth, newHeight;
		/// <summary>
		/// Indicates the time it takes for a panel to change appearance. Larger values mean quicker scales.
		/// </summary>
		public float tweenScale;
		/// <summary>
		/// A box's padding.
		/// </summary>
		public int padding;
		/// <summary>
		/// Gets the panel's size fitter, which is used for fitting content.
		/// </summary>
		/// <value>The size fit.</value>
		public ContentSizeFitter SizeFit
		{
			get { return sizeFitter; }
		}
		/// <summary>
		/// Gets the layout group for the panel, which can determine how its children are layed out.
		/// </summary>
		/// <value>The layout.</value>
		public HorizontalOrVerticalLayoutGroup Layout
		{
			get { return layout; }
		}
		/// <summary>
		/// Determines whether this panel is actively appearing or disappearing.
		/// </summary>
		/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
		public bool IsActive
		{
			get { return activePanel; }
		}
		/// <summary>
		/// Activate the panel.
		/// </summary>
		public virtual IEnumerator Enable()
		{
			activePanel = true;
			yield return StartCoroutine (Start ());
		}
		/// <summary>
		/// Initializes values.
		/// </summary>
		protected virtual IEnumerator Start ()
		{
			rt = this.transform.GetComponent<RectTransform> ();

			Image img = this.transform.GetComponent<Image> ();
			img.sprite = texture;
			img.enabled = true;

			layout.padding = new RectOffset (padding, padding, padding, padding);
			layout.spacing = padding;

			if (tweenScale == 0)
			{
				tweenScale = 1;
			}
			yield return new WaitForSeconds (0);
		}
		/// <summary>
		/// Deactivate this panel.
		/// </summary>
		public virtual IEnumerator Disable()
		{
			activePanel = false;
			yield return new WaitForSeconds (0);
		}
		/// <summary>
		/// Changes the position and size of the panel.
		/// </summary>
		/// <param name="xMin">X minimum.</param>
		/// <param name="yMin">Y minimum.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		protected void Position(float xMin, float yMin, float width, float height)
		{
			rt.localPosition = new Vector2 (xMin, yMin);
			rt.sizeDelta = new Vector2 (Mathf.Abs(width), Mathf.Abs(height));
		}
		/// <summary>
		/// Centers the panel and changes the size.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		protected void CenterPosition(float width, float height)
		{
			Position (0, 0, width, height);
		}
	}
}