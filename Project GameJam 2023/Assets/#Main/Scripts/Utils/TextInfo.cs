using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextInfo : MonoBehaviour
{
    public string textValue;
    public TextMeshProUGUI textInfo;
    public float timeHide;
    public float speedMove;

    public Color colorGreen;
    public Color colorRed;

    public void ShowText(string val, int col = 0)
    {
        textValue = val;
        textInfo.text = textValue;
        if (col == 0) textInfo.color = colorRed;
        else textInfo.color = colorGreen;

        StartCoroutine(MoveAndDisappear(gameObject, Vector3.up, speedMove, timeHide));
    }

    void Update()
    {
    }

    private IEnumerator MoveAndDisappear(GameObject obj, Vector3 direction, float moveSpeed, float disappearTime)
    {
        float elapsedTime = 0;

        while (elapsedTime < disappearTime)
        {
            obj.transform.Translate(direction * moveSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(obj);
    }
}
