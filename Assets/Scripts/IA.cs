using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IA : MonoBehaviour
{
    public Text scoreText;
    public int capas = 5;
    public int neuronas = 10;
    public Matriz[] pesos;
    public Matriz[] biases;
    Matriz entradas;
    float acceleration;
    float accelerationV; // Aceleración vertical
    float rotation;
    public float score;
    bool lost = false;

    // Fitness variables
    private Vector3 lastPosition;
    private float distanceTraveled = 0;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        lastPosition = transform.position;
    }

    public void Initialize()
    {
        pesos = new Matriz[capas];
        biases = new Matriz[capas];
        entradas = new Matriz(1, 5);

        for (int i = 0; i < capas; i++)
        {
            if (i == 0)
            {
                pesos[i] = new Matriz(5, neuronas);
                pesos[i].RandomInitialize();
                biases[i] = new Matriz(1, neuronas);
                biases[i].RandomInitialize();
            }
            else if (i == capas - 1)
            {
                pesos[i] = new Matriz(neuronas, 3);
                pesos[i].RandomInitialize();
                biases[i] = new Matriz(1, 3);
                biases[i].RandomInitialize();
            }
            else
            {
                pesos[i] = new Matriz(neuronas, neuronas);
                pesos[i].RandomInitialize();
                biases[i] = new Matriz(1, neuronas);
                biases[i].RandomInitialize();
            }
        }
    }

    void Update()
    {
        if (lost || Time.timeScale == 0)
            return;
    
        // Obtener las distancias del drone usando los raycasts
        float FD = GetComponent<Drone>().ForwardDistance;
        float RD = GetComponent<Drone>().RightDistance;
        float LD = GetComponent<Drone>().LeftDistance;
        float UD = GetComponent<Drone>().UpDistance;
        float DD = GetComponent<Drone>().DownDistance;
    
        // Establecer las entradas a la red neuronal
        entradas.SetAt(0, 0, FD);
        entradas.SetAt(0, 1, RD);
        entradas.SetAt(0, 2, LD);
        entradas.SetAt(0, 3, UD);
        entradas.SetAt(0, 4, DD);
    
        // Resolver la red neuronal para obtener el movimiento
        Resolve();
    
        // Movimiento: adelante, hacia arriba, y rotación (izquierda/derecha)
        // Aplicar movimiento en base a las salidas de la IA
        Vector3 moveDirection = Vector3.zero;
    
        moveDirection += Vector3.forward * acceleration;//aceleración
        moveDirection += Vector3.up * accelerationV;//altitud
        transform.eulerAngles += new Vector3(0, rotation * 90 * Time.deltaTime* Time.timeScale, 0);//rotación
    
        // Aplicar movimiento con la velocidad ajustada por el tiempo
        transform.Translate(moveDirection * Time.deltaTime* Time.timeScale);
    
        // Calcular la distancia recorrida y actualizar la puntuación
        distanceTraveled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        SetScore();
    }
    
    void Resolve()
    {
        Matriz result = Activation((entradas * pesos[0]) + biases[0]);
        for (int i = 1; i < capas; i++)
        {
            result = Activation((result * pesos[i]) + biases[i]);
        }
        ActivationLast(result);
    }

    Matriz Activation(Matriz m)
    {
        for (int i = 0; i < m.rows; i++)
        {
            for (int j = 0; j < m.columns; j++)
            {
                m.SetAt(i, j, (float)MathL.HyperbolicTangtent(m.GetAt(i, j)));
            }
        }
        return m;
    }

    void ActivationLast(Matriz m)
    {
        if (m.columns >= 3)
        {
            rotation = (float)MathL.HyperbolicTangtent(m.GetAt(0, 0));
            acceleration = MathL.Sigmoid(m.GetAt(0, 1));
            accelerationV = (float)MathL.HyperbolicTangtent(m.GetAt(0, 2));
        }
    }

    void SetScore()
    {
        float FD = GetComponent<Drone>().ForwardDistance;
        float RD = GetComponent<Drone>().RightDistance;
        float LD = GetComponent<Drone>().LeftDistance;
        float UD = GetComponent<Drone>().UpDistance;
        float DD = GetComponent<Drone>().DownDistance;

        float avgDistance = ((3 * FD) + (2 * (UD + DD)) + RD + LD) / 5;
        score += avgDistance + distanceTraveled;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") || (other.CompareTag("CAR") && Time.deltaTime >= 5))
        {
            lost = true;
            Genetics.DroneAlive--;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CAR") && Time.deltaTime >= 5)
        {
            lost = true;
            Genetics.DroneAlive--;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CAR") && Time.deltaTime >= 5)
        {
            lost = true;
            Genetics.DroneAlive--;
        }
    }
}