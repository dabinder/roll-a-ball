using UnityEngine;

/// <summary>
/// manages camera location
/// </summary>
public class CameraController : MonoBehaviour {
	[SerializeField] private GameObject player;

	private Vector3 _offset;

	/// <summary>
	/// start camera a fixed distance behind player
	/// </summary>
	void Start() {
		_offset = transform.position - player.transform.position;
	}

	/// <summary>
	/// maintain fixed camera distance
	/// </summary>
	void LateUpdate() {
		transform.position = player.transform.position + _offset;
	}
}
