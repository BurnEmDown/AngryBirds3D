using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform levelButtonsHolder;
    [SerializeField] private Button levelButtonPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        SetLevelButtons();
    }

    // I don't like the way this method is structured - extremely not flexible, but I will keep it this way to save time
    private void SetLevelButtons()
    {
        List<string> levelNames = new List<string>();
        foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
                string name = S.path.Substring(S.path.LastIndexOf('/') + 1);
                name = name.Substring(0, name.Length - 6);
                levelNames.Add(name);
            }
        }

        foreach (var levelName in levelNames)
        {
            if (levelName == "Main Menu")
                continue;

            Button levelButton = Instantiate(levelButtonPrefab, levelButtonsHolder.position, Quaternion.identity,
                levelButtonsHolder);
            levelButton.onClick.AddListener(() => SceneManager.LoadScene(levelName));
            levelButton.GetComponentInChildren<TMP_Text>().text = levelName;
        }
    }
}
