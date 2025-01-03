using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class FinishPanel : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text infoText;

    private void Start()
    {
        GameManager.onGameStateChanges += GameManager_onGameStateChanges;
    }

    private void GameManager_onGameStateChanges(GameStates obj)
    {
        if(obj == GameStates.Finished)
        {
            panel.SetActive(true);
            OnGameFinished();
        }
    }

    void OnGameFinished()
    {
        if(player.isDead)
        {
            infoText.text = "You lost";
            infoText.color = Color.red;
        }
        else
        {
            infoText.text = "You Won";
            infoText.color = Color.green;
        }
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
