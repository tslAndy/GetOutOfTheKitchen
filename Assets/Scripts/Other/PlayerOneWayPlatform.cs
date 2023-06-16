using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{
    [HideInInspector] public GameObject OneWayPlatform;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("OneWayPlatform"))    
        {
            OneWayPlatform = collision.gameObject; 
        } 
    }

    public IEnumerator DisableCollision(Collider2D playerCollider)
    {
        if(OneWayPlatform != null)
        {
            Collider2D collision = OneWayPlatform.gameObject.GetComponent<Collider2D>();           
            Physics2D.IgnoreCollision(playerCollider, collision);    //    игнорировать столкновение
            yield return new WaitForSeconds(0.6f);                       //        подождать 0.6 секунд
            OneWayPlatform = null;                                            //        обнулить наличие проходимой платформы под ногами игрока
            Physics2D.IgnoreCollision(playerCollider, collision, false);          //         возобновить возсожность столкновения
            Debug.Log("Done");
        }
       
    }
}
