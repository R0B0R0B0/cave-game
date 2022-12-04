using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using TMPro;

public class DialogueNodeParser : MonoBehaviour
{
    public DialogueGraph graph;
    public TextMeshProUGUI speaker;
    public TextMeshProUGUI dialogue;
    public Image speakerImage;

    Coroutine _parser;

    //This script does all of the logic

    void Start()
    {
        foreach (DialogueBaseNode node in graph.nodes)
        {
            if(node.GetString() == "Start")
            {
                graph.current = node;
                break;
            }
        }

        _parser = StartCoroutine(ParseNode());
    }

    IEnumerator ParseNode()
    {
        Debug.Log("Parsing nodes ...");
        DialogueBaseNode node = graph.current;
        string data = node.GetString();
        string[] dataParts = data.Split("/");
        //This is where the logic happends
        switch(dataParts[0])
        {
            case "Start":
                NextNode("exit");
                break;
            case "DialogueNode":
                speaker.text= dataParts[1];
                dialogue.text= dataParts[2];
                speakerImage.sprite = node.GetSprite();
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
                NextNode("exit");
                break;
        }
        yield return null;
    }

    public void NextNode(string fieldname)
    {
        if(_parser != null)
        {
            StopCoroutine(_parser);
            _parser = null;
        }
        foreach (NodePort port in graph.current.Ports)
        {
            Debug.Log(port);
            if (port.fieldName == fieldname && port.Connection != null)
            {
                graph.current = port.Connection.node as DialogueBaseNode;
                break;
            }
            else
            {
                graph.current = null;
            }
        }
        if(graph.current != null)
        {
            _parser = StartCoroutine(ParseNode());
        }
    }
}
