using UnityEngine;

public class GuideScript : MonoBehaviour
{
    public GameObject Guide;
    public float offsetDistance = 2f; // ระยะห่างด้านหน้า
    public GameObject FinishState;

    private GameObject Player;

    private void Awake()
    {
        Guide.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector3 offset = Player.transform.forward * offsetDistance;

        Guide.transform.position = Player.transform.position + offset;

        Guide.transform.LookAt(FinishState.transform.position);
    }
}
