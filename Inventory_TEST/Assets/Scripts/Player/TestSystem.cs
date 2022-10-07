using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TestSystem : MonoBehaviour
{
    public float time;
    public string text = null;
    public Text textEntered;

    private void Start()
    {
        textEntered.text = null;
    }

    public void OnPressedButton()
    {
        GameObject.FindGameObjectWithTag("dialog").GetComponent<Button>().enabled = false;
        StartCoroutine(textSystem(text));
    }

    IEnumerator textSystem(string text)
    {
        foreach(var item in text)
        {
            textEntered.text += item;
            yield return new WaitForSeconds(time);
        }

        yield return new WaitForSeconds(1f);
        textEntered.text = null;
    }

}
