using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Wilberforce; //namespace reference when using plugin

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MAIN_MENU = 0,
        LOADING,
        PLAYING,
        WIN = GameManager.WIN,
        GAME_OVER,
        WORLD_COMPLETE
    }

    private const int levelCount = WIN - 1;

    private GameState state; //for now so can change in editor
    private Stack<GameState> stateStack = new Stack<GameState>();
    private SaveFile saveFile;
    private string dataPath;
    public static GameManager instance = null;
    private int levelIndex;
    public Colorblind cbeFilter; //reference colorblind plugin
    public GameObject cameraFilter;

    /* For loading screen
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    */
    //scene numbers
    private const int MAIN_MENU = 0;
    private const int LEVEL_ONE = 1;
    private const int WIN = 43;
    private const int GAME_OVER = WIN + 1;
    private const int WORLD_COMPLETE = WIN + 2;

    private bool classicMode = false;
    private Player player;
    private static int counter = -1;
    private int deaths = 0;
    private int lives = 0;
    private const int STARTING_LIVES = 50;

    bool condition = true;
    private void Awake()
    {
        //Unity tut, Data persistence
        if (instance == null) instance = this; 
        else if (instance != this) Destroy(gameObject);

        dataPath = Path.Combine(Application.persistentDataPath, "savedata.dat");

        DontDestroyOnLoad(gameObject);
        

    }

    void Start()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(dataPath);
        Debug.Log("level index: "+levelIndex);
        saveFile = GameSaver.LoadData(dataPath);

        PushGameState(GameState.MAIN_MENU);
        OnStateEntered();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetAxisRaw("Warp") != 0 && stateStack.Peek() != GameState.LOADING)
        {
            int warpIndex = levelIndex + (int)Input.GetAxisRaw("Warp");
            if (warpIndex < 0)
            {
                LoadLevel(WIN - 1);
                PopGameState();
                PushGameState(GameState.PLAYING);
                PushGameState(GameState.LOADING);
                return;
            }
            else if (warpIndex > SceneManager.sceneCountInBuildSettings - 1)
            {

            }
            else
            {
                LoadLevel(warpIndex);
                if (warpIndex == MAIN_MENU || warpIndex == WIN || warpIndex == GAME_OVER)
                {
                    PopGameState();
                    PushGameState((GameState)warpIndex);
                    PushGameState(GameState.LOADING);
                }
                else if (stateStack.Peek() == GameState.PLAYING)
                {
                    PushGameState(GameState.LOADING);
                }
                else
                {
                    PopGameState();
                    PushGameState(GameState.PLAYING);
                    PushGameState(GameState.LOADING);
                }
            }
        }
#endif
        switch (stateStack.Peek())
        {
            case GameState.MAIN_MENU:
            if(!condition)
                {
                    Filter();
                    condition = true;
                }
                break;
            

            case GameState.LOADING:
                if (SceneManager.GetSceneByBuildIndex(levelIndex).isLoaded)
                {

                    PopGameState();
                    OnStateEntered(); 
                    Debug.Log("chosen filter: "+ counter); //colorblind filter checker
                    Debug.Log("Loading gamestate deaths: "+ EntityManager.instance.deathCount.text);

                    
                    
                    
                }
                break;

            case GameState.PLAYING:
                
                if (EntityManager.instance.levelComplete)
                {
                    if (classicMode)
                    {
                         
                        saveFile.classicLevelIndicesCompleted.Add(levelIndex);
                        string levels = "";
                        foreach (int i in saveFile.classicLevelIndicesCompleted)
                        {
                            levels += (i.ToString() + ", ");
                        }
                        Debug.Log(levels);
                    }
                    else
                    {
                        
                        saveFile.standardLevelIndicesCompleted.Add(levelIndex);
                        string levels = "";
                        foreach (int i in saveFile.standardLevelIndicesCompleted)
                        {
                            levels += (i.ToString() + ", ");
                        }
                        Debug.Log("normal mode level: " + levels);
                        
                    }

                    // if (levelIndex == 10 || levelIndex == 20)
                    // {
                    //    Debug.Log(levelIndex);
                    //    SceneManager.LoadScene(WORLD_COMPLETE);
                    //    Debug.Log("scene loaded");
                    // }
                    
                    LoadLevel(levelIndex + 1); //change this to update level index itself, make more useful function to change level...
                    if (levelIndex == WIN)
                    {
                        PopGameState();
                        PushGameState(GameState.WIN);
                        PushGameState(GameState.LOADING);
                    }
                    else PushGameState(GameState.LOADING);

                    return;
                }
                break;

            case GameState.GAME_OVER:
                break;

            case GameState.WIN:
                break;
        }
    }

    private void HandlePlayerDeathGM()
    {
        if (!classicMode)
        {
            deaths++;
            EntityManager.instance.UpdateDeathCount(deaths);
            saveFile.deathCount = deaths;
            GameSaver.SaveData(saveFile, dataPath);
        }
        else
        {
            lives--;
            EntityManager.instance.UpdateDeathCount(lives);
            saveFile.lifeCount = lives;
            GameSaver.SaveData(saveFile, dataPath);

            if (lives <= 0)
            {
                player.gameObject.SetActive(false);

                LoadLevel(GAME_OVER);
                PopGameState();
                PushGameState(GameState.GAME_OVER);
                PushGameState(GameState.LOADING);
            }
        }

    }

    private void OnStateEntered() //similar to entry requirement for class game states- future...
    {
        switch (stateStack.Peek())
        {
            case GameState.MAIN_MENU:
                AudioPlayer.instance.PlayMusic(Menu.instance.menuMusic);
                ContinueLevelText();
                CompletionText();
                Filter();
                break;

            case GameState.PLAYING:
                AudioPlayer.instance.NoMusic();
                if (classicMode)
                {
                    lives = saveFile.lifeCount;
                    EntityManager.instance.UpdateDeathCount(lives);
                }
                else
                {
                    EntityManager.instance.UpdateDeathCount(deaths);
                }
                player = FindObjectOfType<Player>();
                player.onPlayerDeath += HandlePlayerDeathGM;
                //code to fix player dropping input on load scene here

                saveFile.currentLevel = levelIndex;
                saveFile.wasInGame = true;
                GameSaver.SaveData(saveFile, dataPath);
                
                break;

            case GameState.GAME_OVER:
                AudioPlayer.instance.PlayMusic(Menu.instance.menuMusic);

                classicMode = false;

                saveFile.wasInGame = false;
                saveFile.currentLevel = 0;
                saveFile.inClassicMode = false;
                GameSaver.SaveData(saveFile, dataPath);
               
                break;

            case GameState.WIN:
                // AudioPlayer.instance.PlayMusic(Menu.instance.menuMusic); //eventually move this to menu or remove menu entirely... seems annoying to put here
                Text deathCount = GameObject.Find("deathcount").GetComponent<Text>();
                Text secret = GameObject.Find("secret").GetComponent<Text>();
                if (classicMode)
                {
                    deathCount.text = (50 - lives).ToString();
                    secret.text = "that's all there is to it!";
                }
                else
                {
                    deathCount.text = deaths.ToString();
                    if (deaths < 5) secret.text = "(try classic mode!)";
                }

                classicMode = false;

                saveFile.wasInGame = false;
                saveFile.currentLevel = 0;
                saveFile.inClassicMode = false;
                GameSaver.SaveData(saveFile, dataPath);

                break;
        }
    }

    public void ChangeGameState(GameState state)
    {
        this.state = state;
        OnStateEntered();
    }

    public void PushGameState(GameState state)
    {
        stateStack.Push(state);
        
    }

    public GameState PopGameState()
    {
        return stateStack.Pop();
    }

    public void NewGame()
    {
        
        if (classicMode)
        {
            saveFile.inClassicMode = true;
            saveFile.lifeCount = STARTING_LIVES;
        }
        else saveFile.inClassicMode = false;

        saveFile.wasInGame = true;

        LoadLevel(LEVEL_ONE);
        PushGameState(GameState.PLAYING);
        PushGameState(GameState.LOADING);
    }

    //Persistent Save file -z
    public void ContinueGame()
    {
        if (saveFile.wasInGame)
        {
            if (saveFile.inClassicMode)
            {
                classicMode = true;
            }
            else
            {
                classicMode = false;
                deaths = saveFile.deathCount;
            }
            //saveFile = GameSaver.LoadData(dataPath);
            LoadLevel(saveFile.currentLevel);
            PushGameState(GameState.PLAYING);
            PushGameState(GameState.LOADING);
        }
        else NewGame();
    }
#region Backbtn functions
    // function for BackBtn Save Game
    public void SaveMenuGame()
    {
        
        if (saveFile.wasInGame == true)
        {
            if (saveFile.inClassicMode)
            {
                classicMode = true;
                counter = counter -1;
            }
            else
            {
                classicMode = false;
                deaths = saveFile.deathCount;
                counter = counter -1;
            }
            GameSaver.SaveData(saveFile, dataPath);
            LoadLevel(MAIN_MENU);
            PushGameState(GameState.MAIN_MENU);
            PushGameState(GameState.LOADING);
        }
        else Debug.Log("Save File not Found");
    }

    //function for BackBtn Menu
    public void LoadMainMenu()
    {
         deaths = 0;
         lives = 0;
        LoadLevel(MAIN_MENU);
        PopGameState();
        PushGameState(GameState.MAIN_MENU);
        PushGameState(GameState.LOADING);  
    }
#endregion
    public void QuitGame()
    {

#if UNITY_EDITOR
        GameSaver.SaveData(saveFile, dataPath);
        UnityEditor.EditorApplication.isPlaying = false;
#else
        GameSaver.SaveData(saveFile, dataPath);
        Application.Quit();
#endif

    }

    private void CompletionText() //move to ui manager, rework menu system somehow
    {
        Text completionText = GameObject.Find("completion").GetComponent<Text>();

        double percentComplete = (saveFile.standardLevelIndicesCompleted.Count +
            saveFile.classicLevelIndicesCompleted.Count) / (levelCount * 2.0) * 100.0;
        int roundedPercent = Mathf.RoundToInt((float)percentComplete);
        completionText.text = roundedPercent.ToString() + "%";
    }

    private void ContinueLevelText() //move to UI manager for menu, run in mainmenu state
    {
        Text savedLevelText = GameObject.Find("savedlevel").GetComponent<Text>();

        if (saveFile.currentLevel == 0)
        {
            savedLevelText.text = "";
        }
        else
        {
            savedLevelText.text = saveFile.currentLevel.ToString();
            if (saveFile.inClassicMode)
            {
                savedLevelText.text += "!";
            }
        }
    }

    public void SaveFilterSelected() //save Selected Filter
    {
        Text savedSelectedFilter = GameObject.Find("filter").GetComponent<Text>();

        if (counter == 0)
        {
            savedSelectedFilter.text = "Normal Vision";
        }
        if (counter == 1)
        {
            savedSelectedFilter.text = "Protanopia";
        }
        if (counter == 2)
        {
            savedSelectedFilter.text = "Deuteranopia";
        }
        if (counter == 3)
        {
            savedSelectedFilter.text = "Tritanopia";
        }
    }

    public void ToggleClassicMode()
    {
        classicMode = !classicMode;
        GameObject.Find("classicyn").GetComponent<Text>().text = classicMode ? "yes" : "no";
        lives = 1; //this needs to get fixed
    }
    
    public void Filter()
    {
        // Validation Checker for BackBtn (probably redundant but will try and refactor this code segment)
        // if false then increment else set back filterChecker to false to reset its value
        if(!BackBtn.filterChecker)
        {
            counter++;
            
        }
        else
        {
            BackBtn.filterChecker = false;    
        }
        
        {
        if (counter == 1) {
            GameObject.Find("filter").GetComponent<Text>().text = "Protanopia";
            cbeFilter.Type = 1;
        }
        else  if (counter == 2) {
            GameObject.Find("filter").GetComponent<Text>().text = "Deuteranopia";
            cbeFilter.Type = 2;
        }
        else if (counter == 3) {
            GameObject.Find("filter").GetComponent<Text>().text = "Tritanopia";
            cbeFilter.Type = 3;
        } 
        else {
            GameObject.Find("filter").GetComponent<Text>().text = "Normal Vision";
            counter = 0;
            cbeFilter.Type = 0;
        }
        Debug.Log("filter counter: "+counter);
        Debug.Log("Filter: " + cbeFilter.Type);
        }
    }

    public void ResetSave()
    {
        // deaths = 0;
        // lives = 0;
        saveFile = new SaveFile();
        ContinueLevelText();
        CompletionText();
        cbeFilter.Type = 0;
        counter = 0;
        GameObject.Find("filter").GetComponent<Text>().text = "Normal Vision";
        
    }

    public void RestartGame()
    {
        deaths = 0;
        lives = 0;
        LoadLevel(MAIN_MENU);
        PopGameState();
        PushGameState(GameState.MAIN_MENU);
        PushGameState(GameState.LOADING);
    }

    //warning: this changes the internal level index to keep track of things
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Single);
        this.levelIndex = levelIndex;
        // StartCoroutine(LoadAsynchronously(levelIndex));
    }

    /*IEnumerator LoadAsynchronously (int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Single);
        this.levelIndex = levelIndex;

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            // Debug.Log(progress);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }*/
    public bool ClassicMode()
    {
        return classicMode;
    }
    
}