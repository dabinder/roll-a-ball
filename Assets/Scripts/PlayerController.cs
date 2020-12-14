using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// manages player actions
/// </summary>
public class PlayerController : MonoBehaviour {
	public float speed = 0;
	public Color warningColor;
	public int maxLives;

	public UnityEvent OnScoreUpdated, OnLivesUpdated;

	private int _score = 0;
	/// <summary>
	/// indicates player's current score
	/// </summary>
	public int Score {
		get => _score;
		private set {
			_score = value;
			OnScoreUpdated?.Invoke();
		}
	}

	private int _lives;
	/// <summary>
	/// indicates player's number of lives remaining until game over
	/// </summary>
	public int Lives { 
		get => _lives;
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

	/// <summary>
	/// find rigid body for player and set initial variables
	/// </summary>
	private void Start() {
		_rb = GetComponent<Rigidbody>();
		Lives = maxLives;
	}

	/// <summary>
	/// handle movement input
	/// </summary>
	/// <param name="movementValue">up/down/left/right input</param>
	private void OnMove(InputValue movementValue) {
		Vector2 movementVector = movementValue.Get<Vector2>();
		_movementX = movementVector.x;
		_movementY = movementVector.y;
	}

	/// <summary>
	/// translate movement input into force on ball
	/// </summary>
	private void FixedUpdate() {
		_rb.AddForce(new Vector3(_movementX, 0, _movementY) * speed);
	}

	/// <summary>
	/// handle collision with pickups
	/// </summary>
	/// <param name="other">object the player has run into</param>
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag(Tags.PICKUP)) {
			Destroy(other.gameObject);
			Score++;
		}
	}

	/// <summary>
	/// handle player death
	/// </summary>
	public void Kill() {
		Lives--;
	}

	/// <summary>
	/// stop player movement temporarily
	/// </summary>
	public void Stop() {
		_rb.velocity = Vector3.zero;
		_rb.angularVelocity = Vector3.zero;
	}

	/// <summary>
	/// freeze player movement
	/// </summary>
	public void Freeze() {
		_rb.constraints = RigidbodyConstraints.FreezeAll;
	}
}
