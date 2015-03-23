using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DsLib
{
    #region BSTUtils
    public static class BSTUtils
    {
        public static BinarySearchTree<T> GetABuiltBST<T>(IEnumerable<T> valuesToAdd) where T : IComparable<T>
        {
            BinarySearchTree<T> tree = new BinarySearchTree<T>();

            foreach (T i in valuesToAdd)
            {
                tree.Add(i);
            }

            return tree;
        }

        #region Caml Operation
        /// <summary>
        /// Draw Caml Tree for filter control
        /// </summary>
        /// <param name="structureData">format as 0-And:1;0-And:2;0-And:3;0-And:4;1-Or:5;1-Or:6;2-And:7, has Parent-child relationship</param>
        /// <param name="realData">caml list</param>
        public static string DrawCamlTree(string structureData, Dictionary<string, string> realData)
        {
            var dataBlock = FormatStringToDic(structureData);
            BinarySearchTree<string> tree = new BinarySearchTree<string>();
            foreach (var str in dataBlock.Keys)
            {
                DoDraw(ref tree, str, dataBlock[str]);

            }

            return FillCamlTreeWithRealData(tree.CamlGenerate(tree.Root),realData);
        }

        private static void DoDraw(ref BinarySearchTree<string> tree, string parent, List<string> data)
        {
            string parentNodeValue = parent.Replace("-Or", "").Replace("-And", "");
            string parentNodeOperate = parent.Replace(parentNodeValue + "-", "");
            BinaryTreeNode<string> parentNode = new BinaryTreeNode<string>("-1");
            List<string> endData = new List<string>();
            if (parentNodeValue != "0")
            {

                tree.SearchNodeInorder(new BinaryTreeNode<string>(parentNodeValue), tree.Root, ref parentNode);
                if (parentNode.Value != "-1")
                {
                    if (parentNode.Parent.Right == null)
                    {
                        parentNode.Parent.Right = parentNode.Parent.Left;
                    }
                    tree.StartNode = parentNode;
                    parentNode.Value = parentNodeOperate;
                    data.Insert(0, parentNodeValue);
                }
            }
            else
            {
                tree.Add(parentNodeOperate, false);
            }

            for (int i = 0; i < data.Count; i++)
            {
                if (i < data.Count - 2)
                {
                    endData.Add(parentNodeOperate);
                }
                endData.Add(data[i]);
            }

            foreach (var str in endData)
            {
                tree.Add(str, false);
            }
        }

        private static string FillCamlTreeWithRealData(string realStructure,Dictionary<string,string> realData)
        {

            foreach (var realDataItem in realData)
            {
                realStructure = realStructure
                             .Replace("<And>(" + realDataItem.Key + ")", "<And>" + realDataItem.Value)
                             .Replace("<Or>(" + realDataItem.Key + ")", "<Or>" + realDataItem.Value)
                             .Replace("(" + realDataItem.Key + ")</And>", realDataItem.Value + "</And>")
                             .Replace("(" + realDataItem.Key + ")</Or>", realDataItem.Value + "</Or>");
            }
            return realStructure;
        }

        private static Dictionary<string, List<string>> FormatStringToDic(string data)
        {
            var dataBlock = new Dictionary<string, List<string>>();
            string[] strs = data.Split(';');
            foreach (var str in strs)
            {
                string[] thisStrs = str.Split(':');
                if (!dataBlock.ContainsKey(thisStrs[0]))
                {
                    var valueList = new List<string>();
                    valueList.Add(thisStrs[1]);
                    dataBlock.Add(thisStrs[0], valueList);
                }
                else
                {
                    dataBlock[thisStrs[0]].Add(thisStrs[1]);
                }
            }
            return dataBlock;
        }
        #endregion
    }
    #endregion
}
