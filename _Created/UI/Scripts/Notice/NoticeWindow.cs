using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoticeWindow : MonoBehaviour
{
    [SerializeField] GameObject noticeForm;
    [SerializeField] float timeDisplayForm;
    float _timeFormInQueue;
    [SerializeField] Transform contentHolder;
    public Queue<GameObject> poolForms = new Queue<GameObject>();
    private void Awake()
    {
        _timeFormInQueue = timeDisplayForm + 3f; // always in queue after 3 seconds if don't used
    }
    public void AddNotice(Notice newNotice)
    {
        // setup form
        GameObject form = GetForm();

        // set info
        NoticeForm ctrl = form.GetComponent<NoticeForm>();
        ctrl.Setup(newNotice);
    }
    IEnumerator SetTimeDisplayForm(GameObject form)
    {
        yield return new WaitForSeconds(timeDisplayForm);

        // add new form into pool
        poolForms.Enqueue(form);
        form.SetActive(false);

        StartCoroutine(SetTimeFormInQueue());
    }
    IEnumerator SetTimeFormInQueue()
    {
        yield return new WaitForSeconds(_timeFormInQueue);

        // destroy if this form not used
        if(poolForms.Count > 0)
        {
            GameObject form = poolForms.Dequeue();
            if(!form.activeSelf) Destroy(form);
        }
    }
    private GameObject GetForm()
    {
        GameObject form = null;
        if (poolForms.Count > 0)
        {
            form = poolForms.Dequeue();
        }
        else
        {
            form = Instantiate(noticeForm, contentHolder);
        }
        form.SetActive(true);
        StartCoroutine(SetTimeDisplayForm(form));

        return form;
    }
}
