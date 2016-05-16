using UnityEngine;
using System.Collections;

using game.projectile;

namespace game.weapon {

	public class CWeaponMissileLauncher : CWeaponBase {


		protected override Vector3 CalcProjInitialVelo (Vector3 vecTargetDelta) {

			Vector3 vecSpread = Random.insideUnitSphere;
			Vector3 vecInitVelo = vecSpread;

			if(m_spawnPoint != null) {
				vecInitVelo += m_spawnPoint.rotation * Vector3.forward;
			}

			vecInitVelo *= m_muzzleVelocity;
			//proj.Velocity = (vecTarget + vecSpread) * m_muzzleVelocity;

			return vecInitVelo;
		}
	}

}