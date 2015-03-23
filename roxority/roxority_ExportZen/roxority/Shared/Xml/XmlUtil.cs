namespace roxority.Shared.Xml
{
    using roxority.Shared;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml;

    internal static class XmlUtil
    {
        internal static string Attribute(XmlNode node, string name)
        {
            return Attribute(node, name, string.Empty);
        }

        internal static string Attribute(XmlNode node, string name, string defaultValue)
        {
            XmlAttribute attribute;
            if (((attribute = node.Attributes[name]) != null) && !SharedUtil.IsEmpty(attribute.Value))
            {
                return attribute.Value;
            }
            return defaultValue;
        }

        internal static XmlDocument Compress(XmlDocument document)
        {
            if ((document != null) && (document.DocumentElement != null))
            {
                Dictionary<string, string> dictionary;
                Dictionary<string, string> dictionary2;
                XmlNodeList list;
                XmlNode node;
                if (!SharedUtil.IsEmpty(list = document.SelectNodes("processing-instruction()")))
                {
                    foreach (XmlProcessingInstruction instruction in list)
                    {
                        if (instruction.Name.StartsWith("n_"))
                        {
                            return document;
                        }
                    }
                }
                Compress(dictionary2 = new Dictionary<string, string>(), dictionary = new Dictionary<string, string>(), document.DocumentElement);
                foreach (KeyValuePair<string, string> pair in dictionary2)
                {
                    document.InsertBefore(document.CreateProcessingInstruction(pair.Value, pair.Key), document.DocumentElement);
                }
                if (!SharedUtil.IsEmpty(list = document.DocumentElement.SelectNodes("dsxc_values")))
                {
                    foreach (XmlNode node2 in list)
                    {
                        document.DocumentElement.RemoveChild(node2);
                    }
                }
                document.DocumentElement.InsertBefore(node = document.CreateElement("dsxc_values"), document.DocumentElement.FirstChild);
                foreach (KeyValuePair<string, string> pair2 in dictionary)
                {
                    node.AppendChild(document.CreateElement(pair2.Value)).AppendChild(document.CreateCDataSection(pair2.Key));
                }
            }
            return document;
        }

        internal static void Compress(Dictionary<string, string> symbolTable, Dictionary<string, string> valueTable, XmlNode node)
        {
            XmlDocument document;
            if ((node != null) && ((document = node.OwnerDocument) != null))
            {
                string str;
                if (!symbolTable.TryGetValue(node.LocalName, out str))
                {
                    symbolTable[node.LocalName] = str = "n_" + symbolTable.Count;
                }
                XmlNode newChild = document.CreateElement(str);
                while ((node.Attributes != null) && (node.Attributes.Count > 0))
                {
                    XmlAttribute attribute;
                    string str2;
                    node.Attributes.Remove(attribute = node.Attributes[0]);
                    if (!symbolTable.TryGetValue(attribute.LocalName, out str))
                    {
                        symbolTable[attribute.LocalName] = str = "n_" + symbolTable.Count;
                    }
                    if (!valueTable.TryGetValue(attribute.Value, out str2))
                    {
                        if (attribute.Value.Length > ("v" + valueTable.Count).Length)
                        {
                            valueTable[attribute.Value] = str2 = "v" + valueTable.Count;
                        }
                        else
                        {
                            str2 = attribute.Value;
                        }
                    }
                    newChild.Attributes.Append(document.CreateAttribute(str)).Value = str2;
                }
                while (node.HasChildNodes)
                {
                    XmlNode node3;
                    node.RemoveChild(node3 = node.FirstChild);
                    newChild.AppendChild(node3);
                    if (node3.NodeType == XmlNodeType.Element)
                    {
                        Compress(symbolTable, valueTable, node3);
                    }
                }
                node.ParentNode.ReplaceChild(newChild, node);
            }
        }

        internal static string EnsureAttribute(XmlNode node, string attributeName, string defaultValue)
        {
            return EnsureAttribute(node, attributeName, defaultValue, false);
        }

        internal static string EnsureAttribute(XmlNode node, string attributeName, string defaultValue, bool overwrite)
        {
            XmlAttribute orCreateAttribute = GetOrCreateAttribute(node, attributeName);
            if (overwrite || SharedUtil.IsEmpty(orCreateAttribute.Value))
            {
                orCreateAttribute.Value = defaultValue;
            }
            return orCreateAttribute.Value;
        }

        internal static XmlDocument Expand(XmlDocument document)
        {
            XmlNode node = null;
            XmlNodeList list;
            if (((document != null) && (document.DocumentElement != null)) && !SharedUtil.IsEmpty(list = document.DocumentElement.SelectNodes("dsxc_values")))
            {
                foreach (XmlNode node2 in list)
                {
                    document.DocumentElement.RemoveChild(node = node2);
                }
                Dictionary<string, string> valueTable = new Dictionary<string, string>();
                Dictionary<string, string> symbolTable = new Dictionary<string, string>();
                if (SharedUtil.IsEmpty(list = document.SelectNodes("processing-instruction()")))
                {
                    return document;
                }
                foreach (XmlProcessingInstruction instruction in list)
                {
                    if (instruction.Name.StartsWith("n_"))
                    {
                        document.RemoveChild(instruction);
                        symbolTable[instruction.Name] = instruction.Value;
                    }
                }
                if (SharedUtil.IsEmpty((ICollection) symbolTable))
                {
                    return document;
                }
                foreach (XmlNode node3 in node)
                {
                    if ((node3.FirstChild != null) && (node3.FirstChild.NodeType == XmlNodeType.CDATA))
                    {
                        valueTable[node3.LocalName] = node3.FirstChild.Value;
                    }
                }
                Expand(symbolTable, valueTable, document.DocumentElement);
            }
            return document;
        }

        internal static void Expand(Dictionary<string, string> symbolTable, Dictionary<string, string> valueTable, XmlNode node)
        {
            XmlDocument document;
            if ((node != null) && ((document = node.OwnerDocument) != null))
            {
                string localName;
                if (!symbolTable.TryGetValue(node.LocalName, out localName))
                {
                    localName = node.LocalName;
                }
                XmlNode newChild = document.CreateElement(localName);
                while ((node.Attributes != null) && (node.Attributes.Count > 0))
                {
                    XmlAttribute attribute;
                    string str2;
                    node.Attributes.Remove(attribute = node.Attributes[0]);
                    if (!symbolTable.TryGetValue(attribute.LocalName, out localName))
                    {
                        localName = attribute.LocalName;
                    }
                    if (!valueTable.TryGetValue(attribute.Value, out str2))
                    {
                        str2 = attribute.Value;
                    }
                    newChild.Attributes.Append(document.CreateAttribute(localName)).Value = str2;
                }
                while (node.HasChildNodes)
                {
                    XmlNode node3;
                    node.RemoveChild(node3 = node.FirstChild);
                    newChild.AppendChild(node3);
                    if (node3.NodeType == XmlNodeType.Element)
                    {
                        Expand(symbolTable, valueTable, node3);
                    }
                }
                node.ParentNode.ReplaceChild(newChild, node);
            }
        }

        internal static void ForEach(XmlNode node, string xpath, Action<XmlNode> action)
        {
            XmlNodeList list = node.SelectNodes(xpath);
            if (!SharedUtil.IsEmpty(list))
            {
                foreach (XmlNode node2 in list)
                {
                    action(node2);
                }
            }
        }

        internal static string GetAttributeValue(XmlNode node, string name, string defaultValue)
        {
            XmlAttribute attribute;
            if (((node != null) && (node.Attributes != null)) && ((attribute = node.Attributes[name]) != null))
            {
                return attribute.Value;
            }
            return defaultValue;
        }

        internal static Duo<XmlNode, string> GetNodeAtPosition(XmlNode node, int selectionStart, int selectionLength, XmlNodeType moveUpTo, params XmlNodeType[] moveUpIf)
        {
            XmlNode parentNode = null;
            string str = null;
            string str2;
            if ((((node != null) && !SharedUtil.IsEmpty(str2 = node.OuterXml)) && ((selectionStart >= 0) && (selectionStart <= str2.Length))) && ((selectionLength >= 0) && ((selectionStart + selectionLength) <= str2.Length)))
            {
                str = str2.Substring(selectionStart, selectionLength);
                if (!node.HasChildNodes)
                {
                    parentNode = node;
                }
                else
                {
                    int num;
                    string innerXml = node.InnerXml;
                    int num2 = num = str2.IndexOf(innerXml, str2.IndexOf('>'));
                    if (num2 < 0)
                    {
                        num2 = num = 0;
                    }
                    if (selectionStart >= num)
                    {
                        foreach (XmlNode node3 in node.ChildNodes)
                        {
                            if (((selectionStart >= num2) && (selectionStart < (num2 + node3.OuterXml.Length))) && ((parentNode = GetNodeAtPosition(node3, selectionStart - num2, selectionLength, moveUpTo, new XmlNodeType[0]).Value1) != null))
                            {
                                break;
                            }
                            num2 += node3.OuterXml.Length;
                        }
                    }
                    if (parentNode == null)
                    {
                        parentNode = node;
                    }
                }
                if (moveUpTo != XmlNodeType.None)
                {
                    while (((parentNode != null) && (parentNode.NodeType != moveUpTo)) && (parentNode.ParentNode != null))
                    {
                        parentNode = parentNode.ParentNode;
                    }
                }
                else if (!SharedUtil.IsEmpty((ICollection) moveUpIf))
                {
                    while (((parentNode != null) && SharedUtil.In<XmlNodeType>(parentNode.NodeType, moveUpIf)) && (parentNode.ParentNode != null))
                    {
                        parentNode = parentNode.ParentNode;
                    }
                }
            }
            return new Duo<XmlNode, string>(parentNode, str);
        }

        internal static int GetNodePosition(XmlNode parentNode, XmlNode targetNode)
        {
            if ((targetNode != null) && (parentNode != null))
            {
                if ((targetNode == parentNode) || ((targetNode.OuterXml == parentNode.OuterXml) && (GetXPath(targetNode) == GetXPath(parentNode))))
                {
                    return 0;
                }
                if (parentNode.HasChildNodes)
                {
                    int index = parentNode.OuterXml.IndexOf(parentNode.InnerXml, parentNode.OuterXml.IndexOf('>'));
                    if (index < 0)
                    {
                        index = 0;
                    }
                    foreach (XmlNode node in parentNode.ChildNodes)
                    {
                        int nodePosition = GetNodePosition(node, targetNode);
                        if (nodePosition >= 0)
                        {
                            return (index + nodePosition);
                        }
                        index += node.OuterXml.Length;
                    }
                }
            }
            return -1;
        }

        internal static XmlAttribute GetOrCreateAttribute(XmlNode node, string name)
        {
            XmlAttribute attribute = node.Attributes[name];
            if (attribute != null)
            {
                return attribute;
            }
            return (attribute = node.Attributes.Append(node.OwnerDocument.CreateAttribute(name)));
        }

        internal static XmlNode GetOrCreateCData(XmlNode node, string data)
        {
            if (node == null)
            {
                return null;
            }
            if (!node.HasChildNodes || (node.FirstChild.NodeType != XmlNodeType.CDATA))
            {
                XmlNode newChild = node.OwnerDocument.CreateCDataSection(data);
                if (node.HasChildNodes)
                {
                    node.InsertBefore(newChild, node.FirstChild);
                }
                else
                {
                    node.AppendChild(newChild);
                }
            }
            return node.FirstChild;
        }

        internal static XmlNode GetOrCreateNode(XmlNode node, string name)
        {
            if (node == null)
            {
                return null;
            }
            XmlNode node2 = node.SelectSingleNode(name);
            if (node2 != null)
            {
                return node2;
            }
            return (node2 = node.AppendChild(node.OwnerDocument.CreateElement(name)));
        }

        internal static string GetXPath(XmlNode node)
        {
            return GetXPath(node, null, null);
        }

        internal static string GetXPath(XmlNode node, XmlNode stopBefore, string encodeSlash)
        {
            string str = null;
            int num = 0;
            if (node != null)
            {
                string str2;
                str = "/" + node.LocalName;
                if (!SharedUtil.IsEmpty((ICollection) node.Attributes))
                {
                    str = str + " [";
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        str = str + string.Format("@{0} = '{1}' and ", attribute.LocalName, attribute.Value.Replace("'", "&apos;").Replace("/", SharedUtil.IsEmpty(encodeSlash) ? "/" : encodeSlash));
                    }
                    str = str.Substring(0, str.Length - " and ".Length) + "]";
                }
                else
                {
                    XmlNodeList list;
                    if (((node.ParentNode != null) && (node.NodeType == XmlNodeType.Element)) && (!SharedUtil.IsEmpty(list = node.ParentNode.SelectNodes(node.LocalName)) && (list.Count > 1)))
                    {
                        foreach (XmlNode node2 in list)
                        {
                            num++;
                            if (node2 == node)
                            {
                                break;
                            }
                            if (num == list.Count)
                            {
                                num = 0;
                            }
                        }
                        if (num > 0)
                        {
                            object obj2 = str;
                            str = string.Concat(new object[] { obj2, " [", num, "]" });
                        }
                    }
                }
                if (((node.ParentNode != null) && (node.ParentNode != node.OwnerDocument)) && ((node.ParentNode != stopBefore) && !SharedUtil.IsEmpty(str2 = GetXPath(node.ParentNode))))
                {
                    str = str2 + str;
                }
            }
            return str;
        }

        internal static XmlDocument LoadXmlDocument(string filePath)
        {
            return LoadXmlDocument(filePath, null);
        }

        internal static XmlDocument LoadXmlDocument(string filePath, XmlNameTable nameTable)
        {
            XmlDocument document = (nameTable == null) ? new XmlDocument() : new XmlDocument(nameTable);
            document.Load(filePath);
            return document;
        }

        internal static XmlNode LoadXmlNode(XmlDocument document, string xml)
        {
            document.LoadXml(xml);
            return document.DocumentElement;
        }

        internal static XmlDocument ParseXmlDocument(string xml)
        {
            return ParseXmlDocument(xml, null);
        }

        internal static XmlDocument ParseXmlDocument(string xml, XmlNameTable nameTable)
        {
            XmlDocument document = (nameTable == null) ? new XmlDocument() : new XmlDocument(nameTable);
            document.LoadXml(xml);
            return document;
        }

        internal static XmlNode ParseXmlNode(XmlDocument document, string xml, XmlNameTable nameTable)
        {
            XmlDocument document2 = new XmlDocument(nameTable);
            document2.LoadXml(xml);
            return document.ImportNode(document2.DocumentElement, true);
        }

        internal static int TotalNodeCount(XmlNode node)
        {
            int num = 0;
            if (node != null)
            {
                num++;
                if (!node.HasChildNodes)
                {
                    return num;
                }
                foreach (XmlNode node2 in node.ChildNodes)
                {
                    num += TotalNodeCount(node2);
                }
            }
            return num;
        }
    }
}

