using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float rotateSpeed = 2.0f;

    [SerializeField] private Material red;
    [SerializeField] private Material blue;

    private int color = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (color == 0)
            {
                color = 1;
            }
            else
            {
                color = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject newBullet = Instantiate(bullet);

            newBullet.transform.position = transform.position;
            newBullet.transform.rotation = transform.rotation;

            MeshRenderer meshRenderer = newBullet.GetComponentsInChildren<MeshRenderer>()[0];

            if (color == 1)
            {
                meshRenderer.sharedMaterial = red;
            }
            if (color == 0)
            {
                meshRenderer.sharedMaterial = blue;
            }
        }

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Rotate(new Vector3(0.0f, 90.0f * rotateSpeed * Time.deltaTime, 0.0f));
        //}

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Rotate(new Vector3(0.0f, -90.0f * rotateSpeed * Time.deltaTime, 0.0f));
        //}

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.Rotate(new Vector3(-90.0f * rotateSpeed * Time.deltaTime, 0.0f, 0.0f));
        //}

        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.Rotate(new Vector3(90.0f * rotateSpeed * Time.deltaTime, 0.0f, 0.0f));
        //}

        Vector3 direction = (GameObject.Find("Monster").transform.position - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = targetRotation;


    }
}
