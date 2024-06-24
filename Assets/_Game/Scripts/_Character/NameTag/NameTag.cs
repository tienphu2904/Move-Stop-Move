using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameTag : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killCountText, characterName;
    [SerializeField] private Image backgroundImage;
    private Transform cameraTransform;
    private Vector3 offset = new Vector3(0, 180, 0);

    private void Start()
    {
        cameraTransform = CameraFollow.Ins.transform;
    }

    void Update()
    {
        transform.LookAt(cameraTransform);
        transform.Rotate(offset);
    }

    public void OnInit(string name, Color color)
    {
        transform.gameObject.SetActive(true);
        SetCharacterName(name);
        SetBackgroundImage(color);
    }

    public void OnDespawn()
    {
        transform.gameObject.SetActive(false);
        SetKillCountText(0);
    }

    public void SetKillCountText(int killCount)
    {
        killCountText.text = killCount.ToString();
    }


    public void SetCharacterName(string name)
    {
        characterName.text = name;
    }

    public void SetBackgroundImage(Color color)
    {
        backgroundImage.color = color;
    }
}
