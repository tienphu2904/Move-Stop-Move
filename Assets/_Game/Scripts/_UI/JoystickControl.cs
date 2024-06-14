using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickControl : MonoBehaviour
{
    public static Vector3 direct;

    [SerializeField] private RectTransform joystickBG;
    [SerializeField] private RectTransform joystickControl;
    [SerializeField] private GameObject joystickPanel;

    private Vector3 screen;

    private Vector3 startPoint;

    private Vector3 updatePoint;

    void Awake()
    {
        screen.x = Screen.width;
        screen.y = Screen.height;

        direct = Vector3.zero;

        joystickPanel.SetActive(false);
    }

    void Update()
    {
        if (GameManager.Ins.IsState(GameState.Gameplay) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPoint = Input.mousePosition - screen / 2;
                joystickBG.anchoredPosition = startPoint;
                joystickPanel.SetActive(true);
            }

            if (Input.GetMouseButton(0))
            {
                updatePoint = Input.mousePosition - screen / 2;
                joystickControl.anchoredPosition = Vector3.ClampMagnitude(updatePoint - startPoint, 100f) + startPoint;

                direct = (updatePoint - startPoint).normalized;
                direct.z = direct.y;
                direct.y = 0;

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            joystickPanel.SetActive(false);
            direct = Vector3.zero;
        }
    }

    private void OnDisable()
    {
        direct = Vector3.zero;
    }
}
