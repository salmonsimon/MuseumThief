using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Player player;
    //[SerializeField] private FloatingTextManager floatingTextManager;
    [SerializeField] private GameObject InventoriesUI;
    [SerializeField] private StolenManager stolenManager;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(InventoriesUI.gameObject);
            Destroy(stolenManager.gameObject);
            //Destroy(floatingTextManager.gameObject);

            return;
        }

        instance = this;
    }

    private void Start()
    {
        ZSerializer.ZSerialize.LoadScene();
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
        ZSerializer.ZSerialize.LoadScene();
        player.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
    }

    /* Making this function available as public in the GameManager allow us to 
     * call it from wherever we want in our game, without having to program it
     * in each script
     */
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        //floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public Player GetPlayer()
    {
        return player;
    }

    public StolenManager GetStolenManager()
    {
        return stolenManager;
    }
}
