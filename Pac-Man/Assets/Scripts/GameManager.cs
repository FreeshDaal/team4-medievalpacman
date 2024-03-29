using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI YouWinText;

    public AudioClip WinMusic;
    public AudioClip LoseMusic;


    

    private void Start()
    {
        NewGame();
        GameOverText.enabled = false;
        YouWinText.enabled = false;
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
        GameOverText.enabled = false;
    }

    private void NewRound()
    {

        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

    }

    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
    }

    private void GameOver()
    {
        GetComponent<AudioSource>().clip = LoseMusic;
            GetComponent<AudioSource>().Play();

        GameOverText.enabled = true;
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);

       StartCoroutine(endscreen());
    }
    IEnumerator endscreen()
    {
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("Menu");
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier++;
        Scoring.instance.AddPoint();
    }
    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        Scoring.instance.AddPoint();
        
        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Pacman 2"))
            {
             YouWinText.enabled = true;
             GetComponent<AudioSource>().clip = WinMusic;
            GetComponent<AudioSource>().Play();

                StartCoroutine(endscreen());
    
            IEnumerator endscreen()
            {
                yield return new WaitForSecondsRealtime(5);
                SceneManager.LoadScene("Menu");
            }
            }
            else
            {
                SceneManager.LoadScene("Pacman 2");
            }
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
        
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
}
