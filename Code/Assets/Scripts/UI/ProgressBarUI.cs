 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image progresssIamge;

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateProgress(float progresss)
    {
        Show();
        progresssIamge.fillAmount = progresss;

        if (progresss==1)
        {
            Invoke("Hide", 0.5f);
        }
    }
}
