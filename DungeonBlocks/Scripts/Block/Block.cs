using UnityEngine;
using System.Collections.Generic;

public class Block : MonoBehaviour {

    public List<Attachment> Attachments = new List<Attachment>();

    //***************************************************************************************************
    // Use this for initialization
    void Start ()
    {
        Generate();
        Destroy();
    }

    //***************************************************************************************************

    void Generate ()
    {
        foreach (Attachment attachment in Attachments)
        {
            if (attachment.Correspond == true)
            {
                GenerateObjWithCorrespondence(attachment);
            }
            else
            {
                GenerateObjRandomly(attachment);
            }
        }
    }

    //***************************************************************************************************

    void GenerateObjRandomly (Attachment attachment)
    {
        List<GameObject> UsedConnectors = new List<GameObject>();

        int R = Random.Range(attachment.MinNumberOfObjects, attachment.MaxNumberOfObjects+1);
        int Min = attachment.MinNumberOfObjects;

        //print(Min + " :: R: " + R + " :: Max: " + (attachment.Connectors.Count+1));

        for (int i = Min; i <= R; i++)
        {
            GameObject Connector = ChooseRandomConnector(attachment.Connectors, UsedConnectors);

            if (Connector == null) { Destroy(Connector); continue; }

            GameObject Object = attachment.Objects[ Random.Range(0, attachment.Objects.Count) ];
            GenerateObject(Object, Connector);
            UsedConnectors.Add(Connector);
        }
    }

    //***************************************************************************************************

    GameObject ChooseRandomConnector ( List<GameObject> Connectors , List<GameObject> Used)
    {
        int i = 0;

        while (true)
        {
            if (i > 10) { return null; }
           
            int R = Random.Range(0, Connectors.Count);
            GameObject Connector = Connectors[R];

            if (Used.Exists(obj => obj == Connector)) { i++; continue; }
            else { return Connector; }
        }
    }

    //***************************************************************************************************

    void GenerateObjWithCorrespondence (Attachment attachment)
    {
        for (int i = 0; i < attachment.Objects.Count; i++)
        {
            GameObject Connector = attachment.Connectors[i];
            GameObject Object = attachment.Objects[i];
            GenerateObject(Object, Connector);
        }
    }

    //***************************************************************************************************

    void GenerateObject (GameObject Object , GameObject Connector )
    {
        GameObject ObjClone = Instantiate(Object);
        ObjClone.transform.position = Connector.transform.position;
        ObjClone.transform.rotation = Connector.transform.rotation;
        ObjClone.transform.parent = gameObject.transform;
    }

    //***************************************************************************************************

    void Destroy()
    {
        for (int i = 0; i < Attachments.Count; i++)
        {
            Attachment attachment = Attachments[i];
            for (int j = 0; j < attachment.Connectors.Count; j++)
            {
                Destroy(attachment.Connectors[j]);
            }
        }
        Destroy(this);
    }

    //***************************************************************************************************
}

//***************************************************************************************************
[System.Serializable]
public class Attachment
{
    public bool Correspond = false;
    public int MinNumberOfObjects;
    public int MaxNumberOfObjects;
    public List<GameObject> Objects = new List<GameObject>();
    public List<GameObject> Connectors = new List<GameObject>();
}

//***************************************************************************************************