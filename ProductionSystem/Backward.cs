using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem
{
    public static partial class Functions
    {
        //Cпециальный нод, помечающий невозможность решения этой ветви (в отличии от Null, указывающей просто отсутствие этого решения)
        public static Node unsolvable = new Node("unsolvable");

        //Нерекурсивный алгоритм обратного поиска. Принимает на вход факт который нужно доказать, начальные факты, и правила.
        //Возвращает одностороннее дерево нодов, рекурсивно определяемое как Rules - применяемое правило и Proves - ноды доказательства для всех фактов, требуемых для его выполнения.
        //Возвращает null, если решение не найдено
        //Нихуя не оптимизированно, возможно всё ещё сломано. Юзать на свой страх и риск.
        public static Node Prove(string prove, HashSet<string> facts, HashSet<Rule> rules)
        {
            Node solution = null;
            var branches = new Stack<Branch>();
            branches.Push(new Branch(prove));

            
            while (branches.Count != 0)
            {
                var currBranch = branches.Pop();
                //Рассчёт доказуемости каждого факта
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
                //Рассмотрение структуры дерева
                else if (currBranch.stage==1)
                {
                    var possibleRules = rules.Where(x => x.Action == currBranch.prove).ToList();
                    if (possibleRules.Count <= currBranch.nodeNum)
                    {
                        continue;
                    }
                    if (solution != null && solution!=unsolvable && !currBranch.buffer.Contains(solution))
                    {
                        currBranch.buffer.Add(solution);
                        solution = null;
                    }
                    if (possibleRules[currBranch.nodeNum].Conditions.Count == currBranch.factNum)
                    {
                        if (currBranch.buffer.Count != 0 && solution!=unsolvable)
                            solution = new Node(currBranch.prove, currBranch.buffer);
                        else if (possibleRules.Count-1 > currBranch.nodeNum)
                        {
                            solution = null;
                            branches.Push(new Branch(currBranch.prove, 1, currBranch.nodeNum + 1, 0));
                        }
                        continue;
                    }
                    if (solution==unsolvable)
                    {
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

    //Представление ячейки в дереве поиска
    //ВАЖНО: Node с пустым Prooves и правилом вида а -> a - представление константного факта.
    public class Node
    {
       
        Rule rule;
        List<Node> proves;
        //Применяемое правило
        public Rule Rule
        {
            get { return rule; }
            set { rule = value; }
        }
        //Node доказательства для всех фактов, требуемых для выполнения правила в этом ноде
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

        public override string ToString()
        {
            var res = "";

            if (Proves==null)
            {
                res = rule.Action;
            }
            else
            { 
                foreach (var proof in Proves)
                {
                    res += proof.Rule.Action;
                    if (proof != Proves.Last())
                        res += ",";
                }
                res += " ­-> " + rule.Action;
            }
            return res;
        }
    }


    //Представление ветви дерева. Хранит данные, нужные для каждого уровня глубины и определения, на каком И/Или node мы находимся
    public class Branch
    {
        public string prove { get; set; }
        public int stage { get; set; }

        //Номер "Или" поддерева
        public int nodeNum { get; set; }
        //Номер "И" поддерева
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
