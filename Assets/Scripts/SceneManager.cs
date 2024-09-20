using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    public Player Player;
    public List<Enemie> Enemies;
    public GameObject Lose;
    public GameObject Win;

    [SerializeField] private Text waves;

    private int currWave = 0;
    [SerializeField] private LevelConfig Config;

    public bool cooldownNo=true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnWave();
    }

    private void Update() {
        if (cooldownNo)
        {
            if (Input.GetKey(KeyCode.G))
            {
                //cooldownNo = false;
                //StartCoroutine(waitForCoo());
            }
        }
    }

    public void cool() {
        if (cooldownNo) {
            //cooldownNo = false;
            //StartCoroutine(waitForCoo());
        }
    }

    IEnumerator waitForCoo() {
        yield return new WaitForSeconds(2f);
        //cooldownNo = true;
    }

    public void AddEnemie(Enemie enemie)
    {
        Enemies.Add(enemie);
    }

    public void RemoveEnemie(Enemie enemie)
    {
        Enemies.Remove(enemie);
        if(Enemies.Count == 0)
        {
            SpawnWave();
        }
    }

    public void GameOver()
    {
        Lose.SetActive(true);
    }

    private void SpawnWave()
    {
        if (currWave >= Config.Waves.Length)
        {
            Win.SetActive(true);
            int highScore = PlayerPrefs.GetInt("Score");
            if (highScore < currWave) {
                PlayerPrefs.SetInt("Score", currWave);
            }
            return;
        }

        waves.text = currWave + 1 + "/" + Config.Waves.Length;
        int highScoreB = PlayerPrefs.GetInt("Score");
        if (highScoreB < currWave) {
            PlayerPrefs.SetInt("Score", currWave);
        }

        var wave = Config.Waves[currWave];
        foreach (var character in wave.Characters)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(character, pos, Quaternion.identity);
        }
        currWave++;

    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    

}
