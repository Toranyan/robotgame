using UnityEngine;
using System.Collections;

namespace game.utiliy {

	public class CObjectSpawner : MonoBehaviour {

		/****************************************
		 * Inspector Fields
		 * **************************************/

		[SerializeField]
		protected GameObject m_spawnObject;

		[SerializeField]
		protected float m_spawnRate;

		[SerializeField]
		protected Vector3 m_spawnVector;

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
			GameObject.Instantiate(m_spawnObject, transform.position, transform.rotation);
			m_timeElapsedLastSpawn = Time.time;
		}


	}

}