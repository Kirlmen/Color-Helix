using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDisplay : MonoBehaviour
{
    private TextMesh textMesh;

    // Start is called before the first frame update
    void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, BallHandler.GetZ());
        Destroy(gameObject, 1.2f);

    }

    public void SetText(string text)
    {
        this.textMesh.text = text;
        textMesh.color = Color.white;
    }
}
