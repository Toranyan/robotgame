using UnityEngine;
using System.Collections;

namespace ss.unit {

	public interface IssDestructible {

		GameObject GameObject {
			get;
		}

		void Destroy();
		void Damage(float damage);


	}

}