using UnityEngine;

public class ComputerInteractWorldUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject promptCanvas;
    public GameObject computerPanel;
    public GameObject NewsPanel;
    public GameObject ControlsPanel;

    [Header("Player")]
    public GameObject playerObject;
    public MonoBehaviour movementScript;
    public GameObject playerCameraRoot;

    [Header("Drone Flight Mode")]
    public GameObject droneObject;
    public MonoBehaviour droneControlScript;
    public GameObject droneCameraRoot;
    public KeyCode exitDroneKey = KeyCode.P;

    [Header("Interaction")]
    public KeyCode interactKey = KeyCode.E;

    private bool playerInRange;
    private bool inDroneMode = false;

    void Start()
    {
        if (promptCanvas) promptCanvas.SetActive(false);
        if (computerPanel) computerPanel.SetActive(false);
        if (NewsPanel) NewsPanel.SetActive(false);
        if (ControlsPanel) ControlsPanel.SetActive(false);

        if (droneControlScript) droneControlScript.enabled = false;
    }

    void Update()
    {
        if (inDroneMode)
        {
            if (Input.GetKeyDown(exitDroneKey))
                ReturnToComputerFromDrone();
            return;
        }

        if (!playerInRange) return;
        if (computerPanel != null && computerPanel.activeSelf) return;

        if (Input.GetKeyDown(interactKey))
            OpenComputer();
    }

    void OpenComputer()
    {
        if (promptCanvas) promptCanvas.SetActive(false);
        if (computerPanel) computerPanel.SetActive(true);

        if (movementScript) movementScript.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void TurnDownPanel()
    {
        if (computerPanel) computerPanel.SetActive(false);

        if (movementScript) movementScript.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }

    public void StartFlightMode()
    {
        if (NewsPanel) NewsPanel.SetActive(false);
        if (ControlsPanel) ControlsPanel.SetActive(false);
        if (computerPanel) computerPanel.SetActive(false);

        Time.timeScale = 1f;

        if (playerObject) playerObject.SetActive(false);
        if (droneObject) droneObject.SetActive(true);
        if (droneControlScript) droneControlScript.enabled = true;

        if (playerCameraRoot) playerCameraRoot.SetActive(false);
        if (droneCameraRoot) droneCameraRoot.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        inDroneMode = true;
    }

    public void ReturnToComputerFromDrone()
    {
        if (droneControlScript) droneControlScript.enabled = false;
        if (droneObject) droneObject.SetActive(false);
        if (droneCameraRoot) droneCameraRoot.SetActive(false);

        if (playerObject) playerObject.SetActive(true);
        if (playerCameraRoot) playerCameraRoot.SetActive(true);

        if (computerPanel) computerPanel.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        inDroneMode = false;
    }

    public void TurnNewsOn()
    {
        if (NewsPanel) NewsPanel.SetActive(true);
    }

    public void TurnNewsOff()
    {
        if (NewsPanel) NewsPanel.SetActive(false);
    }

    public void ControlsOn()
    {
        if (ControlsPanel) ControlsPanel.SetActive(true);
    }

    public void ControlsOff()
    {
        if (ControlsPanel) ControlsPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
        if (promptCanvas) promptCanvas.SetActive(true);

        if (playerObject == null)
            playerObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        if (promptCanvas) promptCanvas.SetActive(false);
    }
}


