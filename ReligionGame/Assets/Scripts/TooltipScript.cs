using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOver = false;
    public GameObject tooltipPrefab;
    GameObject tooltipInstance;
    float timeCounter=0f;
    [SerializeField]
    [Multiline]
    string shortTooltipText;
    [SerializeField]
    [Multiline]
    string longTooltipText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
    }

    void Start()
    {
        Debug.Log("start tooltip script");
        tooltipInstance = GameObject.Instantiate(tooltipPrefab, this.transform.position, Quaternion.identity) as GameObject;
        tooltipInstance.transform.SetParent(GameObject.Find("Canvas").transform);
        tooltipInstance.transform.localScale = Vector3.one;
        tooltipInstance.transform.localPosition = this.transform.localPosition;
        tooltipInstance.GetComponent<Text>().text = shortTooltipText;
        tooltipInstance.SetActive(false);
    }

    void Update()
    {
        if(isOver)
        {
            Debug.Log("This object is being moused over.");
            tooltipInstance.SetActive(true);
            timeCounter += Time.deltaTime;
        }
        else
        {
            tooltipInstance.GetComponent<Text>().text = shortTooltipText;
            tooltipInstance.SetActive(false);
            timeCounter = 0f;
        }

        if (timeCounter>3f)
        {
            tooltipInstance.GetComponent<Text>().text = longTooltipText;
        }
    }

}
