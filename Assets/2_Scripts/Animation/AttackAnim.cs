using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackAnim : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;
    private Image image;
    private float time;
    private bool isParticle;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.transform.GetChild(0).GetComponent<Image>();
        time = 0;
        isParticle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isParticle)
        {
            isParticle = true;
            particle.GetComponent<ParticleControl>().tempTime = 0.6f;
            particle.SetActive(true);
        }
        if (time > 0.6f)
        {
            time = 0;
            image.color = new Color(1, 1, 1, 1);
            isParticle = false;
            gameObject.SetActive(false);
        }
        image.color = new Color(1, 1, 1, 1 - time);
        time += Time.deltaTime;
    }
}
