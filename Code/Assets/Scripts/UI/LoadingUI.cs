using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;
    private string baseText; // 保存初始的文本内容

    private void Start()
    {
        baseText = loadingText.text; // 将初始文本内容保存下来
        StartCoroutine(LoadingAnimation());
    }

    IEnumerator LoadingAnimation()
    {
        int maxDots = 6;
        int currentDots = 0;

        while (true)
        {
            // 构建显示文本
            string dots = new string('.', currentDots);
            loadingText.text = baseText + dots;

            // 更新点数
            currentDots = (currentDots + 1) % (maxDots + 1);

            // 每隔0.5秒更新一次
            yield return new WaitForSeconds(0.5f);
        }
    }
}