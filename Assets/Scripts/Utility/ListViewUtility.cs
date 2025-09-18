using UnityEngine;
using UnityEngine.UI;

public class ListViewUtility
{
    /// <summary>
    /// 指定した要素にスクロールする
    /// </summary>
    public void ScrollToTarget(ScrollRect targetScroll, RectTransform content, RectTransform targetContent)
    {
        float viewportHeight = targetScroll.viewport.rect.height;
        float currentContentAnchorPosition = targetScroll.content.anchoredPosition.y;
        float displayScrollViewArea = viewportHeight + currentContentAnchorPosition;
        float targetContentTopY = -targetContent.anchoredPosition.y;
        float targetContentUnderY = -targetContent.anchoredPosition.y + targetContent.rect.height;


        //↑移動でスクロールする場合
        if (targetContentTopY < currentContentAnchorPosition)
        {
            // Content内でのアイテムの位置（0〜1の範囲）
            float contentHeight = content.rect.height;

            // スクロール位置を計算（0が最下部、1が最上部）
            float normalizedPosition = 1 - (targetContentTopY / (contentHeight - viewportHeight));
            normalizedPosition = Mathf.Clamp01(normalizedPosition);

            targetScroll.verticalNormalizedPosition = normalizedPosition;
            return;
        }

        // ↓移動でスクロールする場合
        if (targetContentUnderY > displayScrollViewArea)
        {
            // Content内でのアイテムの位置（0〜1の範囲）
            float contentHeight = content.rect.height;

            // 選択項目を表示されているListの最下段に表示したいので
            // targetContentUnderYから、Listの最上部のY座標を算出する
            float listTopY = targetContentUnderY - viewportHeight;


            // スクロール位置を計算（0が最下部、1が最上部）
            float normalizedPosition = 1 - (listTopY / (contentHeight - viewportHeight));
            normalizedPosition = Mathf.Clamp01(normalizedPosition);

            targetScroll.verticalNormalizedPosition = normalizedPosition;
            return;
        }
    }
}
