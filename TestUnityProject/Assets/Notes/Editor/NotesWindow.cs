using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class NotesWindow : EditorWindow
{
    private const string fileName = "Notes.dat";
    private const string tagFileName = "NoteTags.dat";

    private string newNoteName = "New Note";

    private string noteContent = "";

    //private string selectedNote = "";

    private NoteData currentNote;

    public Notebook notebook = new Notebook(new List<NoteData>());

    private Vector2 notebookScrollView;
    private Vector2 contentScrollView;

    private string noteSearchTerm = "";

    private bool deletePromptToggle = false;
    private bool editToggle = false;

    private int newNoteTag = 2;
    private string newTag = "New Tag";
    private int searchTag = 0;

    private int curTag = 0;

    [SerializeField]
    private List<string> tags = new List<string>() { "New Tag", "Delete Tag", "Default"};
    private List<string> searchTags()
    {
        List<string> temp = new List<string>(tags);
        temp.Remove("New Tag");
        temp.Remove("Delete Tag");
        temp.Insert(0, "None");
        return temp;
    }
    private List<string> changeTags()
    {
        List<string> temp = new List<string>(tags);
        temp.Remove("New Tag");
        temp.Remove("Delete Tag");
        return temp;
    }

    Color color = Color.white;

    [MenuItem("Window/Notes")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(NotesWindow));
    }

    private void OnFocus()
    {
        Load();
    }

    private void OnLostFocus()
    {
        Save();
    }

    private void OnDestroy()
    {
        Save();
    }

    private void Awake()
    {
        Load();
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal(GUI.skin.box);
        GUILayout.FlexibleSpace();
        GUILayout.Label("Notes", EditorStyles.boldLabel);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        NewNoteGUI();
        //GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical(GUI.skin.box, GUILayout.MaxWidth(300));
        ListNotes();
        GUILayout.EndVertical();
        if (currentNote != null)
        {
            GUI.backgroundColor = currentNote.NoteColor();
        }
        GUILayout.BeginVertical(GUI.skin.box);      
        NoteContentGUI();
        GUI.backgroundColor = Color.white;
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }

    private void NoteContentGUI()
    {
        GUILayout.BeginHorizontal(GUI.skin.box);
        GUILayout.FlexibleSpace();
        if (currentNote != null)
        {
            GUILayout.Label(currentNote.Key, EditorStyles.boldLabel, GUILayout.MaxWidth(300));
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal(GUI.skin.box);
        GUILayout.FlexibleSpace();
        if (currentNote != null && currentNote.Key != "")
        {
            curTag = EditorGUILayout.Popup(curTag, changeTags().ToArray(), GUILayout.MaxWidth(75));
            if (changeTags().IndexOf(currentNote.tag) != curTag)
            {
                currentNote.tag = changeTags()[curTag];
            }
            color = EditorGUILayout.ColorField(color, GUILayout.MaxWidth(75));
            if(currentNote.NoteColor() != color)
            {
                currentNote.SetColor(color);
                Save();
            }
            EditorGUIUtility.labelWidth = 50;
            editToggle = EditorGUILayout.Toggle("Editable", editToggle, GUILayout.MaxWidth(75));
            EditorGUIUtility.labelWidth = 0;
            if (GUILayout.Button("Delete", GUILayout.MaxWidth(75)))
            {
                deletePromptToggle = true;
            }
        }
        if (deletePromptToggle)
        {
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Are you sure?", GUILayout.MaxWidth(150));
            if (GUILayout.Button("Yes", GUILayout.MaxWidth(100)))
            {
                deletePromptToggle = false;
                notebook.notes.Remove(currentNote);
                noteContent = "";

            }
            if (GUILayout.Button("No", GUILayout.MaxWidth(100)))
            {
                deletePromptToggle = false;
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal(GUI.skin.box);
        contentScrollView = GUILayout.BeginScrollView(contentScrollView, false, false);
        EditorGUI.BeginDisabledGroup(!editToggle);
        noteContent = EditorGUILayout.TextArea(noteContent, GUILayout.Height(position.height));
        GUILayout.EndScrollView();
        EditorGUI.EndDisabledGroup();
        GUILayout.EndHorizontal();
    }

    private void NewNoteGUI()
    {
        GUILayout.BeginHorizontal(GUI.skin.box);
        if (GUILayout.Button("Create Note", GUILayout.MaxWidth(290)))
        {
            NewNote();
        }
        EditorGUIUtility.labelWidth = 85;
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        newNoteName = EditorGUILayout.TextField("Note Name", newNoteName, GUILayout.MaxWidth(200));
        newNoteTag = EditorGUILayout.Popup(newNoteTag, tags.ToArray(), GUILayout.MaxWidth(100));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (newNoteTag == 0)
        {
            newTag = EditorGUILayout.TextField("Tag Name" , newTag, GUILayout.MaxWidth(200));           
            if(GUILayout.Button("Create", GUILayout.MaxWidth(100)))
            {
                bool tagExists = false;
                foreach(string tag in tags)
                {
                    if(tag == newTag)
                    {
                        tagExists = true;
                        break;
                    }
                }
                if(!tagExists)
                {
                    tags.Add(newTag);
                    newNoteTag = tags.IndexOf(newTag);
                    Save();
                }
                else
                {
                    //Throw error
                }
            }

        }
        if (newNoteTag == 1)
        {
            newTag = EditorGUILayout.TextField("Tag Name", newTag, GUILayout.MaxWidth(200));
            if (GUILayout.Button("Delete", GUILayout.MaxWidth(100)))
            {
                if (newTag != tags[0] && newTag != tags[1] && newTag != tags[2])
                {
                    bool tagExists = false;
                    foreach (string tag in tags)
                    {
                        if (tag == newTag)
                        {
                            tagExists = true;
                            break;
                        }
                    }
                    if (tagExists)
                    {
                        tags.Remove(newTag);

                    }
                    else
                    {
                        //Throw error
                    }
                }
                else
                {
                    //Throw Error
                }
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        EditorGUIUtility.labelWidth = 0;
 
        GUILayout.EndHorizontal();
    }

    private void ListNotes()
    {
        GUILayout.BeginHorizontal(GUI.skin.box);
        EditorGUIUtility.labelWidth = 50;
        noteSearchTerm = EditorGUILayout.TextField("Search", noteSearchTerm, GUILayout.MaxWidth(190));
        EditorGUIUtility.labelWidth = 0;
        searchTag = EditorGUILayout.Popup(searchTag, searchTags().ToArray(), GUILayout.MaxWidth(100));
        GUILayout.EndHorizontal();
        notebookScrollView = GUILayout.BeginScrollView(notebookScrollView, false, false, GUILayout.MaxWidth(300));
        GUILayout.BeginVertical(GUI.skin.box);
        if (noteSearchTerm == "" && searchTag == 0)
        {
            foreach (NoteData note in notebook.notes)
            {
                if(!tags.Contains(note.tag))
                {
                    note.tag = "Default";
                }
                string temp = "";
                if(note.tag != "Default")
                {
                    temp += "[" + note.tag + "]";
                }
                GUI.backgroundColor = note.NoteColor();
                if (GUILayout.Button(temp + note.Key, GUILayout.MaxWidth(300)))
                {
                    if (currentNote != null)
                    {
                        currentNote.Value = noteContent;
                    }
                    GUI.FocusControl(null);
                    currentNote = note;
                    noteContent = note.Value;
                    curTag = changeTags().IndexOf(note.tag);
                    color = note.NoteColor();
                }
            }
            GUI.backgroundColor = Color.white;
        }
        else
        {
            foreach (NoteData note in notebook.notes)
            {
                if (!tags.Contains(note.tag))
                {
                    note.tag = "Default";
                }
                string temp = "";
                if (note.tag != "Default")
                {
                    temp += "[" + note.tag + "]";
                }
                if ((note.Key.Contains(noteSearchTerm) && note.tag == searchTags()[searchTag]) || (note.Key.Contains(noteSearchTerm) && searchTag == 0))
                {
                    GUI.backgroundColor = note.NoteColor();
                    if (GUILayout.Button(temp + note.Key, GUILayout.MaxWidth(300)))
                    {
                        if (currentNote != null)
                        {
                            currentNote.Value = noteContent;
                        }
                        GUI.FocusControl(null);
                        currentNote = note;
                        noteContent = note.Value;
                        //selectedNote = note.Key;
                        curTag = changeTags().IndexOf(note.tag);
                        color = note.NoteColor();
                    }
                }
            }
            GUI.backgroundColor = Color.white;
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        
    }

    private void NewNote()
    {
        bool duplicateName = false;
        foreach(NoteData note in notebook.notes)
        {
            if(note.Key == newNoteName)
            {
                duplicateName = true;
                break;
            }
        }
        if(!duplicateName)
        {
            notebook.notes.Add(new NoteData(newNoteName, "", tags[newNoteTag]));
        }

    }

    private void Save()
    {
        string destination = Application.dataPath + "/Notes/" + fileName;
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, notebook);
        file.Close();

        destination = Application.dataPath + "/Notes/" + tagFileName;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        //bf = new BinaryFormatter();
        bf.Serialize(file, tags);
        file.Close();
    }

    private void Load()
    {
        string destination = Application.dataPath + "/Notes/" + fileName;
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            //Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        Notebook data = (Notebook)bf.Deserialize(file);
        file.Close();

        notebook = data;

        destination = Application.dataPath + "/Notes/" + tagFileName;
        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            //Debug.LogError("File not found");
            return;
        }

        //BinaryFormatter bf = new BinaryFormatter();
        List<string> data2 = (List<string>)bf.Deserialize(file);
        file.Close();

        tags = data2;
    }
}

[System.Serializable]
public class NoteData
{
    public string Key;
    public string Value;
    public string tag;

    [SerializeField]
    private float[] color = new float[4];

    public NoteData(string k, string v, string t)
    {
        Key = k;
        Value = v;
        tag = t;
        color[0] = Color.white.r;
        color[1] = Color.white.g;
        color[2] = Color.white.b;
        color[3] = Color.white.a;
    }

    public Color NoteColor()
    {
        return new Color(color[0], color[1], color[2], color[3]);
    }
    public void SetColor(Color c)
    {
        color[0] = c.r;
        color[1] = c.g;
        color[2] = c.b;
        color[3] = c.a;
    }
}

[System.Serializable]
public class Notebook
{
    public List<NoteData> notes;

    public Notebook(List<NoteData> n)
    {
        notes = n;
    }
}
