using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Main;

namespace Overworld
{
	/// <summary>
	/// The map represents part of the current area the player or camera is in.
	/// </summary>
	public class Map : MonoBehaviour
	{
		/// <summary>
		/// Active entities on the map
		/// </summary>
		public List<Transform> entities;
		/// <summary>
		/// Find all entities and update rendering orders based upon them
		/// </summary>
	    void Start ()
	    {
			GameObject go = GameObject.FindWithTag ("Game");

			if (go.name == "Game")
			{
				Game g = go.GetComponent<Game> ();
				for (int e = 0; e < g.Entities.Count; e++)
				{
					SpriteRenderer spr = g.Entities[e].GetComponent<SpriteRenderer> ();
					if (spr != null)
					{
						entities.Add (g.Entities [e]);
					}
				}
				AddRenderEvents ();
			}
			else
			{
				Assert.IsNull (go, "Gameobject not found");
			}
	    }

		private void AddRenderEvents()
		{
			foreach (Transform ent in entities)
			{
				GameEventHandler handler = ent.GetComponent<GameEventHandler> ();
				if (handler != null)
				{
					handler.moveEvent += UpdateEntityRenderingOrders;
				}
			}
		}

		/// <summary>
		/// Updates the entity's rendering orders based upon position of last moved entity.
		/// </summary>
	    public void UpdateEntityRenderingOrders()
	    {
	        entities.Sort(delegate (Transform go1, Transform go2)
	        {
	            return go1.position.y.CompareTo(go2.position.y);
	        });

	        for (int i = 0; i < entities.Count; i++)
	        {
	            SpriteRenderer sp = entities[i].GetComponent<SpriteRenderer>();
				if (sp != null)
				{
					sp.sortingOrder = -i;
				}
	        }
	    }
	}
}
