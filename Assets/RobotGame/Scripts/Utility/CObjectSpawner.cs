using UnityEngine;
using System.Collections;

namespace game.utility {

	public class CObjectSpawner : MonoBehaviour {

		/****************************************
		 * Inspector Fields
		 * **************************************/

		[SerializeField]
		protected GameObject m_spawnObject;

		[SerializeField]
		protected float m_spawnRate;

		[SerializeField]
		protected Vector3 m_spawnVectorRange;

		[SerializeField]
		protected bool m_enabled;

		/****************************************
		 * Private Fields
		 * **************************************/

		protected float m_timeElapsedLastSpawn;


		/****************************************
		 * Protected Methods
		 * **************************************/

		protected void Update() {

			//tick
			m_timeElapsedLastSpawn += Time.deltaTime;

			if(m_timeElapsedLastSpawn >= 1 / m_spawnRate) {
				Spawn();
			} 
		}

		protected void Spawn() {
			var pos = Vector3.zero;
			pos.x = Random.Range(-m_spawnVectorRange.x, m_spawnVectorRange.x);
			pos.y = Random.Range(-m_spawnVectorRange.y, m_spawnVectorRange.y);
			pos.z = Random.Range(-m_spawnVectorRange.z, m_spawnVectorRange.z);
			pos += transform.position;
			GameObject.Instantiate(m_spawnObject, pos, transform.rotation);
			m_timeElapsedLastSpawn = 0;
		}


	}

}