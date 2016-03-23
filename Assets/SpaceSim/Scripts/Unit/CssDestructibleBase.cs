using UnityEngine;
using System.Collections;

namespace ss.unit {

	public class CssDestructibleBase : MonoBehaviour, IssDestructible {

		[SerializeField]
		protected float m_hpMax = 100;

		[SerializeField]
		protected float m_hp;

		[SerializeField]
		protected GameObject m_destroyEffect;

		public GameObject GameObject {
			get { return gameObject; }
		}

		public void Awake() {
			m_hp = m_hpMax;
		}

		public void Damage(float damage) {
			m_hp -= damage;
			if(m_hp <= 0) {
				Destroy();
			}
		}

		public void Destroy() {
			if(m_destroyEffect != null) {
				GameObject.Instantiate(m_destroyEffect, transform.position, Quaternion.identity);
			}
			GameObject.Destroy(gameObject);
		}

	}

}