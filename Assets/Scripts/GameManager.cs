using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Player player;
    [SerializeField] private FloatingTextManager floatingTextManager;
    [SerializeField] private int money;

    public Player GetPlayer()
    {
        return player;
    }

    public int GetMoney()
    {
        return money;
    }

    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            return;
        }

        instance = this;

        SceneManager.sceneLoaded += LoadState;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
    }

    /* Making this function available as public in the GameManager allow us to 
     * call it from wherever we want in our game, without having to program it
     * in each script
     */
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    /*
     * Things we want to save in our game
     * money
     * masterpieces stolen
     * items owned (FOR LATER)
     */

    public void SaveState()
    {
        string s = "";

        s += money.ToString() + "|";
        //s += masterpiecesStolenIds.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split("|");

        money = int.Parse(data[0]);

        //masterpiecesStolenIds = data[3];

        player.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
    }
}
