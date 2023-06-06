
using UnityEngine;
using UnityEngine.UI;

public class ItemDrop: MonoBehaviour
{
    public Image imgDisplay;
    [SerializeField] ItemsHolder holder;
    [SerializeField] float rotateSpeed;
    [SerializeField] Rigidbody rigid;
    [SerializeField] bool canPick;
    private void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed*Time.deltaTime);
    }
    public void SetHolder(ItemsHolder holder) => this.holder = holder;
    public void Spawn(Sprite avatar, float force)
    {
        // set image display
        if(imgDisplay != null) imgDisplay.sprite = avatar;
        // throw item
        float xDir = Random.Range(-1, 1f);
        float zDir = Random.Range(-1, 1f);
        Vector3 directionRandom = new Vector3(xDir, 1f, zDir).normalized; 
        rigid.AddForce(directionRandom * force);
    }
    public ItemsHolder Pick()
    {
        if (!canPick) return null;
        else
        {
            Destroy(gameObject);
            return holder;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6) // collie with walkable layer
        {
            canPick = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6) // out collie with walkable layer
        {
            canPick = false;
        }
    }
}