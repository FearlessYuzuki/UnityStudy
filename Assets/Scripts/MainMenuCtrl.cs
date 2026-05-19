using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuCtrl : MonoBehaviour
{
    private UIDocument uiDocument;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();

        // 通过 name 找到 UXML 中对应的按钮
        var startBtn = uiDocument.rootVisualElement.Q<Button>("Start");
        var exitBtn = uiDocument.rootVisualElement.Q<Button>("Exit");
        var settingsBtn = uiDocument.rootVisualElement.Q<Button>("SettingsButton");

        // 注册点击事件
        startBtn?.RegisterCallback<ClickEvent>(OnStartClicked);
        exitBtn?.RegisterCallback<ClickEvent>(OnExitClicked);
        settingsBtn?.RegisterCallback<ClickEvent>(OnSettingsClicked);
    }

    private void OnStartClicked(ClickEvent evt)
    {
        SceneManager.LoadScene("GameScenes");
    }

    private void OnExitClicked(ClickEvent evt)
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void OnSettingsClicked(ClickEvent evt)
    {
        SceneManager.LoadScene("SettingsUI");
    }
}