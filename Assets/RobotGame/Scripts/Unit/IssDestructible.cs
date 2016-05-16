using UnityEngine;
using System.Collections;

namespace game.unit {

	public interface IssDestructible {

		GameObject GameObject {
			get;
		}

		void Destroy();
		void Damage(float damage);


	}

}