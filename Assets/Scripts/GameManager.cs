using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public TextMeshProUGUI scoreText, livesText;
	public GameObject winTextObj, loseTextObj, restartTextObj;
	public Transform spawnPoint;

	private int _maxScore;
	private bool _gameActive;
	private PlayerController _player;

	private const int FALL_ZONE = -50;

	// Start is called before the first frame update
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

	// Update is called once per frame
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

	public void UpdateScore() {
		scoreText.text = $"Score: {_player.Score} / {_maxScore}";
		CheckWinCondition();
	}

	public void UpdateLives() {
		livesText.text = $"Lives: {_player.Lives}";
	}

	private void CheckWinCondition() {
		if (_player.Score >= _maxScore) {
			Win();
		}
	}

	private void CheckLoseCondition() {
		if (_player.Lives <= 0) {
			GameOver();
		}
	}

	private void Win() {
		winTextObj.SetActive(true);
		StopGame();
	}

	private void GameOver() {
		loseTextObj.SetActive(true);
		StopGame();
	}

	private void StopGame() {
		_gameActive = false;
		restartTextObj.SetActive(true);
		_player.Freeze();
	}

	private void ReSpawnPlayer() {
		_player.transform.position = spawnPoint.transform.position;
		_player.transform.rotation = spawnPoint.transform.rotation;
		_player.Stop();
	}
}
