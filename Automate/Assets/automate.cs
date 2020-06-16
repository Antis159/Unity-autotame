using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using UnityEngine.Rendering.PostProcessing;

public class automate : MonoBehaviour
{
    public GameObject spherePrefab;
    public GameObject particles;
    public GameObject mainCube;
    public Mesh sphere;
    public Mesh cube;
    Slider helth;
    public Terrain terrain;
    public static EditorWindow GetMainGameView()
    {
        System.Reflection.Assembly assembly = typeof(UnityEditor.EditorWindow).Assembly;
        Type type = assembly.GetType("UnityEditor.GameView");
        EditorWindow gameview = EditorWindow.GetWindow(type);
        return gameview;
    }
    public static EditorWindow GetSceneView()
    {
        System.Reflection.Assembly assembly = typeof(UnityEditor.EditorWindow).Assembly;
        Type type = assembly.GetType("UnityEditor.SceneView");
        EditorWindow sceneview = EditorWindow.GetWindow(type);
        return sceneview;
    }

    /*public class Window : EditorWindow
    {
        [MenuItem("Window/myWindow")]
        public static void showThisWindow ()
        {
            //Window window = EditorWindow.GetWindow<Window>();
            GetWindow<SceneView>().ShowNotification(new GUIContent("bruh"));
            //EditorWindow.ShowNotification(new GUIContent("Skuma"));
        }
    }*/
    void Start()
    {
        helth = FindObjectOfType<Slider>();
        helth.gameObject.SetActive(false);
        StartCoroutine(Move());
    }


    void Update()
    {

    }

    IEnumerator Move()
    {
        while (true)
        {
            Vector3 position = transform.position;
            if (position.z < 1 && position.x <= 0)
            {
                position.z += 1 * Time.deltaTime;
                position.x = 0;
            }
            else if (position.z >= 1 && position.x < 1)
            {
                position.z = 1;
                position.x += 1 * Time.deltaTime;
            }
            else if (position.x >= 1 && position.z > 0)
            {
                position.x = 1;
                position.z -= 1 * Time.deltaTime;
            }
            else if (position.z <= 0 && position.x > 0)
            {
                position.z = 0;
                position.x -= 1 * Time.deltaTime;
            }

            transform.position = position;
            if (position.x <= 0 && position.z <= 0)
            {
                StartCoroutine(ColorChange());
                break;
            }
            yield return null;
        }
    }

    IEnumerator ColorChange()
    {
        float startTime = Time.time;
        while (true)
        {
            MeshRenderer ColorInst = GetComponent<MeshRenderer>();
            float h = Time.time % 1;
            float s = 1;
            float v = 1;
            Color bruh = Color.HSVToRGB(h, s, v);
            ColorInst.material.color = bruh;
            if (Time.time >= startTime + 2)
            {
                bruh = new Color(1, 1, 1, 1);
                ColorInst.material.color = bruh;
                StartCoroutine(Particles());
                break;
            }
            yield return null;
        }
    }

    IEnumerator Particles()
    {
        GameObject particleInst = Instantiate(particles, transform.position, Quaternion.Euler(-90, 0, 0));
        yield return null;
    }

    public IEnumerator HealthBar()
    {
        while (true)
        {
            helth.gameObject.SetActive(true);
            helth.value += Time.time % 1;
            if (helth.value >= 10)
            {
                StartCoroutine(tyxt());
                helth.gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
    }

    IEnumerator tyxt()
    {
        string asd = "Phasellus maximus";
        TMPro.TextMeshProUGUI tekst = FindObjectOfType<TMPro.TextMeshProUGUI>();
        for (int i = 0; i < asd.Length; i++)
        {
            tekst.text += asd[i];
            yield return new WaitForSeconds(.1f);
        }
        tekst.text = "";
        StartCoroutine(shape());
    }

    IEnumerator shape ()
    {
        GetMainGameView().ShowNotification(new GUIContent("Cheese and a large soda"));
        GetSceneView().ShowNotification(new GUIContent("Cheese and a large soda"));
        yield return new WaitForSeconds(1.5f);

        MeshFilter mesh = gameObject.GetComponent<MeshFilter>();
        for (int i=0;i<15;i++)
        {
            mesh.mesh = sphere;
            yield return new WaitForSeconds(.1f);
            mesh.mesh = cube;
            yield return new WaitForSeconds(.1f);
        }
        StartCoroutine(fizik());
    }

    IEnumerator fizik()
    {
        Terrain terr = Instantiate(terrain, transform.position + new Vector3(-10, -10, -10), Quaternion.identity);
        mainCube.GetComponent<Rigidbody>().useGravity = true;
        for (int i=0; i<10; i++)
        {
            Instantiate(spherePrefab, transform.position + new Vector3(Random.Range(-10, 10), 10, Random.Range(-10, 10)), Quaternion.identity);
            yield return new WaitForSeconds(.2f);
        }
        Physics.gravity = new Vector3(0, 1, 0);

        yield return null;
        StartCoroutine(pp());
    }

    IEnumerator pp()
    {
        PostProcessVolume ppVolume;
        Vignette vignette;

        vignette = ScriptableObject.CreateInstance<Vignette>();
        vignette.enabled.Override(true);
        vignette.intensity.Override(0.1f);
        ppVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignette);
        
        for (int i=0; i<100;i++)
        {
            vignette.intensity.value = Mathf.Pow(Time.time/20, 3);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
