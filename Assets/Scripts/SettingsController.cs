using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SettingsController : MonoBehaviour
{
    private UIDocument uiDocument;

    private DropdownField resolutionDropdown;
    private Toggle fullscreenToggle;
    private Button saveBtn;
    private Button resetBtn;
    private Button exitBtn;

    private List<Resolution> uniqueResolutions = new List<Resolution>();
    private int currentResolutionIndex;
    private bool currentFullscreen;

    private const string PREFS_RESOLUTION_WIDTH = "Settings_ResolutionWidth";
    private const string PREFS_RESOLUTION_HEIGHT = "Settings_ResolutionHeight";
    private const string PREFS_FULLSCREEN = "Settings_Fullscreen";

    // 默认值
    private const int DEFAULT_WIDTH = 1920;
    private const int DEFAULT_HEIGHT = 1080;
    private const bool DEFAULT_FULLSCREEN = true;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();

        resolutionDropdown = uiDocument.rootVisualElement.Q<DropdownField>("ResolutionDropdown");
        fullscreenToggle = uiDocument.rootVisualElement.Q<Toggle>("FullscreenToggle");
        saveBtn = uiDocument.rootVisualElement.Q<Button>("Save");
        resetBtn = uiDocument.rootVisualElement.Q<Button>("ResetButton");
        exitBtn = uiDocument.rootVisualElement.Q<Button>("SettingsQuit");

        InitResolutions();
        LoadSettings();

        saveBtn?.RegisterCallback<ClickEvent>(OnSaveClicked);
        resetBtn?.RegisterCallback<ClickEvent>(OnResetClicked);
        exitBtn?.RegisterCallback<ClickEvent>(OnExitClicked);

        // 当dropdown或toggle改变时, 不立即应用, 等待用户点击Save
        resolutionDropdown?.RegisterValueChangedCallback(OnResolutionChanged);
        fullscreenToggle?.RegisterValueChangedCallback(OnFullscreenChanged);
    }

    /// <summary>
    /// 收集唯一分辨率并填充Dropdown
    /// </summary>
    private void InitResolutions()
    {
        if (resolutionDropdown == null) return;

        uniqueResolutions.Clear();
        resolutionDropdown.choices.Clear();

        var seen = new HashSet<string>();
        foreach (var res in Screen.resolutions)
        {
            var key = $"{res.width}x{res.height}";
            if (seen.Contains(key)) continue;
            seen.Add(key);
            uniqueResolutions.Add(res);
            resolutionDropdown.choices.Add($"{res.width} x {res.height}");
        }

        // 默认选中当前分辨率
        var currentKey = $"{Screen.currentResolution.width}x{Screen.currentResolution.height}";
        currentResolutionIndex = resolutionDropdown.choices.IndexOf(currentKey);
        if (currentResolutionIndex < 0) currentResolutionIndex = resolutionDropdown.choices.Count - 1;
    }

    /// <summary>
    /// 从 PlayerPrefs 加载已保存的设置
    /// </summary>
    private void LoadSettings()
    {
        int savedWidth = PlayerPrefs.GetInt(PREFS_RESOLUTION_WIDTH, DEFAULT_WIDTH);
        int savedHeight = PlayerPrefs.GetInt(PREFS_RESOLUTION_HEIGHT, DEFAULT_HEIGHT);
        bool savedFullscreen = PlayerPrefs.GetInt(PREFS_FULLSCREEN, DEFAULT_FULLSCREEN ? 1 : 0) == 1;

        // 在dropdown中找到已保存的分辨率
        var key = $"{savedWidth}x{savedHeight}";
        int idx = resolutionDropdown?.choices.IndexOf(key) ?? -1;
        if (idx >= 0)
        {
            currentResolutionIndex = idx;
        }

        currentFullscreen = savedFullscreen;

        // 应用到UI
        if (resolutionDropdown != null)
            resolutionDropdown.index = currentResolutionIndex;

        if (fullscreenToggle != null)
            fullscreenToggle.value = currentFullscreen;

        // 实际应用设置
        ApplySettings();
    }

    private void OnResolutionChanged(ChangeEvent<string> evt)
    {
        currentResolutionIndex = resolutionDropdown.index;
    }

    private void OnFullscreenChanged(ChangeEvent<bool> evt)
    {
        currentFullscreen = evt.newValue;
    }

    /// <summary>
    /// 应用当前设置到屏幕
    /// </summary>
    private void ApplySettings()
    {
        if (currentResolutionIndex >= 0 && currentResolutionIndex < uniqueResolutions.Count)
        {
            var res = uniqueResolutions[currentResolutionIndex];
            Screen.SetResolution(res.width, res.height, currentFullscreen ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed);
        }
    }

    private void OnSaveClicked(ClickEvent evt)
    {
        // 应用设置
        ApplySettings();

        // 保存到 PlayerPrefs
        if (currentResolutionIndex >= 0 && currentResolutionIndex < uniqueResolutions.Count)
        {
            var res = uniqueResolutions[currentResolutionIndex];
            PlayerPrefs.SetInt(PREFS_RESOLUTION_WIDTH, res.width);
            PlayerPrefs.SetInt(PREFS_RESOLUTION_HEIGHT, res.height);
        }
        PlayerPrefs.SetInt(PREFS_FULLSCREEN, currentFullscreen ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("[SettingsController] 设置已保存");
    }

    private void OnResetClicked(ClickEvent evt)
    {
        // 重置UI到默认值
        var defaultKey = $"{DEFAULT_WIDTH}x{DEFAULT_HEIGHT}";
        int idx = resolutionDropdown?.choices.IndexOf(defaultKey) ?? 0;
        if (idx < 0) idx = resolutionDropdown.choices.Count - 1;

        currentResolutionIndex = idx;
        currentFullscreen = DEFAULT_FULLSCREEN;

        if (resolutionDropdown != null)
            resolutionDropdown.index = currentResolutionIndex;

        if (fullscreenToggle != null)
            fullscreenToggle.value = currentFullscreen;

        // 立即应用并保存
        ApplySettings();

        PlayerPrefs.SetInt(PREFS_RESOLUTION_WIDTH, DEFAULT_WIDTH);
        PlayerPrefs.SetInt(PREFS_RESOLUTION_HEIGHT, DEFAULT_HEIGHT);
        PlayerPrefs.SetInt(PREFS_FULLSCREEN, DEFAULT_FULLSCREEN ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("[SettingsController] 设置已重置为默认值");
    }

    private void OnExitClicked(ClickEvent evt)
    {
        SceneManager.LoadScene("Main UI");
    }
}