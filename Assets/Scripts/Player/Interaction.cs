using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    private float maxCheckDistance;
    public float checkDistanceOnFPS;
    public float plusCheckDistanceOnTPS;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;

    private Camera camera;

    //TODO : 추후 스크립트로 연결하도록 수정
    public Transform interactionRayPointTransform;


    void Start()
    {
        camera = Camera.main;
        promptText = UIManager.Instance.InGameUI.transform.Find("PromptText").GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = returnInteractionRay();
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private Ray returnInteractionRay()
    {
        Ray ray;

        if (GameManager.Instance.player.cameraController.cameraMode == CameraMode.FirstPersonView)
        {
            ray = camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            maxCheckDistance = checkDistanceOnFPS;
        }
        else
        {
            ray = new Ray(interactionRayPointTransform.position, interactionRayPointTransform.forward);
            maxCheckDistance = checkDistanceOnFPS + plusCheckDistanceOnTPS;
        }

        return ray;
    }


    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
