using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//librerías para insertar imagen de StreamingAssets
using UnityEngine.UI;
using System.IO;

public class Ordenar : MonoBehaviour
{
    public Transform[] cuadroFragmento;
    [SerializeField]
    Vector3[] positions;
    Vector3[] rotations;
    public Vector3[] finalRotation;
    public Vector3[] finalPosition;
    public List<Texture> texturas = new List<Texture>();

    //Texture2D fotoTexture = Resources.Load<Texture2D>("fotos/001");
    public int currentTexture;

    private void Awake()
    {
        positions = new Vector3[cuadroFragmento.Length];
        rotations = new Vector3[cuadroFragmento.Length];
        //finalRotation = new Vector3[cuadroFragmento.Length];
        //finalPosition = new Vector3[cuadroFragmento.Length];

        for (int i = 0; i < cuadroFragmento.Length; i++)
        {
            positions[i] = cuadroFragmento[i].position;
            rotations[i] = cuadroFragmento[i].rotation.eulerAngles;
        }
    }

    IEnumerator LoadCuadro(FileInfo fotoFile)
    {
        //1
        if (fotoFile.Name.Contains("meta"))
        {
            yield break;
        }
        //2
        else
        {
            //string fotoFileWithoutExtension = Path.GetFileNameWithoutExtension(fotoFile.ToString());
            //string[] playerNameData = fotoFileWithoutExtension.Split(" "[0]);
            ////3
            //string tempFotoName = "";
            //int i = 0;
            //foreach (string stringFromFileName in playerNameData)
            //{
            //    if (i != 0)
            //    {
            //        tempFotoName = tempFotoName + stringFromFileName + " ";
            //    }
            //    i++;
            //}
            //4
            string wwwPlayerFilePath = "file://" + fotoFile.FullName.ToString();
            WWW www = new WWW(wwwPlayerFilePath);
            yield return www;
            //5
            //cuadroFragmentoFoto.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
            texturas.Add(www.texture);
        }
    }

    private void Start()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        print("Streaming Assets Path: " + Application.streamingAssetsPath);
        FileInfo[] allFiles = directoryInfo.GetFiles("*.*");

        foreach (FileInfo file in allFiles)
        {
            Debug.Log(file.FullName);
            if (file.Name.Contains("foto"))
            {
                StartCoroutine("LoadCuadro", file);
            }
        }

        for (int i = 0; i < cuadroFragmento.Length; i++)
        {
            cuadroFragmento[i].localPosition = finalPosition[i];
            cuadroFragmento[i].localEulerAngles = finalRotation[i];
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("trigger enter: " + other.name);
            OrdenarCuadros();
        }   
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("trigger exit: " + other.name);
            Desordenar();
        }
    }
    void OrdenarCuadros()
    {
        bool ultaFotoExist = false;
        for (int i = 0; i < cuadroFragmento.Length; i++)
        {
            cuadroFragmento[i].GetComponent<Rigidbody>().isKinematic = true;
            cuadroFragmento[i].LeanMove( positions[i],0.5f);
            cuadroFragmento[i].LeanRotate( rotations[i], 1.0f);

            currentTexture++;
            currentTexture %= texturas.Count;
            if (texturas[currentTexture] == texturas[texturas.Count - 1]) ultaFotoExist = true;
            cuadroFragmento[i].GetComponent<Renderer>().material.mainTexture = texturas[currentTexture];  
            //cuadroFragmento[i].GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture2D>("fotos/00"+(i+1));
        }
        if(!ultaFotoExist)cuadroFragmento[1].GetComponent<Renderer>().material.mainTexture = texturas[texturas.Count-1];
    }
    void Desordenar()
    {
        for (int i = 0; i < cuadroFragmento.Length; i++)
        {
            Rigidbody rb = cuadroFragmento[i].GetComponent<Rigidbody>();
            rb.isKinematic = false;
            //rb.AddForce(new Vector3(Random.Range(-1,1), Random.Range(-1, 1), Random.Range(-1, 1)) * Random.Range(10f,100f) * Time.deltaTime, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        //for (int i = 0; i < cuadroFragmento.Length; i++)
        //{
        //    finalRotation[i] = cuadroFragmento[i].localRotation.eulerAngles;
        //    finalPosition[i] = cuadroFragmento[i].localPosition;
        //}   
    }
}
