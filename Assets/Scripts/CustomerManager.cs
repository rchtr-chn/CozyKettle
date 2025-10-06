using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject CustomerPrefab;
    public RectTransform CustomerEntrancePos;
    public RectTransform CustomerExitPos;
    public List<GameObject> CustomerSeats;
    private float speed = 2f;

    [SerializeField] private Beverage[] _availableBeverages;
    protected static Dictionary<Beverage, string> _beverageToRequestText;

    private void Awake()
    {
        if (_availableBeverages != null)
        {
            _availableBeverages = Resources.LoadAll<Beverage>("OBJs/Beverages");
        }
    }

    void Start()
    {
        _beverageToRequestText = new() {
            { _availableBeverages[0], "Bring me a strong, robust cup—bold and full-bodied, the kind that warms you right up" }, // black tea
            { _availableBeverages[1], "Bring me something deep and robust, but sweetened with a comforting warmth." }, // black tea + honey
            { _availableBeverages[2], "I’d like something bold but with a bright, citrus edge to cut through the heaviness." }, // black tea + lemon
            { _availableBeverages[3], "I’m craving a strong, bold cup that’s softened by a rich, creamy finish." }, // black tea + milk
            { _availableBeverages[4], "I’d like something light and refreshing, with a gentle grassy note that feels pure and clean." }, // green tea
            { _availableBeverages[5], "I want something refreshing yet sweet, like a spring breeze with a golden sweetness." }, // green tea + honey
            { _availableBeverages[6], "Give me something sharp and zesty, but still clean and light—like a splash of sunshine." }, // green tea + lemon
            { _availableBeverages[7], "Something smooth and calming, a little grassy but mellowed out with a creamy touch." }, // green tea + milk
            { _availableBeverages[8], "Give me something earthy and energizing, with that deep, grassy kick of pure matcha." }, // matcha tea
            { _availableBeverages[9], "Bring me something earthy but sweet, like a meadow dusted with golden nectar." }, // matcha tea + honey
            { _availableBeverages[10], "Give me something bold and grassy, but with a sharp citrus twist to wake me up." }, // matcha tea + lemon
            { _availableBeverages[11], "I’d like something rich and velvety, with that earthy matcha flavor smoothed out by cream." }, // matcha tea + milk
            { _availableBeverages[12], "I want something smooth and floral, balanced between richness and lightness." }, // oolong tea
            { _availableBeverages[13], "Give me something balanced—floral and nutty, with a soft, natural sweetness." }, // oolong tea + honey
            { _availableBeverages[14], "I’m in the mood for something fragrant and light, with a little citrus sparkle." }, // oolong tea + lemon
            { _availableBeverages[15], "I want a drink that’s smooth and floral, but grounded with a silky creaminess." }, // oolong tea + milk
        };

        GetCustomers();
    }

    void GetCustomers()
    {
        foreach (GameObject seat in CustomerSeats)
        {
            if (seat.transform.childCount == 0)
            {
                // Seat is empty, spawn a customer
                GameObject customer = Instantiate(CustomerPrefab, CustomerEntrancePos.position, Quaternion.identity, seat.transform);

                // Assign a random beverage request
                Customer custScript = customer.GetComponent<Customer>();
                custScript.CustomerManager = this;
                Beverage beverage = GetRequest(_availableBeverages);
                custScript.BeverageRequested = beverage;
                custScript.SpeechBubbleText = _beverageToRequestText[beverage];

                float randSpeed = Random.Range(1f, 5f);
                StartCoroutine(MoveToSeat(customer, seat, randSpeed));
            }
        }
    }
    Beverage GetRequest(Beverage[] availableBevs)
    {
        int rand = Random.Range(0, availableBevs.Length - 1);
        return availableBevs[rand];
    }

    IEnumerator MoveToSeat(GameObject customer, GameObject target, float speed)
    {
        while (customer.transform.position != target.transform.position)
        {
            customer.transform.position = Vector3.MoveTowards(customer.transform.position, target.transform.position, speed * 500f * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        Customer custScript = customer.GetComponent<Customer>();
        custScript.PromptRequest();

        yield return null;
    }

    public IEnumerator WalkUpAndLeave(GameObject customer)
    {
        while (customer.transform.position != CustomerExitPos.position)
        {
            customer.transform.position = Vector3.MoveTowards(customer.transform.position, CustomerExitPos.position, speed * 500f * Time.deltaTime);
            yield return null;
        }
        Destroy(customer);
        GetCustomers();
        yield return null;
    }

}
