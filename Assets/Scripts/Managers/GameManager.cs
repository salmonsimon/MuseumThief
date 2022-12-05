using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region Parameters and Variables

    #region Main Logic Variables

    private bool gameIsPaused = false;
    private bool gameHasBeenSaved = false;
    private bool firstTimePlaying = true;

    [SerializeField] private bool onMainMenu = true;

    private bool onEmergency = false;

    #endregion

    #region Game Objects

    [SerializeField] private Player player;
    [SerializeField] private StolenManager stolenManager;
    [SerializeField] private FloatingTextManager floatingTextManager;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private PathfinderGraphUpdater pathfinderGraphUpdater;
    [SerializeField] private CinemachineShake cinemachineShake;

    #endregion

    #region Menu's & UI's

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject welcomeUI;

    #endregion

    #region Masterpiece Holding

    [SerializeField] private GameObject masterpieceHoldingPosition;
    [SerializeField] private Masterpiece heldMasterpiece;

    #endregion

    #region Inventories

    // Inventories - UI
    [SerializeField] private GameObject InventoriesUI;

    // Inventories - Carrying
    [SerializeField] private Transform stealableContentCarrying;
    [SerializeField] private GameObject carryingCapacityText;

    // Inventories - Stolen
    [SerializeField] private Transform stealableContentStolen;
    [SerializeField] private GameObject stolenObject;

    // Inventories - Items
    [SerializeField] private Transform inventoryItemContent;

    #endregion

    #region Item Shop

    // Shop - UI
    [SerializeField] private GameObject itemShopUI;

    // Shop - Main Panel
    [SerializeField] private Transform itemShopContent;
    [SerializeField] private GameObject itemButtonObject;

    // Shop - Selected Item Panel
    [SerializeField] private ItemController selectedItem;
    [SerializeField] private GameObject selectedItemPanel;
    [SerializeField] private Transform selectedItemContent;
    [SerializeField] private GameObject itemObject;
    [SerializeField] private GameObject itemDescription;
    [SerializeField] private GameObject itemPrice;
    [SerializeField] private GameObject moneyAmountText;

    // Shop - Purchase Confirmation Panels
    [SerializeField] private GameObject purchaseConfirmationPanel;
    [SerializeField] private GameObject notEnoughFundsPanel;

    #endregion

    #region Sold Masterpieces

    [SerializeField] private Transform SoldMasterpieceCanvas;
    [SerializeField] private GameObject SoldMasterpiecePanel;

    #endregion


    #endregion

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
            Destroy(itemShopUI.gameObject);
            Destroy(welcomeUI.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(soundManager.gameObject);
            Destroy(musicManager.gameObject);
            Destroy(levelLoader.gameObject);
            Destroy(pathfinderGraphUpdater.gameObject);
            Destroy(cinemachineShake.gameObject);

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
        {
            ZSerializer.ZSerialize.LoadScene();
            firstTimePlaying = false;
        }

        if (!onMainMenu)
        {
            stolenManager.LoadStolenManager();
            UpdateCarryingCapacityText();
            player.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
            InventoriesUI.SetActive(true);
            mainMenu.SetActive(false);

            if (firstTimePlaying && !gameHasBeenSaved)
                welcomeUI.SetActive(true);

            GameObject virtualCameraGameObject = GameObject.FindGameObjectWithTag(Config.CINEMACHINE_CAMERA_TAG);

            if (virtualCameraGameObject)
            {
                Cinemachine.CinemachineVirtualCamera virtualCamera = virtualCameraGameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>();
                virtualCamera.Follow = player.transform;
            }
        }
        else if (onMainMenu)
        {
            InventoriesUI.SetActive(false);
            itemShopUI.SetActive(false);
            mainMenu.SetActive(true);
        }

        StartCoroutine(levelLoader.FinishTransition());
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    #region Main Menu

    public void ToMainMenu()
    {
        gameIsPaused = false;
        onMainMenu = true;

        Time.timeScale = 1f;
        levelLoader.LoadLevel(Config.MAIN_MENU_SCENE_NAME, Config.CROSSFADE_TRANSITION);
        pauseMenu.SetActive(false);
    }

    public void PlayGame()
    {
        onMainMenu = false;
        levelLoader.LoadLevel(Config.STUDIO_SCENE_NAME, Config.CROSSFADE_TRANSITION);
    }

    public void QuitGame()
    {
        Application.Quit();
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

    #endregion

    #region Pause Menu

    public void PauseGame()
    {
        GameManager.instance.GetSoundManager().PlaySound(Config.HOVER_SFX);
        GameManager.instance.SetGameIsPaused(true);

        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        GameManager.instance.SetGameIsPaused(false);

        pauseMenu.SetActive(false);
    }

    #endregion

    #region Inventories

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

    #region Inventories - Carrying
    public void ListItemsCarrying()
    {
        ListItems(stolenManager.carrying, stealableContentCarrying);
    }

    public void UpdateCarryingCapacityText()
    {
        int usedCarryingCapacity = stolenManager.GetUsedCarryingCapacity();
        int carryingCapacity = stolenManager.GetCarryingCapacity();


        carryingCapacityText.GetComponent<Text>().text = usedCarryingCapacity.ToString() + " / " + carryingCapacity.ToString();
    }

    #endregion

    #region Inventories - Stolen

    public void ListItemsStolen()
    {
        ListItems(stolenManager.stolen, stealableContentStolen);
    }

    #endregion

    #region Inventories - Items

    public void ListOwnedItems()
    {
        ListShopItems(stolenManager.ownedItems, inventoryItemContent);
    }

    #endregion

    #endregion

    #region Item Shop

    #region Item Shop - Main Panel

    public void ShowItemShop()
    {
        itemShopUI.SetActive(true);
    }

    private void DisplayMoneyAmount()
    {
        moneyAmountText.GetComponent<Text>().text = GetStolenManager().GetMoney().ToString();
    }

    public void ListShopItems()
    {
        ListShopItems(stolenManager.shopItems, itemShopContent);
    }

    private void ListShopItems(List<Item> itemList, Transform itemContent)
    {
        DisplayMoneyAmount();

        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        int ropeAmount = 0;
        GameObject ropeIcon = null;

        foreach (var item in itemList)
        {
            string itemName = item.itemName;
            bool itemIsRope = itemName == "Rope";

            if (itemIsRope)
                ropeAmount++;

            if (!itemIsRope || ropeAmount == 1)
            {
                 GameObject obj = Instantiate(itemButtonObject, itemContent);

                var name = obj.transform.Find("Item Name").GetComponent<Text>();
                var icon = obj.transform.Find("Item Icon").GetComponent<Image>();

                name.text = itemName;
                icon.sprite = item.icon;

                obj.transform.GetComponent<ItemController>().item = item;

                Button thisButton = obj.transform.GetComponent<Button>();
                thisButton.onClick.AddListener(SetSelectedItem);
                thisButton.onClick.AddListener(DisplaySelectedItem);
                thisButton.onClick.AddListener(ConfirmPurchase);

                if (itemIsRope)
                {
                    ropeIcon = obj;
                }
            }
            else if (item.itemName == "Rope" && ropeAmount > 1)
            {
                if (ropeAmount == 2)
                    ropeIcon.transform.Find("Item Quantity").gameObject.SetActive(true);

                var ropeAmountText = ropeIcon.transform.Find("Item Quantity").GetComponent<Text>();
                ropeAmountText.text = ropeAmount.ToString();
            }
        }
    }

    #endregion

    #region Item Shop - Selected Item Panel

    private void SetSelectedItem()
    {
        selectedItem = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<ItemController>();
    }

    public void DeselectSelectedItem()
    {
        selectedItem = null;
    }

    private void ConfirmPurchase()
    {
        selectedItemPanel.SetActive(true);
    }
    private void DisplaySelectedItem()
    {
        GameManager.instance.GetSoundManager().PlaySound(Config.CLICK_SFX);

        foreach (Transform item in selectedItemContent)
        {
            Destroy(item.gameObject);
        }

        GameObject obj = Instantiate(itemObject, selectedItemContent);

        var name = obj.transform.Find("Item Name").GetComponent<Text>();
        var icon = obj.transform.Find("Item Icon").GetComponent<Image>();

        name.text = selectedItem.item.itemName;
        icon.sprite = selectedItem.item.icon;

        var description = itemDescription.GetComponent<Text>();
        description.text = selectedItem.item.itemDescription;

        var priceText = itemPrice.GetComponent<Text>();
        priceText.text = "The price for this item is: $" + selectedItem.item.price;
    }

    public void BuyItem()
    {
        if (GetStolenManager().money >= selectedItem.item.price)
        {
            GetStolenManager().money -= selectedItem.item.price;

            selectedItem.UseItem();
            GetStolenManager().ShopToOwned(selectedItem.item);

            ShowPurchaseConfirmation();

            GetStolenManager().SaveStolenManager();
        }
        else
        {
            ShowNotEnoughFunds();
        }

    }
    public void ShowPurchaseConfirmation()
    {
        GameManager.instance.GetSoundManager().PlaySound(Config.BOUGHT_SFX);
        purchaseConfirmationPanel.SetActive(true);
    }

    public void ShowNotEnoughFunds()
    {
        GameManager.instance.GetSoundManager().PlaySound(Config.DENIED_SFX);
        notEnoughFundsPanel.SetActive(true);
    }

    #endregion

    #endregion

    #region Sold Masterpieces

    public void SoldDialogues(List<Stealable> stealableList)
    {
        GameManager.instance.GetSoundManager().PlaySound(Config.BOUGHT_SFX);

        foreach (Transform panel in SoldMasterpieceCanvas)
        {
            Destroy(panel.gameObject);
        }

        foreach (Stealable stealable in stealableList)
        {
            GameObject newPanel = Instantiate(SoldMasterpiecePanel, SoldMasterpieceCanvas);
            DisplaySoldMasterpiece(stealable, newPanel);
        }
    }

    private void DisplaySoldMasterpiece(Stealable stealable, GameObject soldMasterpieceDialoguePanel)
    {
        Transform soldMasterpieceContent = soldMasterpieceDialoguePanel.transform.Find("Sold Masterpiece").transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Sold Masterpiece Content");

        GameObject obj = Instantiate(itemObject, soldMasterpieceContent);

        var icon = obj.transform.Find("Item Icon").GetComponent<Image>();
        icon.sprite = stealable.icon;

        var name = soldMasterpieceDialoguePanel.transform.Find("Stealable Name").GetComponentInChildren<Text>();
        name.text = stealable.stealableName;

        var description = soldMasterpieceDialoguePanel.transform.Find("Text Panel").GetComponentInChildren<Text>();
        description.text = stealable.soldDialogue;

        var priceText = soldMasterpieceDialoguePanel.transform.Find("Price Panel").GetComponentInChildren<Text>();
        priceText.text = "I'll give you $" + stealable.price + " for this...";
    }

    #endregion

    #region Getters and Setters
    public Player GetPlayer()
    {
        return player;
    }

    public StolenManager GetStolenManager()
    {
        return stolenManager;
    }

    public SoundManager GetSoundManager()
    {
        return soundManager;
    }

    public MusicManager GetMusicManager()
    {
        return musicManager;
    }

    public LevelLoader GetLevelLoader()
    {
        return levelLoader;
    }

    public PathfinderGraphUpdater GetPathfinderGraphUpdater()
    {
        return pathfinderGraphUpdater;
    }

    public CinemachineShake GetCinemachineShake()
    {
        return cinemachineShake;
    }

    public bool IsGamePaused()
    {
        return gameIsPaused;
    }

    public bool IsOnMainMenu()
    {
        return onMainMenu;
    }

    public void SetGameIsPaused(bool x)
    {
        gameIsPaused = x;

        if (x)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void SetGameHasBeenSaved(bool x)
    {
        gameHasBeenSaved = x;
    }

    public Masterpiece GetHeldMasterpiece()
    {
        return heldMasterpiece;
    }

    public void SetHeldMasterpiece(Masterpiece masterpiece)
    {
        heldMasterpiece = masterpiece;
    }

    public GameObject GetMasterpieceHoldingPosition()
    {
        return masterpieceHoldingPosition;
    }

    public void SetFirstTimePlaying(bool value)
    {
        firstTimePlaying = value;    
    }

    public bool GetOnEmergency()
    {
        return onEmergency;
    }

    public void SetOnEmergency(bool value)
    {
        onEmergency = value;
    }

    #endregion
}
