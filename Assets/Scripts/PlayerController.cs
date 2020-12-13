using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
	public float speed = 0;
	public TextMeshProUGUI scoreText;
	public GameObject winTextObj;

	private Rigidbody rb;
	private float movementX, movementY;
	private int score;
	private int maxScore;

	// Start is called before the first frame update
	private void Start() {
		rb = GetComponent<Rigidbody>();
		score = 0;
		maxScore = GameObject.FindGameObjectsWithTag(Tags.PICKUP).Length;
		SetCountText();
		winTextObj.SetActive(false);
	}

	private void OnMove(InputValue movementValue) {
		Vector2 movementVector = movementValue.Get<Vector2>();
		movementX = movementVector.x;
		movementY = movementVector.y;
	}

	private void SetCountText() {
		scoreText.text = $"Score: {score}";
		if (score >= maxScore) {
			winTextObj.SetActive(true);
		}
	}

	private void FixedUpdate() {
		rb.AddForce(new Vector3(movementX, 0, movementY) * speed);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(Tags.PICKUP)) {
			other.gameObject.SetActive(false);
			score++;
			SetCountText();
		}
	}
}
