using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem
{
    public static partial class Functions
    {
        public static Node unsolvable = new Node("unsolvable");

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
                    var possibleRules = rules.Where(x => x.Action == currBranch.prove).ToList();
                    if (possibleRules.Count==0)
                    {
                        solution = unsolvable;
                        continue;
                    }
                    else
                    {
                        branches.Push(new Branch(currBranch.prove, 1));
                        continue;
                    }
                }
                else if (currBranch.stage==1)
                {
                    if (solution==unsolvable)
                    {
                        solution = null;
                        branches.Push(new Branch(currBranch.prove, 1, currBranch.nodeNum + 1, 0));
                        continue;
                    }
                    if (solution!=null && !currBranch.buffer.Contains(solution))
                        currBranch.buffer.Add(solution);
                    var possibleRules = rules.Where(x => x.Action == currBranch.prove).ToList();
                    if (possibleRules.Count <= currBranch.nodeNum)
                    {
                        continue;
                    }
                    else if (possibleRules[currBranch.nodeNum].Conditions.Count == currBranch.factNum)
                    {
                        if (currBranch.buffer.Count != 0)
                            solution = new Node(currBranch.prove, currBranch.buffer);
                        continue;
                    }
                    else
                    {
                        branches.Push(new Branch(currBranch.prove,currBranch.buffer, 1, currBranch.nodeNum, currBranch.factNum + 1));
                        branches.Push(new Branch(possibleRules[currBranch.nodeNum].Conditions.ToList()[currBranch.factNum],currBranch.buffer, 0));
                        continue;
                    }
                }
            }
            if (solution == unsolvable)
                solution = null;
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

        public Node(string fact,List<Node> proves)
        {
            var prereqs = proves.ConvertAll(x => x.rule.Action);
            rule = new Rule(new HashSet<string>(prereqs), fact);
            this.proves = proves;
        }


        public Node(string fact)
        {
            rule = new Rule(new HashSet<string> { fact }, fact);
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
