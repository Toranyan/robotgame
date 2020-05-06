using UnityEngine;
using System.Collections;

using System.Collections.Generic;

namespace game.ui {


	public class CTargetingUI : MonoBehaviour {

		[SerializeField]
		protected GameObject m_targetSprite;

		/****************************************
		 * Private Fields
		 * **************************************/

		protected List<GameObject> m_listTrackedObject = new List<GameObject>();

		

		/****************************************
		* Public Methods
		* **************************************/

		public void Update() {



		}

		public void AddTrackedObj(GameObject obj) {
			if(!m_listTrackedObject.Contains(obj)) {
				m_listTrackedObject.Add(obj);
			}
		}


		public void ClearTrackedObj() {
			m_listTrackedObject.Clear();
		}


		
	}


}