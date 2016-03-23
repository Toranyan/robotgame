using UnityEngine;
using System.Collections;

namespace ss.unit {

	public class CssAutoDestroy : MonoBehaviour {

		[SerializeField]
		protected float m_lifeTime = 5;

		[SerializeField]
		protected float m_timeElapsed;


		public void Awake() {

		}

		public void Update() {
			m_timeElapsed += Time.deltaTime;
			if(m_timeElapsed >= m_lifeTime) {
				GameObject.Destroy(gameObject);
			}
		}

	}

}