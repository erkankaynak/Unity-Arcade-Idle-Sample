using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 1f;
    

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject backPack;

    private List<GameObject> stackCubes = new List<GameObject>();

    private Rigidbody rb;
    private bool isOnDropArea = false;
    private bool isOnAir = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.rotation = Quaternion.AngleAxis(-90f, Vector3.up);
            transform.position = transform.position  + Vector3.left * speed * Time.deltaTime;   
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.AngleAxis(90f, Vector3.up);
            transform.position = transform.position + Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.AngleAxis(0f, Vector3.up);
            transform.position = transform.position + Vector3.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.AngleAxis(180f, Vector3.up);
            transform.position = transform.position + Vector3.back * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) )
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnAir = true;
        }

        if (transform.position.y <= -2f)
        {
            Destroy(gameObject);
            GameManager.Instance.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            Destroy(collision.gameObject);

            int stackCount = stackCubes.Count;

            var newCube = Instantiate(cubePrefab);
            Destroy(newCube.GetComponent<BoxCollider>());

            newCube.transform.SetParent(backPack.transform);
            newCube.transform.localScale = new Vector3(.5f, .125f, .5f);
            newCube.transform.localRotation = Quaternion.identity;
            newCube.transform.localPosition = new Vector3(0, stackCount * .3f, 0f);

            stackCubes.Add(newCube);
        }

        if (collision.gameObject.CompareTag("Plane"))
        {
            isOnAir = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BridgeOpener"))
        {
            isOnDropArea = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("BridgeOpener"))
        {
            var bridgeOpener = other.gameObject.GetComponent<Opener>();

            if (!isOnDropArea)
            {
                isOnDropArea = true;
                if (stackCubes.Count > 0 && bridgeOpener.cost > 0) StartCoroutine(DropStacks(bridgeOpener));
            }
        }
    }

    IEnumerator DropStacks(Opener opener)
    {
        while (isOnDropArea && stackCubes.Count > 0 && opener.cost > 0)
        {
            var lastIndex = stackCubes.Count - 1;

            var cubePos = stackCubes[lastIndex].transform.position;
            var flyCube = stackCubes[lastIndex];

            stackCubes.RemoveAt(lastIndex);

            flyCube.GetComponent<Fly>().FlyTo(opener);
           

            yield return new WaitForSeconds(.3f);
        }

        isOnDropArea = false;
        yield return null;
    }

}
