using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

namespace RandB
{
    public class TestUI : MonoBehaviour
    {
        //---------------------------------------------
        // MonoBehaviour
        //---------------------------------------------
        void Start()
        {
            var charList = TextToChar.ConvertToChar(templeteText, "Test Function");

            // ゆるやかな拡大：
            // InOutCirc、InOutQuint
            // OutQuint、OutQuart
            // InOutCubic、OutCubic
            // OutQuad
            float targetX = 0.0f;
            float spacing = 0.0f;
            float posX = targetX;
            for (int i = 0; i < charList.Count; i++)
            {
                var str = charList[i];
                str.transform.localPosition = new Vector3(800.0f, 0.0f, 0.0f);

                if (i == 0)
                {
                    posX += str.rectTransform.sizeDelta.x * 0.5f;
                }
                var sequence = DOTween.Sequence()
                    .Append(str.transform.DOLocalMoveX(posX, 0.5f).SetDelay(i * 0.5f).SetEase(Ease.InOutCirc))
                    .Append(str.transform.DORotate(new Vector3(0.0f, 0.0f, 360.0f), 0.5f).SetRelative(true).SetEase(Ease.InOutCirc))
                    ;
                sequence.Play();

                var nextindex = i + 1;
                TMP_Text nextStr = null;
                if (nextindex != charList.Count)
                {
                    nextStr = charList[nextindex];
                }

                float strSizeX = str.rectTransform.sizeDelta.x * 0.5f;
                float nextStrSizeX = (nextStr == null) ? 0.0f : nextStr.rectTransform.sizeDelta.x * 0.5f;
                posX += (strSizeX + spacing + nextStrSizeX);
            }

            /*
            var parent = animationText.gameObject.transform.parent.gameObject;
            Debug.Log("--------------------");
            Debug.Log(parent);
            parent.transform.DOLocalMoveX(500.0f, 3.0f).SetRelative(true).SetEase(Ease.InOutCirc);
            */
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SlowDown();
            }

            //　スローダウンフラグがtrueの時は時間計測
            if (isSlowDown)
            {
                elapsedTime += Time.unscaledDeltaTime;
                if (elapsedTime >= slowTime)
                {
                    SetNormalTime();
                }
            }
        }

        //---------------------------------------------
        // Method
        //---------------------------------------------

        //　時間を遅らせる処理
        public void SlowDown()
        {
            elapsedTime = 0f;
            Time.timeScale = timeScale;
            isSlowDown = true;
        }
        //　時間を元に戻す処理
        public void SetNormalTime()
        {
            Time.timeScale = 1f;
            isSlowDown = false;
        }

        //---------------------------------------------
        // Const/Enum
        //---------------------------------------------

        //---------------------------------------------
        // Property
        //---------------------------------------------

        //---------------------------------------------
        // Field
        //---------------------------------------------
        [SerializeField]
        private TMP_Text templeteText = null;

        //　Time.timeScaleに設定する値
        private float timeScale = 0.1f;
        //　時間を遅くしている時間
        private float slowTime = 1f;
        //　経過時間
        private float elapsedTime = 0f;
        //　時間を遅くしているかどうか
        private bool isSlowDown = false;
    }
}