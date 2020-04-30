using UnityEngine;
using System.Collections;

using game.unit;

namespace game.controllers {

	/// <summary>
	/// C aim controller.
	/// adjusts a units aim using a camera
	/// </summary>
	public class CAimController : MonoBehaviour {

		[SerializeField]
		protected Camera m_camera;

		[SerializeField]
		protected CUnitBase m_unit;

		[SerializeField]
		protected Vector3 m_vectorAim;

		[SerializeField]
		protected LayerMask m_layerMask;

		protected void Update() {

			if(m_camera == null) {
				return;
			}
			if(m_unit == null) {
				return;
			}

			//raytrace
			Ray ray = new Ray(m_camera.transform.position, m_camera.transform.rotation * Vector3.forward);
			RaycastHit hit;
			//int layerMask = LayerMask.GetMask(m_layerNames);
			if(Physics.Raycast(ray, out hit, 2000, m_layerMask)) {
				Vector3 hitPoint = hit.point;

				m_unit.SetTargetVec(hitPoint);

				m_vectorAim = hitPoint;
			}

		}

	}

}