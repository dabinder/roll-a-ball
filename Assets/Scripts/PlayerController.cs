using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
	public float speed = 0;
	public Color warningColor;
	public int maxLives;

	public UnityEvent OnScoreUpdated, OnLivesUpdated;

	private int _score = 0;
	public int Score {
		get {
			return _score;
		}
		private set {
			_score = value;
			OnScoreUpdated?.Invoke();
		}
	}

	private int _lives;
	public int Lives { 
		get {
			return _lives;
		}
		private set {
			_lives = value;
			if (value == 1) {
				GetComponent<Renderer>().material.color = warningColor;
			}
			OnLivesUpdated?.Invoke();
		}
	}

	private Rigidbody _rb;
	private float _movementX, _movementY;

	// Start is called before the first frame update
	private void Start() {
		_rb = GetComponent<Rigidbody>();
		Lives = maxLives;
	}

	private void OnMove(InputValue movementValue) {
		Vector2 movementVector = movementValue.Get<Vector2>();
		_movementX = movementVector.x;
		_movementY = movementVector.y;
	}

	private void Update() {
		
	}

	private void FixedUpdate() {
		_rb.AddForce(new Vector3(_movementX, 0, _movementY) * speed);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(Tags.PICKUP)) {
			Destroy(other.gameObject);
			Score++;
		}
	}

	public void Kill() {
		Lives--;
	}

	public void Stop() {
		_rb.velocity = Vector3.zero;
		_rb.angularVelocity = Vector3.zero;
	}

	public void Freeze() {
		_rb.constraints = RigidbodyConstraints.FreezeAll;
	}
}
