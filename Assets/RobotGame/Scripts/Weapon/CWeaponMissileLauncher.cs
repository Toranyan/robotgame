using UnityEngine;
using System.Collections;

using game.projectile;

namespace game.weapon {

	public class CWeaponMissileLauncher : CWeaponBase {


		protected override void SetProjInitialVelo (CProjectileBase proj, Vector3 vecTargetDelta) {

			Vector3 vecSpread = Random.insideUnitSphere;
			Vector3 vecInitVelo = vecSpread;

			if(m_spawnPoint != null) {
				vecInitVelo = vecInitVelo * 0.3f + (m_spawnPoint.rotation * Vector3.forward);
			}

			vecInitVelo = vecInitVelo.normalized * m_muzzleVelocity;

			//also set rotation
			proj.transform.rotation = Quaternion.LookRotation(vecInitVelo);

			proj.Velocity = vecInitVelo;
		}
	}

}