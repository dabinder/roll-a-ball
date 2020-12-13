using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
	public float speed = 0;
	public TextMeshProUGUI scoreText, livesText;
	public GameObject winTextObj, loseTextObj;
	public Transform spawnPoint;
	public int maxLives;
	public Color warningColor;

	private const int FALL_ZONE = -50;

	private Rigidbody _rb;
	private float _movementX, _movementY;
	private int _score;
	private int _maxScore;
	private int _lives;

	// Start is called before the first frame update
	private void Start() {
		_rb = GetComponent<Rigidbody>();
		_score = 0;
		_maxScore = GameObject.FindGameObjectsWithTag(Tags.PICKUP).Length;
		UpdateScore();
		_lives = maxLives;
		UpdateLives();
		winTextObj.SetActive(false);
		loseTextObj.SetActive(false);
		ReSpawn();
	}

	private void OnMove(InputValue movementValue) {
		Vector2 movementVector = movementValue.Get<Vector2>();
		_movementX = movementVector.x;
		_movementY = movementVector.y;
	}

	private void UpdateScore() {
		scoreText.text = $"Score: {_score} / {_maxScore}";
		if (_score >= _maxScore) {
			winTextObj.SetActive(true);
		}
	}

	private void UpdateLives() {
		livesText.text = $"Lives: {_lives}";
	}

	private void Update() {
		if (transform.position.y < FALL_ZONE && HasLives()) {
			Die();
		}
	}

	private void FixedUpdate() {
		_rb.AddForce(new Vector3(_movementX, 0, _movementY) * speed);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(Tags.PICKUP)) {
			other.gameObject.SetActive(false);
			_score++;
			UpdateScore();
		}
	}

	private void Die() {
		_lives--;
		UpdateLives();
		if (HasLives()) {
			ReSpawn();
		} else {
			GameOver();
		}
	}

	private bool HasLives() {
		return _lives > 0;
	}

	private void GameOver() {
		loseTextObj.SetActive(true);
		_rb.velocity = Vector3.zero;
		_rb.angularVelocity = Vector3.zero;
	}

	private void ReSpawn() {
		transform.position = spawnPoint.transform.position;
		transform.rotation = spawnPoint.transform.rotation;
		_rb.velocity = Vector3.zero;
		_rb.angularVelocity = Vector3.zero;
		if (_lives == 1) {
			GetComponent<Renderer>().material.color = warningColor;
		}
	}
}
