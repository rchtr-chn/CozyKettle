using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float speed;

    private void Update()
    {
        speed = Random.Range(20f, 60f);

        transform.Translate(Vector3.left * Time.deltaTime * speed);
        if (transform.position.x < -2500f)
        {
            Destroy(gameObject);
        }
    }
}
