using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartManager : MonoBehaviour
{

    [Header("Start")]
    [SerializeField] private GameObject startScreen;

    private void Awake()
    {
        //gameOverScreen.SetActive(false);
        startScreen.SetActive(true);
    }
    private void Update()
    {
    }

    #region Game Over
    //Activate game over screen
    //public void GameOver()
    //{
    //    gameOverScreen.SetActive(true);
    //    SoundManager.instance.PlaySound(gameOverSound);
    //}

    //Restart level
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Main Menu
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Play
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    //Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode (will only be executed in the editor)
#endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        //If status == true pause | if status == false unpause
        startScreen.SetActive(status);

        //When pause status is true change timescale to 0 (time stops)
        //when it's false change it back to 1 (time goes by normally)
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion
}