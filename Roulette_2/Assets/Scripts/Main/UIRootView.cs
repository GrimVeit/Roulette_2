using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRootView : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private List<Panel> loadScreens = new List<Panel>();
    [SerializeField] private Transform uiSceneContainer;
    public IEnumerator ShowLoadingScreen(int index)
    {
        Debug.Log("Start Show");
        loadScreens[index].ActivatePanel();
        yield return new WaitForSeconds(0.3f);
        Debug.Log("End Show");
    }

    public IEnumerator HideLoadingScreen(int index)
    {
        Debug.Log("Start Hide");
        loadScreens[index].DeactivatePanel();
        yield return new WaitForSeconds(0.3f);
        Debug.Log("End Hide");
    }

    public void AttachSceneUI(GameObject sceneUI, Camera camera)
    {
        ClearSceneUI();

        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = camera;

        sceneUI.transform.SetParent(uiSceneContainer, false);
        sceneUI.transform.localScale = Vector3.one;

        RectTransform rectTransform = sceneUI.GetComponent<RectTransform>();

        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;

        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }

    private void ClearSceneUI()
    {
        for (int i = 0; i < uiSceneContainer.childCount; i++)
        {
            Destroy(uiSceneContainer.GetChild(i).gameObject);
        }
    }
}
