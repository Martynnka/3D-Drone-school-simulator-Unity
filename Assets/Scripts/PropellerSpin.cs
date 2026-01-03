using UnityEngine;

public class PropellerSpin : MonoBehaviour
{
    public float spinSpeed = 3000f;

    void Update()
    {
        // SUKIMAS APLINK Z AŠĮ
        transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime, Space.Self);
    }
}
