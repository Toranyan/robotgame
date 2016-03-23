using UnityEngine;
using System.Collections;


namespace mt.camera {


	public class CmtCloneCamera : MonoBehaviour {

		public Camera targetCamera;

		public float scale;

		
		// Update is called once per frame
		void Update () {


			transform.localRotation = targetCamera.transform.localRotation;
			transform.localPosition = targetCamera.transform.localPosition * scale;

		
		}
	}


}