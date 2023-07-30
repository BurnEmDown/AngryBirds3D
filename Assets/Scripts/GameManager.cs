using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private const int MAX_LIFE = 3;
    
    public static GameManager Instance;

    private int currentLife;
    private int currentEnemyCount;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene newScene, LoadSceneMode loadMode)
    {
        if (newScene.buildIndex == 0) return;

        SetEnemySpawnPointsAndRandomRotate();
        ResetLife();
    }

    private void SetEnemySpawnPointsAndRandomRotate()
    {
        List<Target> targetList = FindObjectsByType<Target>(FindObjectsSortMode.None).ToList();
        List<GameObject> enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemyMovePoints").ToList();

        int index = targetList.Count - 1;
        
        foreach (Target target in targetList)
        {
            if (index < 0) return; // this should be an error
            
            float randomRotation = Random.Range(0, 360f);
            target.transform.localRotation = Quaternion.Euler(0,randomRotation,0);
            
            Transform spawnPoint = enemySpawnPoints[index].transform;
            enemySpawnPoints.RemoveAt(index);

            target.transform.position = spawnPoint.position;
            target.GetComponent<MoveObject>().SetMoveIndex(index);
            index--;
        }

        // also update current enemy count
        currentEnemyCount = targetList.Count;
    }

    private void ResetLife()
    {
        currentLife = MAX_LIFE;
    }

    public void OnEnemyHit()
    {
        currentEnemyCount--;
        if (currentEnemyCount == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void OnShotMiss()
    {
        currentLife--;
        if (currentLife < 0)
        {
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            FindObjectOfType<UIManager>().SetEmptyHeartAtIndex(currentLife);    
        }
        
    }
}
