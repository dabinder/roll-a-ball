using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// runs the game, spawns the player, and handles win/loss conditions
/// </summary>
public class GameManager : MonoBehaviour {
	private const int FALL_ZONE = -50;

	public TextMeshProUGUI scoreText, livesText;
	public GameObject pausePanel;
	public GameObject winTextObj, loseTextObj, restartTextObj, pauseTextObj;
	public Transform spawnPoint;

	private int _maxScore;
	private bool _gameActive;
	private PlayerController _player;
	private bool _isPaused;

	/// <summary>
	/// set initial values and start up the game
	/// </summary>
	void Start() {
		_maxScore = GameObject.FindGameObjectsWithTag(Tags.PICKUP).Length;
		_player = FindObjectOfType<PlayerController>();
		_gameActive = true;
		UpdateScore();
		UpdateLives();
		Time.timeScale = 1;
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
		pausePanel.SetActive(true);
		winTextObj.SetActive(true);
		StopGame();
	}

	/// <summary>
	/// end game with a game over notification
	/// </summary>
	private void GameOver() {
		pausePanel.SetActive(true);
		loseTextObj.SetActive(true);
		StopGame();
	}

	/// <summary>
	/// stop game activity, prepare for restart
	/// </summary>
	private void StopGame() {
		_gameActive = false;
		_isPaused = true;
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

	/// <summary>
	/// pause or unpause game and bring up reset option
	/// </summary>
	/// <param name="paused">pause/unpause status</param>
	private void SetPaused(bool paused) {
		_isPaused = paused;
		Time.timeScale = paused ? 0 : 1;
		pausePanel.SetActive(paused);
		pauseTextObj.SetActive(paused);
	}

	/// <summary>
	/// handle pause button input
	/// </summary>
	private void OnPause() {
		if (!_gameActive) return;
		SetPaused(!_isPaused);
	}

	/// <summary>
	/// confirm reset after end of game or from pause menu
	/// </summary>
	private void OnConfirm() {
		if (_isPaused) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
