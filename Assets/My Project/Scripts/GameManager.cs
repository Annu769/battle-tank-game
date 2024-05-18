
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BattleTank;
using TMPro;

public class GameManager :GenericSingleTon<GameManager>
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pausePnael;
    [SerializeField] private GameObject playerUIObject;
    [SerializeField] private TextMeshProUGUI waveText;
    public bool isGameOver = false;
    private bool GameIsPaused;
    private int currentWave = 0;
    private int maxWaves = 3;

    private void Start()
    {
      
        playerUIObject.SetActive(true);
    }
   /* private IEnumerator StartWaves()
    {
        while (currentWave < maxWaves && !isGameOver)
        {
            yield return StartCoroutine(StartWave(currentWave));
            currentWave++;
            yield return new WaitForSeconds(5f); // Wait for a few seconds between waves
        }

        // If all waves are completed and the game is not over, player wins
        if (!isGameOver)
        {
            // Implement player win logic here
            Debug.Log("You win!");
        }
    }*/
   /* private IEnumerator StartWave(int waveNumber)
    {
        // Spawn enemies for the current wave
        // You can implement your enemy spawning logic here
        // For example, you can use TankService to spawn enemies
        // You can adjust the number and type of enemies spawned for each wave

        // For demonstration, let's assume we spawn a fixed number of enemies for each wave
        waveText.text = "WAVE " + waveNumber + "/" + maxWaves;
        int enemyCount = 3 + (waveNumber * 2); // Increase enemy count for each wave
        EnemyService enemyService = EnemyService.Instance;

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = enemyService.GetRandomSpawnPoint(); // Implement your spawn position logic
            enemyService.CreateEnemyTank(Random.Range(0, enemyService.enemyTankList.enemies.Length)).EnableEnemyTank(spawnPosition);
            yield return new WaitForSeconds(1f); // Delay between enemy spawns
        }

        // Wait until all enemies are defeated
        while (EnemyService.Instance.enemies.Count > 0)
        {
            yield return null;
        }
    }*/
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                GameIsPause();
            }
        }
    }
    public void GameOver()
    {
        if(isGameOver)
        {
           
            playerUIObject.SetActive(false);
            gameOver.SetActive(true);
            
        }
    }
    public void GameIsPause()
    {
        playerUIObject.SetActive(false);
        pausePnael.SetActive(true);
         Time.timeScale = 0f;
    }
  public void ResumeGame()
    {
        playerUIObject.SetActive(true);
        pausePnael.SetActive(false);
        Time.timeScale = 1f;
    }
  public void GameRestart()
    {
        playerUIObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  public void MainMenuPanel()
    {
        SceneManager.LoadScene(0);
    }
}
