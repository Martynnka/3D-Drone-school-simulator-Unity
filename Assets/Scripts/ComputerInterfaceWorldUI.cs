using UnityEngine;

public class ComputerInteractWorldUI : MonoBehaviour
{
    public GameObject promptCanvas;
    public GameObject computerPanel;

    [Header("Disable movement script when using computer")]
    public MonoBehaviour movementScript; // <- čia užtempk PlayerMovement (ar kitą) scriptą iš Player

    public KeyCode interactKey = KeyCode.E;
    private bool playerInRange;

    void Start()
    {
        if (promptCanvas) promptCanvas.SetActive(false);
        if (computerPanel) computerPanel.SetActive(false);
    }

    void Update()
    {
        if (!playerInRange) return;

        // kad nespaudytum E kai panelė jau atidaryta
        if (computerPanel != null && computerPanel.activeSelf) return;

        if (Input.GetKeyDown(interactKey))
            OpenComputer();
    }

    void OpenComputer()
    {
        if (promptCanvas) promptCanvas.SetActive(false);
        if (computerPanel) computerPanel.SetActive(true);

        if (movementScript) movementScript.enabled = false; // ✅ stop movement

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void TurnDownPanel()
    {
        if (computerPanel) computerPanel.SetActive(false);

        if (movementScript) movementScript.enabled = true; // ✅ resume movement

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
        if (promptCanvas) promptCanvas.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        if (promptCanvas) promptCanvas.SetActive(false);
    }
}
