using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    [SerializeField] private Text highScores;
    // Start is called before the first frame update
    void Start()
    {
        int scores = PlayerPrefs.GetInt("Score");
        highScores.text = "High Score : " + scores;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void exitLevel() {
        Application.Quit();
    }
}
