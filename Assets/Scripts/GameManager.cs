using TMPro;
using UnityEngine;

/// <summary>
/// runs the game, spawns the player, and handles win/loss conditions
/// </summary>
public class GameManager : MonoBehaviour {
	public TextMeshProUGUI scoreText, livesText;
	public GameObject winTextObj, loseTextObj, restartTextObj;
	public Transform spawnPoint;

	private int _maxScore;
	private bool _gameActive;
	private PlayerController _player;

	private const int FALL_ZONE = -50;

	/// <summary>
	/// set initial values and start up the game
	/// </summary>
	void Start() {
		_maxScore = GameObject.FindGameObjectsWithTag(Tags.PICKUP).Length;
		_player = FindObjectOfType<PlayerController>();
		winTextObj.SetActive(false);
		loseTextObj.SetActive(false);
		restartTextObj.SetActive(false);
		_gameActive = true;
		UpdateScore();
		UpdateLives();
	}

	/// <summary>
	/// check if player has fallen off the edge and respawn/end game as appropriate
	/// </summary>
	void Update() {
		if (_gameActive && _player.transform.position.y < FALL_ZONE) {
			_player.Kill();
			if (_player.Lives > 0) {
				ReSpawnPlayer();
			} else {
				GameOver();
			}
		}
	}

	/// <summary>
	/// update displayed score text
	/// </summary>
	public void UpdateScore() {
		scoreText.text = $"Score: {_player.Score} / {_maxScore}";
		CheckWinCondition();
	}

	/// <summary>
	/// update displayed lives text
	/// </summary>
	public void UpdateLives() {
		livesText.text = $"Lives: {_player.Lives}";
		CheckLoseCondition();
	}

	/// <summary>
	/// check if player has won the game
	/// </summary>
	private void CheckWinCondition() {
		if (_player.Score >= _maxScore) {
			Win();
		}
	}

	/// <summary>
	/// check if player has lost the game
	/// </summary>
	private void CheckLoseCondition() {
		if (_player.Lives <= 0) {
			GameOver();
		}
	}

	/// <summary>
	/// end game with a victory notification
	/// </summary>
	private void Win() {
		winTextObj.SetActive(true);
		StopGame();
	}

	/// <summary>
	/// end game with a game over notification
	/// </summary>
	private void GameOver() {
		loseTextObj.SetActive(true);
		StopGame();
	}

	/// <summary>
	/// stop game activity, prepare for restart
	/// </summary>
	private void StopGame() {
		_gameActive = false;
		restartTextObj.SetActive(true);
		_player.Freeze();
	}

	/// <summary>
	/// respawn player on screen after falling off
	/// </summary>
	private void ReSpawnPlayer() {
		_player.transform.position = spawnPoint.transform.position;
		_player.transform.rotation = spawnPoint.transform.rotation;
		_player.Stop();
	}
}
