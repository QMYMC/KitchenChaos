using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyBindingManager : MonoBehaviour
{
    public static KeyBindingManager Instance { get; private set; }

    private KeyCode actionKey = KeyCode.E;
    private KeyCode operateKey = KeyCode.F;
    private KeyCode moveUpKey = KeyCode.W;
    private KeyCode moveDownKey = KeyCode.S;
    private KeyCode moveLeftKey = KeyCode.A;
    private KeyCode moveRightKey = KeyCode.D;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 可选：如果想在场景间保持不销毁
        }
        else
        {
            Destroy(gameObject); // 确保单例模式
        }
    }

    public void SetActionKey(KeyCode newKey) => actionKey = newKey;
    public void SetOperateKey(KeyCode newKey) => operateKey = newKey;
    public void SetMoveUpKey(KeyCode newKey) => moveUpKey = newKey;
    public void SetMoveDownKey(KeyCode newKey) => moveDownKey = newKey;
    public void SetMoveLeftKey(KeyCode newKey) => moveLeftKey = newKey;
    public void SetMoveRightKey(KeyCode newKey) => moveRightKey = newKey;

    public KeyCode GetActionKey() => actionKey;
    public KeyCode GetOperateKey() => operateKey;
    public KeyCode GetMoveUpKey() => moveUpKey;
    public KeyCode GetMoveDownKey() => moveDownKey;
    public KeyCode GetMoveLeftKey() => moveLeftKey;
    public KeyCode GetMoveRightKey() => moveRightKey;

    public void UpdateButtonText(Button button, KeyCode key)
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            buttonText.text = key.ToString();
        }
    }
}