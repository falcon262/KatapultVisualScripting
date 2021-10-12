using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Doozy.Engine.UI;

public class swipeMenu : MonoBehaviour
{
    public GameObject scrollbar;
    public UIView[] infoViews;
    public GameObject[] stages;
    private float scroll_pos = 0;
    float[] pos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
            //Debug.Log("Mouse being pressed");
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }


        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                //Debug.LogWarning("Current Selected Level" + i);
                stages[i].transform.localScale = Vector2.Lerp(stages[i].transform.localScale, new Vector2(1.2f, 1.2f), 0.1f);
                infoViews[i].Show();
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        stages[j].transform.localScale = Vector2.Lerp(stages[j].transform.localScale, new Vector2(1f, 1f), 0.1f);
                        infoViews[j].Hide();
                    }
                }
            }
        }

    }
}