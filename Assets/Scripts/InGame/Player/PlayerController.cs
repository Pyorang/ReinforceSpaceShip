using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private bool fightMode = false;

    [SerializeField] private CommonMapDatas commonMapData;
    [SerializeField] private Movement2D moveMent2D;
    [SerializeField] private Joystick joystick;

    private void Update()
    {
        if (!fightMode)
            moveMent2D.MoveTo(new Vector3(joystick.Horizontal, 0, 0));
        else
            moveMent2D.MoveTo(new Vector3(joystick.Horizontal, joystick.Vertical, 0));
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, commonMapData.LimitMin.x, commonMapData.LimitMax.x), Mathf.Clamp(transform.position.y, commonMapData.LimitMin.y, commonMapData.LimitMax.y));
    }

    public void ChangeAxisOptionsToBoth()
    {
        fightMode = true;
        joystick.AxisOptions = AxisOptions.Both;
    }
}
