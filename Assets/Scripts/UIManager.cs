using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button ExitButton;
    [SerializeField] private Transform heartsParent;

    [SerializeField] private Sprite emptyHeartSprite;

    private void Start()
    {
        ExitButton.onClick.AddListener(Application.Quit);
    }

    public void SetEmptyHeartAtIndex(int index)
    {
        heartsParent.GetChild(index).GetComponent<Image>().sprite = emptyHeartSprite;
    }
}