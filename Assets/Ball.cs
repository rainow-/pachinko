using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bottle"))
        {
            GameObject[] bottles = GameObject.FindGameObjectsWithTag("Bottle");

            var targetType = collision.gameObject.GetComponent<Bottle>().bottleType == Bottle.Type.Floor ? Bottle.Type.Roof : Bottle.Type.Floor;

            List<GameObject> targetBottles = new List<GameObject>();
            foreach (GameObject bottle in bottles)
            {
                if (bottle.GetComponent<Bottle>()?.bottleType == targetType)
                {
                    targetBottles.Add(bottle);
                }
            }

            if (targetBottles.Count > 0)
            {
                int randomIndex = Random.Range(0, targetBottles.Count);
                GameObject randomTargetBottle = targetBottles[randomIndex];

                Vector3 bottlePosition = randomTargetBottle.transform.position;
                bottlePosition.y = targetType == Bottle.Type.Floor ? bottlePosition.y + 1f : bottlePosition.y - 1f;

                transform.position = bottlePosition;

                 Vector2 collisionNormal = collision.contacts[0].normal;

                // Calculate the new velocity by reflecting the current velocity across the collision normal
                Vector2 newVelocity = Vector2.Reflect(gameObject.GetComponent<Rigidbody2D>().velocity, collisionNormal);

                // Set the new velocity
                gameObject.GetComponent<Rigidbody2D>().velocity = newVelocity;
            }
        }
    }
}
