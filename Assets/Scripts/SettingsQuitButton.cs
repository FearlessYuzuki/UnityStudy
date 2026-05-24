using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SettingsQuitButton : MonoBehaviour
{
   private UIDocument uiDocument;

   private void Awake()
   {
      uiDocument = GetComponent<UIDocument>();
      
      var ExitButton = uiDocument.rootVisualElement.Q<Button>("SettingsQuit");
      ExitButton?.RegisterCallback<ClickEvent>(BackToMain);
   }

   private void BackToMain(ClickEvent evt)
   {
      SceneManager.LoadScene("Main UI");
   }
}
