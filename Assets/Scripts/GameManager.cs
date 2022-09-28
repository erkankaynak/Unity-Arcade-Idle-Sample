using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    [SerializeField] private GameObject panelGameOver;

    private void Awake()
    {
        Instance = this;
    }
    public void GameOver()
    {
        panelGameOver.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
