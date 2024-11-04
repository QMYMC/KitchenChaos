using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class KeyBindingUI : MonoBehaviour
{
    [SerializeField] private Button bindActionButton; // 绑定动作按键的按钮
    [SerializeField] private Button bindOperateButton; // 绑定操作按键的按钮
    [SerializeField] private Button bindMoveUpButton; // 绑定向上移动按键的按钮
    [SerializeField] private Button bindMoveDownButton; // 绑定向下移动按键的按钮
    [SerializeField] private Button bindMoveLeftButton; // 绑定向左移动按键的按钮
    [SerializeField] private Button bindMoveRightButton; // 绑定向右移动按键的按钮
    [SerializeField] private TMP_Text hintText; // 显示提示信息的TMP Text

    private void Awake()
    {
        // 初始化按钮文本
        UpdateButtonTexts();

        // 为每个按钮添加监听器
        bindActionButton.onClick.AddListener(() => StartKeyCapture(KeyBindingManager.Instance.SetActionKey, bindActionButton));
        bindOperateButton.onClick.AddListener(() => StartKeyCapture(KeyBindingManager.Instance.SetOperateKey, bindOperateButton));
        bindMoveUpButton.onClick.AddListener(() => StartKeyCapture(KeyBindingManager.Instance.SetMoveUpKey, bindMoveUpButton));
        bindMoveDownButton.onClick.AddListener(() => StartKeyCapture(KeyBindingManager.Instance.SetMoveDownKey, bindMoveDownButton));
        bindMoveLeftButton.onClick.AddListener(() => StartKeyCapture(KeyBindingManager.Instance.SetMoveLeftKey, bindMoveLeftButton));
        bindMoveRightButton.onClick.AddListener(() => StartKeyCapture(KeyBindingManager.Instance.SetMoveRightKey, bindMoveRightButton));
    }

    private void UpdateButtonTexts()
    {
        // 更新每个按钮上的文本
        KeyBindingManager km = KeyBindingManager.Instance;
        km.UpdateButtonText(bindActionButton, km.GetActionKey());
        km.UpdateButtonText(bindOperateButton, km.GetOperateKey());
        km.UpdateButtonText(bindMoveUpButton, km.GetMoveUpKey());
        km.UpdateButtonText(bindMoveDownButton, km.GetMoveDownKey());
        km.UpdateButtonText(bindMoveLeftButton, km.GetMoveLeftKey());
        km.UpdateButtonText(bindMoveRightButton, km.GetMoveRightKey());
    }

    private void StartKeyCapture(System.Action<KeyCode> setKeyAction, Button button)
    {
        // 提示用户按下新的按键
        hintText.text = "请按下新的按键..."; 
        button.interactable = false; // 禁用按钮以避免重复点击
        StartCoroutine(CaptureKey(setKeyAction, button)); // 开始捕获按键
    }

    private IEnumerator CaptureKey(System.Action<KeyCode> setKeyAction, Button button)
    {
        while (true)
        {
            // 检测A到Z键
            for (KeyCode key = KeyCode.A; key <= KeyCode.Z; key++)
            {
                if (Input.GetKeyDown(key))
                {
                    setKeyAction(key); // 设置新的按键绑定
                    KeyBindingManager.Instance.UpdateButtonText(button, key); // 更新按钮文本
                    hintText.text = $"{key} 已绑定！"; // 提示用户按键绑定成功
                    button.interactable = true; // 重新启用按钮
                    yield break; // 结束协程
                }
            }

            // 检测数字键0到9
            for (KeyCode key = KeyCode.Alpha0; key <= KeyCode.Alpha9; key++)
            {
                if (Input.GetKeyDown(key))
                {
                    setKeyAction(key); // 设置新的按键绑定
                    KeyBindingManager.Instance.UpdateButtonText(button, key); // 更新按钮文本
                    hintText.text = $"{key} 已绑定！"; // 提示用户按键绑定成功
                    button.interactable = true; // 重新启用按钮
                    yield break; // 结束协程
                }
            }

            yield return null; // 等待下一帧
        }
    }
}
