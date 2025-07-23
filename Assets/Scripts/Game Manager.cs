using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public enum GameState
{
    Intro,
    Playing,
    Dead
}
public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameState State = GameState.Intro;

    public float PlayStartTime;

    public int Lives = 2;

    public int FoodScore = 0;

    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject PlayButtonUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject SuperFoodSpawner;
    public GameObject BeerSpawner;

    public Player PlayerScript;

    public TMP_Text scoreText;
    public TMP_Text topScoresText;
    private int finalScore = 0;
    public GameObject itemDesc1;
    public GameObject itemDesc2;
    public GameObject itemDesc3;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        //IntroUI.SetActive(true);
    }

    float CalculateScore()
    {
        return  Time.time - PlayStartTime;
    }

    void SaveHighScore()
    {
        int newScore = Mathf.FloorToInt(CalculateScore()) + FoodScore;

        List<int> scores = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey($"highScore{i}"))
            {
                scores.Add(PlayerPrefs.GetInt($"highScore{i}"));
            }
        }

        scores.Add(newScore);
        scores.Sort((a, b) => b.CompareTo(a)); 

        for (int i = 0; i < Mathf.Min(5, scores.Count); i++)
        {
            PlayerPrefs.SetInt($"highScore{i}", scores[i]);
        }

        PlayerPrefs.Save();
    }

    public float CalculateGameSpeed()
    {
        if (State != GameState.Playing)
        {
            return 5f;
        }
        float speed = 10f + (1f * Mathf.Floor(CalculateScore() / 10f));
        return Mathf.Min(speed, 40f);
    }
    
    string GetTopScoresText()
    {
        string result = "Top 5 Scores:\n";
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey($"highScore{i}"))
            {
                int score = PlayerPrefs.GetInt($"highScore{i}");
                result += $"{i + 1}. {score}\n";
            }
        }
        return result;
    }

        public void StartGame()
    {
        State = GameState.Playing;
        IntroUI.SetActive(false);
        DeadUI.SetActive(false);
        PlayButtonUI.SetActive(false);

        EnemySpawner.SetActive(true);
        FoodSpawner.SetActive(true);
        SuperFoodSpawner.SetActive(true);
        BeerSpawner.SetActive(true);
        PlayStartTime = Time.time;
    }

    

        

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("スコアリセット");
        }

        if (State == GameState.Playing)
        {
            scoreText.text = "Score: " + (Mathf.FloorToInt(CalculateScore()) + FoodScore);
            itemDesc1.SetActive(false);
            itemDesc2.SetActive(false);
            itemDesc3.SetActive(false);

        }
        else if (State == GameState.Dead)
        {
            scoreText.text = "Score: " + finalScore;
            topScoresText.text = GetTopScoresText();
        }
        

        // if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        // {
        //     State = GameState.Playing;
        //     IntroUI.SetActive(false);
        //     EnemySpawner.SetActive(true);
        //     FoodSpawner.SetActive(true);
        //     BeerSpawner.SetActive(true);
        //     PlayStartTime = Time.time;

        // }
        if (State == GameState.Playing && Lives == 0)
        {
            PlayerScript.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            SuperFoodSpawner.SetActive(false);
            BeerSpawner.SetActive(false);
            //DeadUI.SetActive(true);
            //PlayButtonUI.SetActive(true);
            SaveHighScore();
            finalScore = Mathf.FloorToInt(CalculateScore()) + FoodScore;
            State = GameState.Dead;
            
        }

        if (State == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        {
            DeadUI.SetActive(false);
            SceneManager.LoadScene("main");
            //State = GameState.Playing;
        }
    }
}
