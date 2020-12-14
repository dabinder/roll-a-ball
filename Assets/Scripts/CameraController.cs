using UnityEngine;

/// <summary>
/// manages camera location
/// </summary>
public class CameraController : MonoBehaviour {
	public GameObject player;
	private Vector3 offset;

	/// <summary>
	/// start camera a fixed distance behind player
	/// </summary>
	void Start() {
		offset = transform.position - player.transform.position;
	}

	/// <summary>
	/// maintain fixed camera distance
	/// </summary>
	void LateUpdate() {
		transform.position = player.transform.position + offset;
	}
}
