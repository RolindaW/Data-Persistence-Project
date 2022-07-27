using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Jobs;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TextMeshProUGUI footerText;
    
    // Start is called before the first frame update
    void Start()
    {
        string name = ConfigurationManager.Instance.Name;
        int score = ConfigurationManager.Instance.Score;

        if (ConfigurationManager.Instance.IsValidName(name))
        {
            bestScoreText.text = String.Format("Best Score: {0} - {1}", name, score);
            nameInput.text = name;
        }
    }

    public void StartGame()
    {
        string name = nameInput.text;
        if (ConfigurationManager.Instance.IsValidName(name))
        {
            ConfigurationManager.Instance.ActiveName = name;
            SceneManager.LoadScene(1);
        }
        else
        {
            StartCoroutine(DisplayFooterMessageText());
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit;
        #endif
    }

    private IEnumerator DisplayFooterMessageText()
    {
        footerText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        footerText.gameObject.SetActive(false);
    }
}
