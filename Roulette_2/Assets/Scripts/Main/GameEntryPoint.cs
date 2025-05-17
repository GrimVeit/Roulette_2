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
        coroutines.StartCoroutine(LoadAndStartCountryChecker());
    }

    private IEnumerator LoadAndStartCountryChecker()
    {
        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.CHECKER);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<CountryCheckerSceneEntryPoint>();

        sceneEntryPoint.GoToOther += () => coroutines.StartCoroutine(LoadAndStartOther());
        sceneEntryPoint.GoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

        sceneEntryPoint.Run(rootView);
    }

    private IEnumerator LoadAndStartOther()
    {
        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.OTHER);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<OtherSceneEntryPoint>();

        sceneEntryPoint.OnGoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

        sceneEntryPoint.Run(rootView);
    }

    private IEnumerator LoadAndStartMainMenu()
    {
        yield return rootView.ShowLoadingScreen(0);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MAIN_MENU);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();

        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToRoulette_Mini += () => coroutines.StartCoroutine(LoadAndStartGameScene_1_Mini());
        sceneEntryPoint.OnGoToRoulette_Euro += () => coroutines.StartCoroutine(LoadAndStartGameScene_2_Euro());
        sceneEntryPoint.OnGoToRoulette_America += () => coroutines.StartCoroutine(LoadAndStartGameScene_3_America());
        sceneEntryPoint.OnGoToRoulette_AmericaMulti += () => coroutines.StartCoroutine(LoadAndStartGameScene_4_AmericaMulti());
        sceneEntryPoint.OnGoToRoulette_French += () => coroutines.StartCoroutine(LoadAndStartGameScene_5_French());
        sceneEntryPoint.OnGoToRoulette_AmericaTracker += () => coroutines.StartCoroutine(LoadAndStartGameScene_6_AmericaTracker());

        yield return rootView.HideLoadingScreen(0);
    }

    private IEnumerator LoadAndStartGameScene_1_Mini()
    {
        yield return rootView.ShowLoadingScreen(1);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_1_MINI);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<GameSceneEntryPoint_MiniGame>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

        yield return rootView.HideLoadingScreen(1);
    }

    private IEnumerator LoadAndStartGameScene_2_Euro()
    {
        yield return rootView.ShowLoadingScreen(2);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_2_EURO);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<GameSceneEntryPoint_Euro>();

        sceneEntryPoint.Run(rootView);
        sceneEntryPoint.OnGoToMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());


        yield return rootView.HideLoadingScreen(2);
    }

    private IEnumerator LoadAndStartGameScene_3_America()
    {
        yield return rootView.ShowLoadingScreen(3);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_3_AMERICA);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<GameSceneEntryPoint_America>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

        yield return rootView.HideLoadingScreen(3);
    }

    private IEnumerator LoadAndStartGameScene_4_AmericaMulti()
    {
        yield return rootView.ShowLoadingScreen(4);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_4_AMERICA_MULTI);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<GameSceneEntryPoint_AmericaMulti>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());


        yield return rootView.HideLoadingScreen(4);
    }

    private IEnumerator LoadAndStartGameScene_5_French()
    {
        yield return rootView.ShowLoadingScreen(5);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_5_FRENCH);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<GameSceneEntryPoint_French>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());


        yield return rootView.HideLoadingScreen(5);
    }

    private IEnumerator LoadAndStartGameScene_6_AmericaTracker()
    {
        yield return rootView.ShowLoadingScreen(6);

        yield return new WaitForSeconds(0.3f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAME_6_AMERICA_TRACKER);

        yield return new WaitForEndOfFrame();

        var sceneEntryPoint = Object.FindObjectOfType<GameSceneEntryPoint_AmericaTracker>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());


        yield return rootView.HideLoadingScreen(6);
    }

    private IEnumerator LoadScene(string scene)
    {
        Debug.Log("Загрузка сцены - " + scene);
        yield return SceneManager.LoadSceneAsync(scene);
    }
}
