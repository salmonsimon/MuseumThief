using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Player player;
    //[SerializeField] private FloatingTextManager floatingTextManager;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject InventoriesUI;
    [SerializeField] private StolenManager stolenManager;

    [SerializeField] private Transform stealableContentCarrying;
    [SerializeField] private Transform stealableContentStolen;
    [SerializeField] private GameObject stolenObject;

    private bool gameIsPaused = false;
    private bool gameHasBeenSaved = false;
    [SerializeField] private bool onMainMenu = true;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(pauseMenu.gameObject);
            Destroy(mainMenu.gameObject);
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

        if (onMainMenu)
        {
            pauseMenu.SetActive(false);
            InventoriesUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (!gameIsPaused && !onMainMenu && Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
        else if (gameIsPaused && !onMainMenu && gameIsPaused && Input.GetKeyDown(KeyCode.Escape))
            ResumeGame();
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
        if (gameHasBeenSaved) 
            ZSerializer.ZSerialize.LoadScene();

        if (!onMainMenu)
        {
            stolenManager.LoadStolenManager();
            player.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
            InventoriesUI.SetActive(true);
            mainMenu.SetActive(false);
        }
        else if (onMainMenu)
        {
            InventoriesUI.SetActive(false);
            mainMenu.SetActive(true);
        }

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

    public void PlayGame()
    {
        onMainMenu = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(Config.MAIN_SCENE_NAME);
    }

    public void QuitGame()
    {
        //To quit the built game
        Application.Quit();

        //To quit the editor application
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void DeleteSavedGame()
    {
        string path = Application.persistentDataPath;

        DirectoryInfo di = new DirectoryInfo(path);

        foreach (FileInfo file in di.EnumerateFiles())
        {
            file.Delete();
        }
        foreach (DirectoryInfo dir in di.EnumerateDirectories())
        {
            dir.Delete(true);
        }
    }
    
    public void PauseGame()
    {
        gameIsPaused = true;

        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        gameIsPaused = false;

        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void ToMainMenu()
    {
        gameIsPaused = false;
        onMainMenu = true;

        Time.timeScale = 1f;
        SceneManager.LoadScene(Config.MAIN_MENU_SCENE_NAME);
        pauseMenu.SetActive(false);
    }

    public bool IsGamePaused()
    {
        return gameIsPaused;
    }

    private void ListItems(List<Stealable> stealableList, Transform stealableContent)
    {
        foreach (Transform stealable in stealableContent)
        {
            Destroy(stealable.gameObject);
        }

        foreach (var stealable in stealableList)
        {
            GameObject obj = Instantiate(stolenObject, stealableContent);

            var name = obj.transform.Find("Item Name").GetComponent<Text>();
            var icon = obj.transform.Find("Item Icon").GetComponent<Image>();

            name.text = stealable.stealableName;
            icon.sprite = stealable.icon;
        }
    }

    public void ListItemsCarrying()
    {
        ListItems(stolenManager.carrying, stealableContentCarrying);
    }

    public void ListItemsStolen()
    {
        ListItems(stolenManager.stolen, stealableContentStolen);
    }

    public void SetGameHasBeenSaved (bool x)
    {
        gameHasBeenSaved = x;
    }
}
