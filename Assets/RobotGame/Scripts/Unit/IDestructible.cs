using UnityEngine;
using System.Collections;

namespace game.unit {

	public interface IDestructible {

		GameObject GameObject {
			get;
		}

		void Destroy();
		void Damage(float damage);


	}

}