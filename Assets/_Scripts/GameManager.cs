﻿using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private int score = 0; 

    private int currentBrickCount;
    private int totalBrickCount;
    public GameObject explosionParticle;

    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }

    private void FireBall()
    {
        ball.FireBall();
    }

    public void OnBrickDestroyed(Vector3 position)
    {
        // Trigger camera shake when a brick is destroyed
        CameraShake.Shake(0.2f, 1f);  // Adjust duration and strength as needed

        // fire audio here
        // implement particle effect here
        Instantiate(explosionParticle, position, Quaternion.identity);
        currentBrickCount--;
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");

        score++;
        scoreCounter.UpdateScore(score);

        if (currentBrickCount == 0)
            SceneHandler.Instance.LoadNextScene();
    }

    public void KillBall()
    {
        maxLives--;
        // Trigger camera shake when the ball is killed
        CameraShake.Shake(0.3f, 1.5f);   // Adjust duration and strength as needed

        // update lives on HUD here
        // game over UI if maxLives < 0, then exit to main menu after delay
        ball.ResetBall();
    }
}
