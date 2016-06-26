using UnityEngine;
using System.Collections;

namespace game.unit {

	public class CDestructibleBase : MonoBehaviour, IDestructible {

		/****************************************
		 * Inspector Fields
		 * **************************************/

		[SerializeField]
		protected float m_hpMax = 100;

		[SerializeField]
		protected float m_hp;

		[SerializeField]
		protected GameObject m_destroyEffect;

		[SerializeField]
		protected bool m_invulnerable = false; //cant take damage

		/****************************************
		 * Properties Fields
		 * **************************************/

		public GameObject GameObject {
			get { return gameObject; }
		}

		public float Hp {
			get { return m_hp; }
		}

		public bool Invulnerable {
			get { return m_invulnerable; } 
			set { m_invulnerable = value; }
		}

		/****************************************
		 * Inspector Fields
		 * **************************************/

		public void Awake() {
			m_hp = m_hpMax;
		}

		public void Damage(float damage) {
			if(Invulnerable) {
				return;
			}

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