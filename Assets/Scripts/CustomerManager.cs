using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject CustomerPrefab;
    public RectTransform CustomerEntrancePos;
    public RectTransform CustomerExitPos;
    public List<GameObject> CustomerSeats;

    void Update()
    {
        foreach(GameObject seat in CustomerSeats)
        {
            //Debug.Log(seat);
            if(seat.transform.childCount == 0)
            {
                Debug.Log(seat);
                GameObject customer = Instantiate(CustomerPrefab, CustomerEntrancePos.position, Quaternion.identity, seat.transform);
                float randSpeed = Random.Range(1f, 5f);
                StartCoroutine(MoveToSeat(customer, seat, randSpeed));
            }
        }
    }

    IEnumerator MoveToSeat(GameObject customer, GameObject target, float speed)
    {
        while(customer.transform.position != target.transform.position)
        {
            customer.transform.position = Vector3.MoveTowards(customer.transform.position, target.transform.position, speed * 500f * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        Customer custScript = customer.GetComponent<Customer>();
        custScript.PromptRequest();

        yield return null;
    }
}
