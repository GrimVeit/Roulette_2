using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    private static GameEntryPoint instance;
    private UIRootView rootView;
    private Coroutines coroutines;
    public GameEntryPoint()
    {
        coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIRootView>("UIRootView");
        rootView = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(rootView.gameObject);

    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Autorun()
    {
        GlobalGameSettings();

        instance = new GameEntryPoint();
        instance.Run();

    }

    private static void GlobalGameSettings()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Run()
    {
        coroutines.StartCoroutine(LoadAndStartMainMenu());
    }

    private IEnumerator LoadAndStartMainMenu()
    {
        rootView.SetLoadScreen(0);

        yield return rootView.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MAIN_MENU);

        var sceneEntryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToRoulette_Mini += () => coroutines.StartCoroutine(LoadAndStartGameScene_1_Mini());
        sceneEntryPoint.OnGoToRoulette_Euro += () => coroutines.StartCoroutine(LoadAndStartGameScene_2_Euro());
        sceneEntryPoint.OnGoToRoulette_America += () => coroutines.StartCoroutine(LoadAndStartGameScene_3_America());
        sceneEntryPoint.OnGoToRoulette_AmericaMulti += () => coroutines.StartCoroutine(LoadAndStartGameScene_4_AmericaMulti());
        sceneEntryPoint.OnGoToRoulette_French += () => coroutines.StartCoroutine(LoadAndStartGameScene_5_French());
        sceneEntryPoint.OnGoToRoulette_AmericaTracker += () => coroutines.StartCoroutine(LoadAndStartGameScene_6_AmericaTracker());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartGameScene_1_Mini()
    {
        rootView.SetLoadScreen(1);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_1_MINI);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<GameSceneEntryPoint_MiniGame>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());


        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartGameScene_2_Euro()
    {
        rootView.SetLoadScreen(1);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_2_EURO);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<GameSceneEntryPoint_Euro>();

        sceneEntryPoint.Run(rootView);
        sceneEntryPoint.OnGoToMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());


        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartGameScene_3_America()
    {
        rootView.SetLoadScreen(1);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_3_AMERICA);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<GameSceneEntryPoint_America>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartGameScene_4_AmericaMulti()
    {
        rootView.SetLoadScreen(1);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_4_AMERICA_MULTI);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<GameSceneEntryPoint_AmericaMulti>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());


        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartGameScene_5_French()
    {
        rootView.SetLoadScreen(1);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_5_FRENCH);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<GameSceneEntryPoint_French>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());


        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartGameScene_6_AmericaTracker()
    {
        rootView.SetLoadScreen(1);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_6_AMERICA_TRACKER);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<GameSceneEntryPoint_AmericaTracker>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());


        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string scene)
    {
        Debug.Log("Загрузка сцены - " + scene);
        yield return SceneManager.LoadSceneAsync(scene);
    }
}
