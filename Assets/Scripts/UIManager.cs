using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Transform heartsParent;

    [SerializeField] private Sprite emptyHeartSprite;

    private void Start()
    {
        exitButton.onClick.AddListener(Application.Quit);
    }

    public void SetEmptyHeartAtIndex(int index)
    {
        heartsParent.GetChild(index).GetComponent<Image>().sprite = emptyHeartSprite;
    }

    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene("Main Menu");
    }
}