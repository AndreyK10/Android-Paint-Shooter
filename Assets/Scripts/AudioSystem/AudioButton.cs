using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AudioButton : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    [SerializeField]
    private Sprite _on, _off;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        if (AudioManager.isMusicMuted) _button.image.sprite = _off;
        else if (!AudioManager.isMusicMuted) _button.image.sprite = _on;
    }
}
