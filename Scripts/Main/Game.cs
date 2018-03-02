using System.Collections.Generic;
using UnityEngine;

namespace Main
{
	/// <summary>
	/// The game keeps reference of the main camera and all its active entities, so that other classes may use them.
	/// </summary>
	public class Game : MonoBehaviour 
	{
		[SerializeField]
		private Camera mainCamera;

		[SerializeField]
		private List<Transform> entities;

		/// <summary>
		/// Gets the main camera.
		/// </summary>
		/// <value>The main camera.</value>
		public Camera MainCamera
		{
			get
			{ 
				return mainCamera;
			}
		}

		/// <summary>
		/// Gets the list of all entities in the game.
		/// </summary>
		/// <value>The entities.</value>
		public List<Transform> Entities
		{
			get
			{
				return entities;
			}
		}
	}
}
