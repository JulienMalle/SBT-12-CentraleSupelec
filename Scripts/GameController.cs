using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    public GameObject[] collectables;
    public GUIText missedCountText;
    public GUIText scoreText;
    public Vector3 spawnValues;
    public int collectableCount;
    public float spawnWait;  // time between the spawn of two object while a wave
    public float startWait;  // time till the spawning starts at the beginning of the game
    public float waitWait;   // time between two waves

    private int score;
    private int missedCount;

    void Start()
    {
        score = 0;
        missedCount = 0;
        UpdateScore();
        UpdateMissedCount();
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for(int i =0; i < collectableCount; i++)
            {
                GameObject collectable = collectables[Random.Range(0, collectables.Length)];
                Vector3 spawnPosition = SpawnCos();  // A SpawnHDC() function needs to be made to spawn collectables accordingly
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(collectable, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waitWait);
        }

    }

    Vector3 SpawnRandomly()   // Returns a Vector3 used to spawn collectables at a random x position at the top of the screen
    {
        return new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
    }

    Vector3 SpawnCos()   // Returns a Vector3 used to spawn collectables with position x(t) = a * cos(t)
    {
        return new Vector3(spawnValues.x * Mathf.Cos(Time.time), spawnValues.y, spawnValues.z);
    }

    Vector3 SpawnHDC(float spawnHDCXValue)
    {
        //We may compute the new value for the spawning x position in Update() with a function (that yet is to be build)
        return new Vector3(spawnHDCXValue, spawnValues.y, spawnValues.z);
    }
    
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void AddMissed ()
    {
        missedCount += 1;
        UpdateMissedCount();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateMissedCount()
    {
        missedCountText.text = "Missed: " + missedCount;
    }
}
