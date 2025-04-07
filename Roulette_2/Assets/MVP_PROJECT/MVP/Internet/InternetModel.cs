using System;
using UnityEngine;

public class InternetModel
{
    public event Action<string> OnGetStatusDescription;
    public event Action OnConnectionAvailable;
    public event Action OnConnectionUnvailable;

    public void CheckConnection()
    {
        //Coroutines.Start(CheckInternet_Coroutine());

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Internet disable");
            OnConnectionUnvailable?.Invoke();
            OnGetStatusDescription?.Invoke("Unable to connect. Please check your internet connection");
        }
        else
        {
            Debug.Log("Internet enable");
            OnConnectionAvailable?.Invoke();
        }
    }

    //private IEnumerator CheckInternet_Coroutine()
    //{
    //    //while (Application.internetReachability == NetworkReachability.NotReachable)
    //    //{
    //    //    isProblem = true;
    //    //    OnInternetUnvailable?.Invoke();
    //    //    Debug.Log("Подключения к интернету нет");
    //    //    OnGetStatusDescription?.Invoke("Unable to connect. Please check your internet connection");
    //    //    yield return new WaitForSeconds(1);
    //    //}
    //}
}
