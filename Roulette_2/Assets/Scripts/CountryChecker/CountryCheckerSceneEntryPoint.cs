using System;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;

public class CountryCheckerSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UICountryCheckerSceneRoot sceneRootPrefab;

    private UICountryCheckerSceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private GeoLocationPresenter geoLocationPresenter;
    private InternetPresenter internetPresenter;
    private SoundPresenter soundPresenter;

    private FirebaseDatabasePresenter firebaseDatabaseRealtimePresenter;

    private string currentCountry;

    public void Run(UIRootView uIRootView)
    {
        Debug.Log("OPEN COUNTRY CHECKER SCENE");

        sceneRoot = Instantiate(sceneRootPrefab);
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                soundPresenter = new SoundPresenter(new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS), viewContainer.GetView<SoundView>());
                soundPresenter.Initialize();

                FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
                FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
                DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

                firebaseDatabaseRealtimePresenter = new FirebaseDatabasePresenter
                (new FirebaseDatabaseModel(firebaseAuth, databaseReference, soundPresenter));

                geoLocationPresenter = new GeoLocationPresenter(new GeoLocationModel());

                internetPresenter = new InternetPresenter(new InternetModel(), viewContainer.GetView<InternetView>());
                internetPresenter.Initialize();

                ActivateActions();

                internetPresenter.StartCheckInternet();
            }
            else
            {
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

    }

    public void Dispose()
    {
        DeactivateActions();

        internetPresenter?.Dispose();
    }

    private void ActivateActions()
    {
        internetPresenter.OnInternetUnavailable += TransitionToMainMenu;
        internetPresenter.OnInternetAvailable += OnInternetAvailable;

        firebaseDatabaseRealtimePresenter.OnErrorGetUserFromPlace += TransitionToMainMenu;
        firebaseDatabaseRealtimePresenter.OnGetUserFromPlace += CheckUser;

        geoLocationPresenter.OnErrorGetCountry += TransitionToMainMenu;
        geoLocationPresenter.OnGetCountry += ActivateSceneInCountry;

        firebaseDatabaseRealtimePresenter.OnErrorGetCountries += TransitionToMainMenu;
        firebaseDatabaseRealtimePresenter.OnGetCountries += CheckCountry;
    }

    private void DeactivateActions()
    {
        internetPresenter.OnInternetUnavailable -= TransitionToMainMenu;
        internetPresenter.OnInternetAvailable -= OnInternetAvailable;

        firebaseDatabaseRealtimePresenter.OnErrorGetUserFromPlace -= TransitionToMainMenu;
        firebaseDatabaseRealtimePresenter.OnGetUserFromPlace -= CheckUser;

        geoLocationPresenter.OnErrorGetCountry -= TransitionToMainMenu;
        geoLocationPresenter.OnGetCountry -= ActivateSceneInCountry;

        firebaseDatabaseRealtimePresenter.OnErrorGetCountries -= TransitionToMainMenu;
        firebaseDatabaseRealtimePresenter.OnGetCountries -= CheckCountry;
    }

    private void OnInternetAvailable()
    {
        Debug.Log("INTERNET CONNECTION = TRUE");
        firebaseDatabaseRealtimePresenter.GetUserFromPlace(1);
    }

    private void CheckUser(UserData userData)
    {
        Debug.Log(userData.Nickname + "//" + userData.Record);

        if(userData.Nickname == "topper")
        {
            Debug.Log("ADMIN IN FIRST");
            geoLocationPresenter.GetUserCountry();
        }
        else
        {
            Debug.Log("ADMIN NOT FIRST");
            TransitionToMainMenu();
        }
    }

    private void ActivateSceneInCountry(string country)
    {
        currentCountry = country;

        firebaseDatabaseRealtimePresenter.GetCountries();
    }

    private void CheckCountry(List<string> countries)
    {
        if (countries.Contains(currentCountry))
        {
            Debug.Log("GOOD COUNTRY = TRUE");
            TransitionToOther();
        }
        else
        {
            Debug.Log("GOOD COUNTRY = FALSE");
            TransitionToMainMenu();
        }
    }

    #region Input

    public event Action GoToMainMenu;
    public event Action GoToOther;

    private void TransitionToMainMenu()
    {
        Dispose();
        Debug.Log("NO GOOD");
        GoToMainMenu?.Invoke();
    }

    private void TransitionToOther()
    {
        Dispose();
        Debug.Log("GOOD");
        GoToOther?.Invoke();
    }

    #endregion
}
