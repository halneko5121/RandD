using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

namespace RandB
{
    public static class TextToChar
    {
        //---------------------------------------------
        // MonoBehaviour
        //---------------------------------------------

        //---------------------------------------------
        // Method
        //---------------------------------------------
        /**
         * 指定されたTextオブジェクトを、1文字づつにしたTextオブジェクトに変換
         */
        public static List<TMP_Text> ConvertToChar(TMP_Text templeteText, string targetString)
        {
            // 元のコンポーネントを保持
            var charList = new List<TMP_Text>();

            // 文字数カウント
            templeteText.gameObject.SetActive(true);
            templeteText.text = targetString;
            var count = templeteText.text.Length;
            char[] textString = templeteText.text.ToCharArray();

            // 「文字列」を「1文字」に分割
            charList.Clear();
            for (int i = 0; i < count; i++)
            {
                // 初期設定
                var text = Object.Instantiate(templeteText);
                text.gameObject.transform.SetParent(templeteText.gameObject.transform.parent.transform);
                text.gameObject.transform.localPosition = Vector3.zero;
                text.gameObject.transform.localScale = Vector3.one;
                text.text = textString[i].ToString();
                var objectName = "[" + text.text + "]" + "Text";
                var replaceName = templeteText.name.Replace(templeteText.name, objectName);
                text.name = replaceName;

                // サイズ調整
                var tmp_obj = text.GetComponent<TextMeshProUGUI>();
                text.rectTransform.sizeDelta = new Vector2(tmp_obj.preferredWidth, tmp_obj.preferredHeight);
                charList.Add(text);
            }
            templeteText.gameObject.SetActive(false);

            return charList;
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
    }
}