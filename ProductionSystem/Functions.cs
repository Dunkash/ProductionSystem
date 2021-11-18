using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductionSystem
{
    static public class Functions
    {
        static public Tuple<HashSet<string>,HashSet<Rule>> Parse(string fname, string separator = "***")
        {
            var facts = new HashSet<string>();
            var rules = new HashSet<Rule>();

            var Reader = File.OpenText(fname);
            string line;
            while ((line = Reader.ReadLine()) != null && line!=separator)
            {
                if (line != "")
                    facts.Add(line.ToLower().Trim());
            }


            var explanation = "";
            while ((line = Reader.ReadLine()) != null)
            {
                if (line != "")
                {
                    if (line[0] == '#')
                    {
                        if (explanation != "")
                            explanation += "\n";
                        explanation += line.Substring(1);
                    }
                    else
                    {
                        var splitRes = line.Split(new string[] { "->" }, StringSplitOptions.None);
                        if (splitRes.Count() == 2)
                            rules.Add(new Rule(new HashSet<string>(splitRes[0].Split(',').Select(x=>x.ToLower().Trim())), splitRes[1].ToLower().Trim(), explanation));
                        explanation = "";
                    }
                }
                else
                    explanation = "";
            }

            return new Tuple<HashSet<string>, HashSet<Rule>>(facts, rules);
        }

        public static HashSet<string> Forward(HashSet<string> facts, HashSet<Rule> rules)
        {
            var res = facts.ToHashSet();
            foreach (var rule in rules)
            {
                if (!rule.Evaluated && rule.Conditions.All(x => res.Contains(x)))
                { 
                    res.Add(rule.Action);
                    rule.Evaluated = true;
                }
            }
            return res;
        }

        public static HashSet<string> Backward(HashSet<string> facts, HashSet<Rule> rules)
        {
            var res = facts.ToHashSet();
            foreach (var rule in rules)
            {
                if (!rule.Evaluated && res.Contains(rule.Action))
                {
                    foreach(var condition in rule.Conditions)
                        res.Add(condition);
                    rule.Evaluated = true;
                }
            }
            return res;
        }

        public static HashSet<string> Evaluate(HashSet<string> facts, HashSet<Rule> rules, bool forward)
        {
            var res = facts.ToHashSet();
            var count = res.Count() + 1;

            while(res.Count()!=count)
            {
                count = res.Count();
                if (forward)
                    res = Forward(res, rules);
                else
                    res = Backward(res, rules);
                
            }
            return res;
        }
    }
}