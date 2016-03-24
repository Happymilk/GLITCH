using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace xnaplatformer
{
    public class FileManager
    {
        enum LoadType { Attributes, Contents };
        LoadType type;
        List<string> tempAttributes, tempContents;
        bool identifierFound = false;

        public void LoadContent(string filename, List<List<string>> attributes, List<List<string>> contents, string identifier)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (identifier == String.Empty)
                        identifierFound = true;
                    else if (line.Contains("EndLoad=") && line.Contains(identifier))
                    {
                        identifierFound = false;
                        break;
                    }
                    else if (line.Contains("Load=") && line.Contains(identifier))
                    {
                        identifierFound = true;
                        continue;
                    }
                    if (identifierFound)
                    {
                        if (line.Contains("Load="))
                        {
                            tempAttributes = new List<string>();
                            line = line.Remove(0, line.IndexOf("=") + 1);
                            type = LoadType.Attributes;
                        }
                        else
                        {
                            tempContents = new List<string>();
                            type = LoadType.Contents;
                        }
                        string[] lineArray = line.Split(']');
                        foreach (string li in lineArray)
                        {
                            string newLine = li.Trim('[', ' ', ']');
                            if (newLine != String.Empty)
                            {
                                if (type == LoadType.Contents)
                                    tempContents.Add(newLine);
                                else
                                    tempAttributes.Add(newLine);
                            }
                        }
                        if (type == LoadType.Contents && tempContents.Count > 0)
                        {
                            contents.Add(tempContents);
                            attributes.Add(tempAttributes);
                        }
                    }
                }
            }
        }

        public void SaveContent(string filename, string[] attributes, string[] contents, string identifier)
        {
            if (identifier == String.Empty)
                identifierFound = true;
            string[] lines = File.ReadAllLines(filename);
            List<string> fileList = new List<string>();
            fileList.AddRange(lines);
            int i = fileList.Count;
            string attribute = String.Empty;
            string content = String.Empty;
            if (!identifierFound)
            {
                for (i = 0; i < fileList.Count; i++)
                {
                    if (fileList[i].Contains("Load=") && fileList[i].Contains(identifier))
                    {
                        identifierFound = true;
                        break;
                    }
                }
            }
            foreach (string att in attributes)
                attribute += "[" + att + "]";
            if (!fileList.Contains("Load=" + attribute)) 
            {
                fileList.Add("");
                i++;
                fileList.Insert(i, "Load=" + attribute);
                i++;
                for (int j = 0; j < contents.LongLength; j++)
                {
                    content += "[" + contents[j] + "]";
                    if ((j + 1) % attributes.LongLength == 0)
                    {
                        fileList.Insert(i, content);
                        content = String.Empty;
                        i++;
                    }
                }
                File.WriteAllLines(filename, fileList.ToArray());
            }
        }
    }
}