﻿using UnityEngine;
using System.Collections;


namespace game.unit {

	/// <summary>
	/// C targeting controller.
	/// Controller attached to a unit
	/// </summary>
	public class CTargetingController : MonoBehaviour {

		[SerializeField]
		protected GameObject m_currentTarget;


		[SerializeField]
		protected float m_range; 

		public GameObject CurrentTarget {
			get { return m_currentTarget; }
			set { m_currentTarget = value; } 
		}

		public float Range {
			get { return m_range; }
			set { m_range = value; } 
		}


		public void Update() {

			if(CurrentTarget == null) {
				//search 
				//TODO replace with optimized code
				SelectClosestTarget();
			}

		}

		public void SelectClosestTarget() {
			Collider[] colliders = Physics.OverlapSphere(transform.position, m_range);

			float closestDist = Mathf.Infinity;

			foreach(Collider col in colliders) {

				IDestructible dest = col.GetComponent<IDestructible>();

				if(dest != null) {

					if(dest.GameObject == gameObject) {
						continue;
					}

					float dist = Vector3.Distance(col.transform.position, transform.position);
					if(dist < closestDist) {
						//replace with new
						closestDist = dist;
						CurrentTarget = dest.GameObject;
					}
				}
			}

		}

	}

}