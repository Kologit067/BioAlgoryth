using ExactStringCompare.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExactStringCompare
{
    //--------------------------------------------------------------------------------------
    // class SuffixTreeSimple 
    //--------------------------------------------------------------------------------------
    public class SuffixTreeSimple
    {
        //--------------------------------------------------------------------------------------
        public SuffixTreeNode Execute(string text)
        {
            int lastPositionInText = text.Length;
            text += "$";
            SuffixTreeNode root = new SuffixTreeNode();
            for(int i = 0; i < text.Length; i++)
            {
                int j = i;
                SuffixTreeNode current = root;
                while(current != null)
                {
                    if (current.Chields.ContainsKey(text[j]))
                    {
                        SuffixTreeNode next = current.Chields[text[j]];
                        int j0 = j;
                        int k = next.StarPosition;
                        while (k <= next.EndPosition)
                            if (text[j++] != text[k++])
                                break;
                        if (k > next.EndPosition)
                            current = next;
                        else
                        {
                            SuffixTreeNode newMiddle = new SuffixTreeNode() {
                                Parent = current.Parent,
                                StarPosition = current.StarPosition,
                                EndPosition = k - 1,
                                StartSymbol = current.StartSymbol
                            };
                            SuffixTreeNode newLeaf = new SuffixTreeNode()
                            {
                                Parent = newMiddle,
                                StarPosition = j,
                                EndPosition = lastPositionInText,
                                StartSymbol = text[j]
                            };
                            newMiddle.Chields.Add(text[k], newLeaf);
                            newMiddle.Chields.Add(text[j], current);
                            newMiddle.Parent.Chields[newMiddle.StartSymbol] = newMiddle;
                            current.Parent = newMiddle;
                            current.StarPosition = k;
                            current.StartSymbol = text[k];
                            break;
                        }
                    }
                    else
                    {
                        current.Chields.Add(text[j], new SuffixTreeNode() { });
                        break;
                    }
                }
            }
            return root;
        }
        //--------------------------------------------------------------------------------------
        public string NodePresentationAsString(SuffixTreeNode node)
        {
            return $"[{node.StarPosition}-{node.EndPosition}({string.Join(",",node.Chields.OrderBy(n => n.Key).Select(n => NodePresentationAsString(n.Value)))})]";
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
