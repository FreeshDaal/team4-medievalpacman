using UnityEngine;

public class GhostScatter : GhostBehaviour
{
    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Nodes nodes = other.GetComponent<Nodes>();

        if (nodes != null && this.enabled && !this.ghost.frightened.enabled)
        {
            int index = Random.Range(0, nodes.availableDirections.Count);

            if (nodes.availableDirections[index] == -this.ghost.movement.direction && nodes.availableDirections.Count > 1)
            {
                index++;

                if (index >= nodes.availableDirections.Count)
                {
                    index = 0;
                }
            }

            this.ghost.movement.SetDirection(nodes.availableDirections[index]);
        }
    }
}
