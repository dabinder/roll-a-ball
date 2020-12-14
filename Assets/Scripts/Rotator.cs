using UnityEngine;

/// <summary>
/// handles rotation for pickup items
/// </summary>
public class Rotator : MonoBehaviour {
	/// <summary>
	/// continuously rotate object
	/// </summary>
	void Update() {
		transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}
}
