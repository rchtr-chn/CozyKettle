using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CustomerManager : MonoBehaviour
{
    public GameObject customerPrefab;
    public RectTransform customerEntrancePos;
    public RectTransform customerExitPos;
    public List<GameObject> customerSeats;

    void Update()
    {
        foreach(GameObject seat in customerSeats)
        {
            //Debug.Log(seat);
            if(seat.transform.childCount == 0)
            {
                Debug.Log(seat);
                GameObject customer = Instantiate(customerPrefab, customerEntrancePos.position, Quaternion.identity, seat.transform);
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

        yield return null;
    }
}
