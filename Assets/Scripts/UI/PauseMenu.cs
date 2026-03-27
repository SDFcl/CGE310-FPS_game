using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject timeSystem;
    public KeyCode buttonPauseGame = KeyCode.Escape;
    public FirstPersonController player;

    private CanvasGroup canvasGroup;
    private bool isPuUpMenu;

    void Awake()
    {
        timeSystem = GameObject.FindGameObjectWithTag("TimeSystem");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (Input.GetKeyDown(buttonPauseGame))
        {
            Pause();
        }
    }

    void Pause()
    {
        if (isPuUpMenu)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;

        player.cameraCanMove = false;
        player.lockCursor = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        timeSystem.SetActive(false);
        PopUpMenu(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        player.cameraCanMove = true;
        player.lockCursor = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        timeSystem.SetActive(true);
        PopUpMenu(false);
    }

    void PopUpMenu(bool show)
    {
        isPuUpMenu = show;

        if (show)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}