using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem
{
    public class Rule
    {
        HashSet<string> _conditions;
        string _action;
        string _explanation;
        bool _evaluated;
        
        public HashSet<string> Conditions
        {
            get { return _conditions; }
        }

        public string Action
        {
            get { return _action; }
        }

        public string Explanation
        {
            get { return _explanation; }
        }

        public bool Evaluated
        {
            get { return _evaluated; }
            set { _evaluated = value; }
        }

        public Rule(HashSet<string> cond, string act, string expl = "")
        {
            _conditions = cond;
            _action = act;
            _explanation = expl;
            _evaluated = false;
        }


        public override string ToString()
        {
            string res = "";
            if (Explanation != "")
                res += "# " + Explanation + "\n";

            res += "If ";
            foreach (var i in Conditions)
            {
                res += i;
                if (i != Conditions.Last())
                    res += ", ";
            }
            
            res += " ­-> " + Action;
            return res;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {


            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var Rule = (Rule)obj;
            return Rule.Conditions.SetEquals(this.Conditions) && Rule.Action.Equals(this.Action);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            var res = 1;
            // TODO: write your implementation of GetHashCode() here
            foreach (var i in Conditions)
                res*=i.GetHashCode();
            return res * Action.GetHashCode();
        }
    }
}
