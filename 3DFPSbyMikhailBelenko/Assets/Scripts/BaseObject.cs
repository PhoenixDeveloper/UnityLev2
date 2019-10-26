using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Базовый класс для всех объектов на сцене, кэширующий ссылки
/// </summary>
public abstract class BaseObject : MonoBehaviour
{
    protected Transform _GOTransform;
    protected GameObject _GOInstance;
    protected string _name;
    protected bool _isVisible;

    protected Vector3 _position;
    protected Vector3 _scale;
    protected Quaternion _rotation;

    protected Material _material;
    protected Color _color;

    protected Rigidbody _rigidbody;

    protected Camera _mainCamera;
    protected Canvas _canvasMainCamera;

    protected Animator _animator;

    #region UnityFunctions
    protected virtual void Awake()
    {
        _GOInstance = gameObject;
        _GOTransform = gameObject.transform;
        _name = gameObject.name;

        _mainCamera = Camera.main;
        _canvasMainCamera = Camera.main.GetComponentInChildren<Canvas>();

        if (GetComponent<Rigidbody>())
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        if (GetComponent<Animator>())
        {
            _animator = GetComponent<Animator>();
        }

        if (GetComponent<Renderer>())
        {
            _material = GetComponent<Renderer>().material;
            _color = GetComponent<Renderer>().material.color;
        }
    }
    #endregion

    /// <summary>
    /// Ссылка на объект
    /// </summary>
    public GameObject InstanceObject
    {
        get { return _GOInstance; }
    }

    /// <summary>
    /// Имя объекта
    /// </summary>
    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
            InstanceObject.name = _name;
        }
    }

    /// <summary>
    /// Видимость объекта
    /// </summary>
    public bool IsVisible
    {
        get
        {
            return _isVisible;
        }

        set
        {
            _isVisible = value;
            if (_GOInstance.GetComponent<Renderer>())
            {
                _GOInstance.GetComponent<Renderer>().enabled = _isVisible;
            }
        }
    }

    /// <summary>
    /// Позиция объекта на сцене
    /// </summary>
    public Vector3 Position
    {
        get
        {
            if(_GOInstance)
            {
                _position = _GOTransform.position;
            }
            return _position;
        }

        set
        {
            _position = value;
            if(_GOInstance)
            {
                _GOTransform.position = _position;
            }
        }
    }

    /// <summary>
    /// Размер объекта
    /// </summary>
    public Vector3 Scale
    {
        get
        {
            if (_GOInstance)
            {
                _scale = _GOTransform.localScale;
            }
            return _scale;
        }

        set
        {
            _scale = value;
            if (_GOInstance)
            {
                _GOTransform.localScale = _scale;
            }
        }
    }

    /// <summary>
    /// Поворот объекта в пространстве
    /// </summary>
    public Quaternion Rotation
    {
        get
        {
            if (_GOInstance)
            {
                _rotation = _GOTransform.rotation;
            }
            return _rotation;
        }

        set
        {
            _rotation = value;
            if (_GOInstance)
            {
                _GOTransform.rotation = _rotation;
            }
        }
    }

    /// <summary>
    /// Получить материал объекта
    /// </summary>
    public Material GetMaterial
    {
        get { return _material; }
    }

    /// <summary>
    /// Получить цвет объекта
    /// </summary>
    public Color GetColor
    {
        get { return _color; }
    }

    /// <summary>
    /// Получить Rigidbody объекта
    /// </summary>
    public Rigidbody GetRigidbody
    {
        get { return _rigidbody; }
    }

    /// <summary>
    /// Получить Animator объекта
    /// </summary>
    public Animator Animator
    {
        get { return _animator; }
    }

    /// <summary>
    /// Возвращает камеру прикрепленную к игроку
    /// </summary>
    public Camera MainCamera
    {
        get { return _mainCamera; }
    }

    /// <summary>
    /// Возвращает количество дочерних объектов
    /// </summary>
    public int ChildCount
    {
        get { return _GOTransform.childCount; }
    }
}
