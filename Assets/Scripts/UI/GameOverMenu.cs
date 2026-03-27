using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public GameObject timeSystem;
    public PlayerDamageHandle playerHealth;
    public FirstPersonController playerController;
    public PlayerThrowItem playerThrowItem;

    private CanvasGroup canvasGroup;
    void Awake()
    {
        timeSystem = GameObject.FindGameObjectWithTag("TimeSystem");
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamageHandle>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        playerThrowItem = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerThrowItem>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth.Health.OnDied += PopUpMenu;
    }
    void OnDisable()
    {
        playerHealth.Health.OnDied -= PopUpMenu;
    }
    void PopUpMenu()
    {
        playerThrowItem.Throw();
        Time.timeScale = 0f;

        playerController.cameraCanMove = false;
        playerController.lockCursor = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        timeSystem.SetActive(false);

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
