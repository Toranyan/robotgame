using UnityEngine;
using System.Collections;

public class CssAmbientSpin : MonoBehaviour {

	[SerializeField]
	protected Vector3 angularVelocity = new Vector3(0, 10, 0)	;



	protected void FixedUpdate() {

		transform.Rotate(angularVelocity * Time.fixedDeltaTime);

	}


}
