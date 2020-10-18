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
            // 文字数カウント
            templeteText.gameObject.SetActive(true);
            var count = templeteText.text.Length;
            char[] textString = templeteText.text.ToCharArray();
            UnityEngine.Debug.Log("----------------");
            for (int i = 0; i < count; i++)
            {
                var text = Object.Instantiate(templeteText);
                text.gameObject.transform.SetParent(this.transform);
                text.gameObject.transform.localPosition = Vector3.zero;
                text.text = textString[i].ToString();
                stringList.Add(text);
                UnityEngine.Debug.Log(text.text);
            }
            templeteText.gameObject.SetActive(false);

            // ゆるやかな拡大：
            // InOutCirc、InOutQuint
            // OutQuint、OutQuart
            // InOutCubic、OutCubic
            // OutQuad
            float targetX = 500.0f;
            float stringSize = 150.0f;
            for (int i = 0; i < stringList.Count; i++)
            {
                var str = stringList[i];
                var sequence = DOTween.Sequence()
                    .Append(str.transform.DOLocalMoveX(-targetX + (stringSize * i), 0.5f).SetDelay(i * 0.5f).SetEase(Ease.InOutCirc))
                    .Append(str.transform.DORotate(new Vector3(0.0f, 0.0f, 360.0f), 0.5f).SetRelative(true).SetEase(Ease.InOutCirc))
                    ;
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
        private TMP_Text animationText = null;

        [SerializeField]
        private TMP_Text templeteText = null;

        List<TMP_Text> stringList = new List<TMP_Text>();

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