using System;
using MagicLeap;
using MagicLeap.DesignToolkit.Actions;
using MagicLeap.LeapBrush;
using UnityEngine;

public class ShapeChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject _cube;

    [SerializeField]
    private GameObject _sphere;

    [SerializeField]
    private GameObject _capsule;

    private ShapeChangerProto.Types.Shape _shape = ShapeChangerProto.Types.Shape.Cube;
    private External3DModel _external3dModel;

    public void SetShape(ShapeChangerProto.Types.Shape shape)
    {
        if (_shape == shape)
        {
            return;
        }

        _shape = shape;

        _cube.SetActive(_shape == ShapeChangerProto.Types.Shape.Cube);
        _sphere.SetActive(_shape == ShapeChangerProto.Types.Shape.Sphere);
        _capsule.SetActive(_shape == ShapeChangerProto.Types.Shape.Capsule);

        _external3dModel.SetShapeChangerData(new ShapeChangerProto
        {
            Shape = _shape
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started");

        _external3dModel = GetComponentInParent<External3DModel>();

        _external3dModel.GetComponent<Interactable>().Events.OnGrab.AddListener(OnGrab);
    }

    private void OnGrab(Interactor _)
    {
        Debug.Log("Grabbed");

        switch (_shape)
        {
            case ShapeChangerProto.Types.Shape.Cube:
                SetShape(ShapeChangerProto.Types.Shape.Sphere);
                return;
            case ShapeChangerProto.Types.Shape.Sphere:
                SetShape(ShapeChangerProto.Types.Shape.Capsule);
                break;
            case ShapeChangerProto.Types.Shape.Capsule:
                SetShape(ShapeChangerProto.Types.Shape.Cube);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
