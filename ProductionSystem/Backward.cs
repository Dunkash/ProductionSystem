using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem
{
    public static partial class Functions
    {
        public static Node Prove(string prove, HashSet<string> facts, HashSet<Rule> rules)
        {
            Node solution = null;
            var branches = new Stack<Branch>();
            branches.Push(new Branch(prove));

            while (branches.Count != 0)
            {
                var currBranch = branches.Pop();
                if (currBranch.stage == 0)
                {
                    if (facts.Contains(currBranch.prove))
                    { 
                        solution = new Node(currBranch.prove);
                        continue;
                    }
                    
                }
                else
                {

                }
            }

            return solution;
        }
    }

    public class Node
    {
        Rule rule;
        List<Node> proves;

        public Rule Rule
        {
            get { return rule; }
            set { rule = value; }
        }
        public List<Node> Proves
        {
            get { return proves; }
        }

        public Node(Rule rule,List<Node> proves)
        {
            this.rule = rule;
            this.proves = proves;
        }

        public Node(string fact)
        {
            this.rule = new Rule(new HashSet<string> { fact }, fact);
        }
    }

    public class Branch
    {
        public string prove { get; set; }
        public int stage { get; set; }
        public int nodeNum { get; set; }
        public int factNum { get; set; }
        public List<Node> buffer { get; set; }

        public Branch(string prove, List<Node> nodes, int stage = 0, int nodeNum = 0, int factNum=0)
        {
            this.prove = prove;
            buffer = nodes;
            this.stage = stage;
            this.nodeNum = nodeNum;
            this.factNum = factNum;
        }

        public Branch(string prove, int stage = 0, int nodeNum = 0, int factNum = 0)
        {
            this.prove = prove;
            buffer = new List<Node>();
            this.stage = stage;
            this.nodeNum = nodeNum;
            this.factNum = factNum;
        }
    }
}
